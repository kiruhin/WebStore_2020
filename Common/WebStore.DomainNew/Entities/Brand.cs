﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("ProductBrands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}