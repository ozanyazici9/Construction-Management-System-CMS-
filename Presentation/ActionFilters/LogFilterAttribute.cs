using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Contracts;

namespace Presentation.ActionFilters;

public class LogFilterAttribute : ActionFilterAttribute
{
    private readonly ILoggerService _logger;

    public LogFilterAttribute(ILoggerService logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
    }

    private string Log(string modelName, RouteData routeData)
    {
        var controller = routeData.Values["controller"];
        var action = routeData.Values["action"];

        var logDetails = new LogDetails
        {
            ModelName = modelName,
            Controller = controller,
            Action = action
        };

        if (routeData.Values.Count >= 3)
            logDetails.Id = routeData.Values["id"];

        return logDetails.ToString();
    }
}
