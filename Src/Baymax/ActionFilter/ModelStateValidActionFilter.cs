using System.Linq;
using Baymax.Model.Dto;
using Baymax.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Baymax.ActionFilter
{
    public sealed class ModelStateValidActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context.ModelState.SelectMany(modelState =>
                modelState.Value.Errors.Select(a => new ErrorDto
                {
                    Field = modelState.Key,
                    Message = a.ErrorMessage
                })).ToList();

            var result = new ResultDto<dynamic>
            {
                Code = EnumHttpStatus.BAD_REQUEST.Value,
                Error = errors,
                Result = EnumErrorStatus.DATA_INVALID.DisplayName
            };

            context.Result = new OkObjectResult(result);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           //Not thing to do 
        }
    }
}