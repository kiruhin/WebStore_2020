using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Models;

namespace WebStore.DomainNew.ViewModels
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }
        public int? CurrentBrandId { get; set; }
    }
}