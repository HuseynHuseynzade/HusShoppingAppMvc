namespace Hus.MvcWebUI.Migrations
{
    using Hus.MvcWebUI.Entity;
    using Hus.MvcWebUI.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hus.MvcWebUI.Entity.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hus.MvcWebUI.Entity.DataContext context)
        {

            if (!context.Categories.Any())
            {
                var categories = new List<Category>()
            {
                new Category(){Name="Camera",Description="Camera Products"},
                 new Category(){Name="Computer",Description="Computer Products"},
                  new Category(){Name="Electronic",Description="Electronic Products"},
                   new Category(){Name="Telephone",Description="Telephone Products"}

            };

                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>()
            {

                new Product(){Name="Canon",Description=" Lorem ipsum dolor sit amet consectetur adipisicing elit. Natus, ipsam?",Price=1000,Stock=10,IsApproved=true,CategoryId=1,IsHome=true,Image="Canon.jpg"},
                new Product(){Name="Nikon",Description="Lorem ipsum dolor sit amet consectetur adipisicing.",Price=1500,Stock=2,IsApproved=true,CategoryId=1,IsHome=true,Image="Nikon.jpg"},
                new Product(){Name="Philips",Description=" Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsam?",Price=1200,Stock=9,IsApproved=false,CategoryId=1,IsHome=false,Image="Philips.jpg"},
                new Product(){Name="Toshiba",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Facere?",Price=1350,Stock=1,IsApproved=true,CategoryId=1,IsHome=true,Image="Toshiba.jpg"},

                new Product(){Name="HP",Description="Lorem ipsum dolor sit amet consectetur adipisicing.",Price=2000,Stock=45,IsApproved=false,CategoryId=2,IsHome=false,Image="HP.jpg"},
                new Product(){Name="Toshiba",Description=" Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsam?",Price=3000,Stock=5,IsApproved=true,CategoryId=2,IsHome=true,Image="ToshibaN.jpg"},
                new Product(){Name="Lenovo",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Natus, ipsam?",Price=4000,Stock=77,IsApproved=true,CategoryId=2,IsHome=true,Image="Lenovo.jpg"},
                new Product(){Name="ASUS",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Facere?",Price=5000,Stock=32,IsApproved=true,CategoryId=2,IsHome=true,Image="Asus.jpg"},

                new Product(){Name="Samsung",Description=" Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsam?",Price=500,Stock=10,IsApproved=false,CategoryId=3,IsHome=true,Image="SamsungTV.jpg"},
                new Product(){Name="Eurolux",Description="Lorem ipsum dolor sit amet consectetur adipisicing.",Price=850,Stock=11,IsApproved=true,CategoryId=3,IsHome=true,Image="Eurolux.jpg"},
                new Product(){Name="Toshiba",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Facere?",Price=700,Stock=55,IsApproved=true,CategoryId=3,IsHome=false,Image="ToshibaTV.jpg"},
                new Product(){Name="Vestel",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Natus, ipsam?",Price=530,Stock=56,IsApproved=false,CategoryId=3,IsHome=false,Image="Vestel.jpg"},

                new Product(){Name="Apple IPhone X",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Facere?",Price=2000,Stock=12,IsApproved=true,CategoryId=4,IsHome=false,Image="IphoneX.jpg"},
                new Product(){Name="Samsung A6",Description=" Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsam?",Price=500,Stock=24,IsApproved=true,CategoryId=4,IsHome=true,Image="SamsungA.jpg"},
                new Product(){Name="Xiaomi MI 8",Description="Lorem ipsum dolor sit amet consectetur adipisicing.",Price=1000,Stock=20,IsApproved=true,CategoryId=4,IsHome=true,Image="Xiaomi.jpg"},
                new Product(){Name="Apple IPhone 8",Description="Lorem ipsum dolor sit amet consectetur adipisicing elit. Natus, ipsam?",Price=1350,Stock=18,IsApproved=false,CategoryId=4,IsHome=true,Image="Iphone8.jpg"}


            };

                foreach (var product in products)
                {
                    context.Products.Add(product);
                }
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var admin = new ApplicationRole() { Name = "admin", Description = "admin" };
                manager.Create(admin);

                var user = new ApplicationRole() { Name = "user", Description = "user" };
                manager.Create(user);


            }
            if (!context.Users.Any())
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var mainUser = new ApplicationUser()
                {
                    Name = "Huseyn",
                    SurName = "Huseynzade",
                    UserName = "Huseynzadeh",
                    Email = "huseyn@gmail.com",


                };
                manager.Create(mainUser, "huseyn123@");
                manager.AddToRole(mainUser.Id, "admin");
                manager.AddToRole(mainUser.Id, "user");


                var user = new ApplicationUser()
                {
                    Name = "Tural",
                    SurName = "Turalov",
                    UserName = "Turalll",
                    Email = "tural@gmail.com",


                };
                manager.Create(user, "tural123@");
                manager.AddToRole(user.Id, "user");
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
