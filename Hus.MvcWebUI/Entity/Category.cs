using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hus.MvcWebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(20,MinimumLength =4,ErrorMessage = "There are up to 20 characters !")]
        public string Name { get; set; }
       
        public string Description { get; set; }
        
       
        public List<Product> Products { get; set; }
       
    }
}