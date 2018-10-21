using Hus.MvcWebUI.Entity;
using Hus.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hus.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {

        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrderModel()
            {
                Id=i.Id,
                OrderNumber=i.OrderNumber,
                OrderDate=i.OrderDate,
                OrderState=i.OrderState,
                Total=i.Total,
                Count=i.OrderLines.Count

            }).OrderByDescending(i=>i.OrderDate).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
               .Select(i => new OrderDetailsModel()
               {
                   OrderId = i.Id,
                   Username=i.Username,
                   OrderNumber = i.OrderNumber,
                   Total = i.Total,
                   OrderDate = i.OrderDate,
                   OrderState = i.OrderState,
                   AddressTitle = i.AddressTitle,
                   Address = i.Address,
                   City = i.City,
                   District = i.District,
                   Neighborhood = i.Neighborhood,
                   PostalCode = i.PostalCode,
                   OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                   {
                       ProductId = x.ProductId,
                       ProductName = x.Product.Name.Length > 50 ? x.Product.Name.Substring(0, 47) + "..." : x.Product.Name,
                       Image = x.Product.Image,
                       Quantity = x.Quantity,
                       Price = x.Price

                   }).ToList()
               }).FirstOrDefault();

            return View(entity);
        }

        public ActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState)
        {
            var order = db.Orders.FirstOrDefault(i => i.Id == OrderId);

            if (order != null)
            {
                order.OrderState = OrderState;
                db.SaveChanges();

                TempData["message"] = "Your information has been saved";

                return RedirectToAction("Details", new { id = OrderId });
            }

            return RedirectToAction("Index");
        }
    }
}