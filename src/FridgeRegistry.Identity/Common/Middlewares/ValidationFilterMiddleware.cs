using FridgeRegistry.Identity.Contracts.Responses;
using FridgeRegistry.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FridgeRegistry.Identity.Common.Middlewares;

public class ValidationFilterMiddleware : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    keyValuePair => keyValuePair.Key,
                    keyValuePair => keyValuePair.Value.Errors
                        .Select(error => error.ErrorMessage)
                )
                .ToArray();

            var validationErrorResponse = new ValidationErrorResponse();

            foreach (var error in errorsInModelState)
            {
                foreach (var message in error.Value)
                {
                    var errorModel = new ValidationErrorModel()
                    {
                        FieldName = error.Key,
                        Message = message,
                    };
                    
                    validationErrorResponse.Errors.Add(errorModel);
                }
            }

            context.Result = new BadRequestObjectResult(validationErrorResponse);
            return;
        }
        
        await next();
    }
}