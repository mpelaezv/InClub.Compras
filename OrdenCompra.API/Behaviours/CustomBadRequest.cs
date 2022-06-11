using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrdenCompra.API.Attributes;
using System.Linq;

namespace OrdenCompra.API.Behaviours
{
    public class CustomBadRequest : ValidationProblemDetails
    {
        public CustomBadRequest(ActionContext context)
        {
            ConstructErrorMessages(context);
            Type = context.HttpContext.TraceIdentifier;
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            var myerror = "A non-empty request body is required.";
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        if (errorMessage == myerror)
                        {
                            var attributes = context.ActionDescriptor.EndpointMetadata.OfType<EntityTagAttribute>().First();
                            key = "RequestBodyError";
                            Errors.Add(key, new[] { $"El {attributes.CurrentEntity} no debe ser nulo." });
                        }
                        else
                        {
                            Errors.Add(key, new[] { errorMessage });
                        }

                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                            if (errorMessages[i] == myerror)
                            {
                                var attributes = context.ActionDescriptor.EndpointMetadata.OfType<EntityTagAttribute>().First();
                                key = "RequestBodyError";
                                errorMessages[i] = $"El {attributes.CurrentEntity} no debe ser nulo.";
                            }
                        }

                        Errors.Add(key, errorMessages);
                    }
                }
            }
        }

        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
                "The input was not valid." :
            error.ErrorMessage;
        }
    }
}
