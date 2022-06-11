using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Usuario.API.Attributes
{
    public class EntityTagAttribute : Attribute, IActionFilter
    {
        public readonly string CurrentEntity;

        public EntityTagAttribute(string entity = "")
        {
            CurrentEntity = entity;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Console.WriteLine(_prefix + "OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Console.WriteLine(_prefix + "OnActionExecuting");
        }
    }
}
