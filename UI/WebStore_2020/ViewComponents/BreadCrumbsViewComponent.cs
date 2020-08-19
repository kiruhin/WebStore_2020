using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ViewComponents
{
    public class BreadCrumbs : ViewComponent
    {
        private readonly IProductService _productService;

        public BreadCrumbs(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(BreadCrumbType type, int id, BreadCrumbType fromType)
        {
            // в зависимости от пришедших параметров сформировать в представление соотв. модель
            if (!Enum.IsDefined(typeof(BreadCrumbType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(BreadCrumbType));
            if (!Enum.IsDefined(typeof(BreadCrumbType), fromType))
                throw new InvalidEnumArgumentException(nameof(fromType), (int)fromType, typeof(BreadCrumbType));

            switch (type)
            {
                case BreadCrumbType.Category:
                    var category = _productService.GetCategoryById(id);
                    return View(new List<BreadCrumbViewModel>
                    {
                        new BreadCrumbViewModel()
                        {
                            BreadCrumbType = type,
                            Id = id.ToString(),
                            Name = category.Name
                        }
                    });
                case BreadCrumbType.Brand:
                    var brand = _productService.GetBrandById(id);
                    return View(new List<BreadCrumbViewModel>
                    {
                        new BreadCrumbViewModel()
                        {
                            BreadCrumbType = type,
                            Id = id.ToString(),
                            Name = brand.Name
                        }
                    });
                case BreadCrumbType.Item:
                    var crumbs = GetItemBreadCrumbs(id, fromType, type);
                    return View(crumbs);
                case BreadCrumbType.None:
                default:
                    return View(new List<BreadCrumbViewModel>());
            }
        }

        private IEnumerable<BreadCrumbViewModel> GetItemBreadCrumbs(int id, BreadCrumbType fromType, BreadCrumbType type)
        {
            var item = _productService.GetProductById(id);
            var crumbs = new List<BreadCrumbViewModel>();

            if (fromType == BreadCrumbType.Category)
                crumbs.Add(new BreadCrumbViewModel
                {
                    BreadCrumbType = fromType,
                    Id = item.Category.Id.ToString(),
                    Name = item.Category.Name
                });
            else
                crumbs.Add(new BreadCrumbViewModel
                {
                    BreadCrumbType = fromType,
                    Id = item.Brand.Id.ToString(),
                    Name = item.Brand.Name
                });

            crumbs.Add(new BreadCrumbViewModel
            {
                BreadCrumbType = type,
                Id = item.Id.ToString(),
                Name = item.Name
            });
            return crumbs;
        }

    }
}