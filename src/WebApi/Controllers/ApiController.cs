using API.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.WebApi.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected List<Error> errors = new List<Error>();
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.IsValidationError))
        {
            return ValidationProblem(errors);
        }
        return Problem(errors.First(x => !x.IsValidationError));
    }

    private IActionResult Problem(Error error)
    {
        if (error.ErrorCode == null)
        {
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: error.Title);
        }
        return Problem(statusCode: error.ErrorCode, title: error.Title);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                "Validation",
                error.Title!);
        }
        return ValidationProblem(modelStateDictionary);
    }
}
