using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentaiton.ActionFilter;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        //Dto
        var param = context.ActionArguments
            .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

        if(param is null)
        {
            context.Result = new BadRequestObjectResult($"object is null. + " +
                $"Controller : {controller} " +
                $"Action : {action}");
            return; // 400
        }

        if(!context.ModelState.IsValid) 
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }
}
