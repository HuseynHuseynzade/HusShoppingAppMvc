using Hus.MvcWebUI.Entity;
using Hus.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hus.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product,1);
            }
            return RedirectToAction("Index");
        }
        
         public ActionResult RemoveFromCart(int Id)
         {
            var product = db.Products.FirstOrDefault(x => x.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
         }

        public Cart GetCart()
        {
            var Cart = (Cart)Session["Cart"];

            if (Cart == null)
            {
                Cart = new Cart();
                Session["Cart"] = Cart;

            }
           

            return Cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }


        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }


        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {

            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoProductError", "There are no products in your cart");
            }

            if (ModelState.IsValid)
            {

                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }
        }

        private void SaveOrder(Cart cart,ShippingDetails entity)
        {
            var Order = new Order();
            Order.OrderNumber ="A"+ (new Random()).Next(11111,99999).ToString();
            Order.Total = cart.Total();
            Order.OrderDate = DateTime.Now;
            Order.OrderState = EnumOrderState.Waiting;
            Order.Username = User.Identity.Name;

            
            Order.AddressTitle = entity.AddressTitle;
            Order.Address = entity.Address;
            Order.City = entity.City;
            Order.District = entity.District;
            Order.Neighborhood = entity.Neighborhood;
            Order.PostalCode = entity.PostalCode;

            Order.OrderLines = new List<OrderLine>();

            foreach(var prod in cart.CartLines)
            {
                var orderLine = new OrderLine();

                orderLine.Quantity = prod.Quantity;
                orderLine.Price =prod.Quantity * prod.Product.Price;
                orderLine.ProductId = prod.Product.Id;

                Order.OrderLines.Add(orderLine);

            }

            db.Orders.Add(Order);
            db.SaveChanges();
        }

            
    }
}