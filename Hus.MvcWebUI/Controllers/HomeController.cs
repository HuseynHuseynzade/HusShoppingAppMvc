using Hus.MvcWebUI.Entity;
using Hus.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hus.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();


        // GET: Home
        public ActionResult Index()
        {
            var products = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id=i.Id,
                    Name= i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description =i.Description.Length>50?i.Description.Substring(0,47)+"...":i.Description,
                    Price=i.Price,
                    Stock=i.Stock,
                    Image=i.Image,
                    CategoryId=i.CategoryId

                }).ToList();

            return View(products);
        }

        public ActionResult Details(int id)
        {
           
                return View(_context.Products.Where(i => i.Id == id).FirstOrDefault());
           
            
        }
        public ActionResult List(int? id)
        {
            var products = _context.Products
               .Where(i => i.IsApproved)
               .Select(i => new ProductModel()
               {
                   Id = i.Id,
                   Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                   Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                   Price = i.Price,
                   Stock = i.Stock,
                   Image = i.Image==null? "1.jpg":i.Image,
                   CategoryId = i.CategoryId

               }).AsQueryable();

            if (id != null)
            {
                products = products.Where(i => i.CategoryId == id);
            }

            return View(products.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }
    }
}