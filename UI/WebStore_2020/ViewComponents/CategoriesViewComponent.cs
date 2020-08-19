using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly IProductService _productService;

        public Categories(IProductService productService)
        {
            _productService = productService;
        }

        // основной метод компонента теперь будет принимать параметр categoryId
        public async Task<IViewComponentResult> InvokeAsync(string categoryId)
        {
            // сконвертируем строковый categoryId в число
            int.TryParse(categoryId, out var categoryIdInt);
            // будем получать id родительской секции в методе GetCategories()
            var categories = GetCategories(categoryIdInt, out var parentCategoryId);
            // возвращать будем новую модель с инфой о текущей секции/категории
            return View(new CategoryCompleteViewModel
            {
                Categories = categories, 
                CurrentCategoryId = categoryIdInt, 
                CurrentParentCategoryId = parentCategoryId
            });
        }

        /// <summary>
        /// Получает секции из базы и строит дерево
        /// также получает id родительской категории для переданной категории
        /// </summary>
        /// <returns></returns>
        private List<CategoryViewModel> GetCategories(int? categoryId, out int? parentCategoryId)
        {
            parentCategoryId = null;

            var categories = _productService.GetCategories();

            var parentCategories = categories.Where(x => !x.ParentId.HasValue).ToArray();
            var parentCategoriesVm = new List<CategoryViewModel>();

            // получим и заполним родительские категории
            foreach (var parentCategory in parentCategories)
            {
                parentCategoriesVm.Add(new CategoryViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                });
            }

            // получим и заполним дочерние категории
            foreach (var categoryViewModel in parentCategoriesVm)
            {
                var childCategories = categories.Where(c => c.ParentId == categoryViewModel.Id);
                foreach (var childCategory in childCategories)
                {
                    // определение родительской категории
                    if (childCategory.Id == categoryId)
                        parentCategoryId = categoryViewModel.Id;

                    categoryViewModel.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = categoryViewModel
                    });
                }

                categoryViewModel.ChildCategories = categoryViewModel.ChildCategories
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            parentCategoriesVm = parentCategoriesVm.OrderBy(c => c.Order).ToList();

            return parentCategoriesVm;
        }
    }
}