using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace OrdenCompra.API.Attributes
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

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
