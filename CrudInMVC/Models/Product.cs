using System;
using System.Collections.Generic;
using System.Linq;
using CrudInMVC.Models;
using System.Threading.Tasks;

namespace CrudInMVC.Models
{
    public class Product
    {
       
        public object Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
