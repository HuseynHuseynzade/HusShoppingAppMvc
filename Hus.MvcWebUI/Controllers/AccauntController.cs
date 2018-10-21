using Hus.MvcWebUI.Entity;
using Hus.MvcWebUI.Identity;
using Hus.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hus.MvcWebUI.Controllers
{

     
    public class AccauntController : Controller
    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccauntController()
        {
              var userStore = new UserStore<ApplicationUser>(new DataContext());
                 UserManager = new UserManager<ApplicationUser>(userStore);

              var roleStore = new RoleStore<ApplicationRole>(new DataContext());
                 RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }


        [Authorize]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders
                .Where(i => i.Username == User.Identity.Name)
                .Select(i=>new UserOrderModel()
                {
                    Id=i.Id,
                    OrderNumber=i.OrderNumber,
                    OrderDate=i.OrderDate,
                    OrderState=i.OrderState,
                    Total=i.Total

                }).OrderByDescending(i=>i.OrderDate).ToList();

            return View(orders);
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
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
                        ProductId=x.ProductId,
                        ProductName=x.Product.Name.Length > 50 ? x.Product.Name.Substring(0, 47) + "..." : x.Product.Name,
                        Image =x.Product.Image,
                        Quantity=x.Quantity,
                        Price=x.Price

                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }
        
        // GET: Accaunt
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.SurName = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;

              var result= UserManager.Create(user, model.Password);
              
                if (result.Succeeded)
                {
                   if(RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Accaunt");
                }
                else
                {
                    ModelState.AddModelError("Register user error", "Error creating register");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);

                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                      return  Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login user error", "Such a user does not exist");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}