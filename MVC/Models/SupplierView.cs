using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class SupplierView
    {
        public int Id { set; get; }
        [Required]
        [StringLength(40)]
        public string CompanyName { set; get; }
        [StringLength(30)]
        public string ContactName { set; get; }
        [StringLength(30)]
        public string ContactTitle { set; get; }
        [StringLength(60)]
        public string Address { set; get; }
        [StringLength(15)]
        public string City { set; get; }
        [StringLength(15)]
        public string Country { set; get; }
        
    }
}
