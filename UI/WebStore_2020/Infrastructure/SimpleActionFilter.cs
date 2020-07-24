using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure
{
    public class SimpleActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //постобработка 
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //предобработка 
            //throw new NotImplementedException();
        }
    }
}
