using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Models;

namespace WebStore.DomainNew.ViewModels
{
    public class SectionCompleteViewModel
    {
        public IEnumerable<CategoryViewModel> Sections { get; set; }
        public int? CurrentParentSectionId { get; set; }
        public int? CurrentSectionId { get; set; }
    }
}