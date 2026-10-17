using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        var param = context
            .ActionArguments.SingleOrDefault(p => p.Value?.GetType().Name.Contains("Dto") == true)
            .Value;

        if (param is null)
        {
            context.Result = new BadRequestObjectResult(
                $"Object is null in controller {controller} and action {action}"
            );
            return;
        }

        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }
}
