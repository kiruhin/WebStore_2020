using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class Sections : ViewComponent
    {
        private readonly IProductService _productService;

        public Sections(IProductService productService)
        {
            _productService = productService;
        }

        // основной метод компонента теперь будет принимать параметр sectionId
        public async Task<IViewComponentResult> InvokeAsync(string sectionId)
        {
            // сконвертируем строковый sectionId в число
            int.TryParse(sectionId, out var sectionIdInt);
            // будем получать id родительской секции в методе GetSections()
            var sections = GetSections(sectionIdInt, out var parentSectionId);
            // возвращать будем новую модель с инфой о текущей секции/категории
            return View(new SectionCompleteViewModel
            {
                Sections = sections, 
                CurrentSectionId = sectionIdInt, 
                CurrentParentSectionId = parentSectionId
            });
        }

        /// <summary>
        /// Получает секции из базы и строит дерево
        /// также получает id родительской категории для переданной категории
        /// </summary>
        /// <returns></returns>
        private List<CategoryViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var categories = _productService.GetCategories();

            var parentCategories = categories.Where(x => !x.ParentId.HasValue).ToArray();
            var parentSections = new List<CategoryViewModel>();

            // получим и заполним родительские категории
            foreach (var parentCategory in parentCategories)
            {
                parentSections.Add(new CategoryViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory= null
                });
            }

            // получим и заполним дочерние категории
            foreach (var sectionViewModel in parentSections)
            {
                var childCategories = categories.Where(c => c.ParentId == sectionViewModel.Id);
                foreach (var childCategory in childCategories)
                {
                    // определение родительской категории
                    if (childCategory.Id == sectionId)
                        parentSectionId = sectionViewModel.Id;

                    sectionViewModel.ChildCategories.Add(new CategoryViewModel
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = sectionViewModel
                    });
                }

                sectionViewModel.ChildCategories = sectionViewModel.ChildCategories
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            parentSections = parentSections.OrderBy(c => c.Order).ToList();

            return parentSections;
        }
    }
}