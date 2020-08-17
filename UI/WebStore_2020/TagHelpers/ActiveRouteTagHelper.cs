using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        /// <summary>Имя действия контроллера</summary>
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        /// <summary>Имя контроллера</summary>
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeNotBound] // не искать это свойство в атрибутах тэга
        [ViewContext] // сюда попадет инфа о запрашиваемом пути
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            bool ignoreAction = context
                .AllAttributes
                .TryGetAttribute("ignore-action", out _);

            if (ShouldBeActive(ignoreAction))
                MakeActive(output);
        }

        private void MakeActive(TagHelperOutput output)
        {
            // получим значение атрибута class (все css-классы для тэга)
            var classAttribute = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttribute == null)
            { // если такого атрибута нет, то добавляем его со значением 'active' (для подсветки)
                classAttribute = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttribute);
            }
            else if (classAttribute.Value?.ToString().Contains("active", StringComparison.Ordinal) != true)
            { // если такой атрибут уже есть у тэга, то дописываем к нему ' active'
                output.Attributes.SetAttribute("class", classAttribute.Value is null
                    ? "active"
                    : classAttribute.Value + " active");
            }
        }

        private bool ShouldBeActive(bool ignoreAction)
        {
            // получим текущие значения контроллера и action-метода
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["Action"].ToString();

            // если контроллер не найден или не наш, то не подсвечиваем
            if (!string.IsNullOrWhiteSpace(Controller) && currentController != Controller)
                return false;

            // контроллер уже наш!
            // если action не найден или не наш, то не подсвечиваем
            if (!ignoreAction && !string.IsNullOrWhiteSpace(Action) && Action != currentAction)
                return false;

            // считаем, что пункт меню активный
            return true;
        }
    }
}
