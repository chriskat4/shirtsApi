﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shirtsApi.Model.Repository;

namespace shirtsApi.Model.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out int shirtId))
            {
                if (!ShirtRepository.shirtExist(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", "ShirtId doesn't exist anymore");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}
