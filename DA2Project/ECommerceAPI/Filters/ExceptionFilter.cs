using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Logic.Exceptions;
using Domain.Models.Exceptions;
using DataAccess.Exceptions;

namespace ECommerceAPI.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (context.Exception is RequestValidationException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is InvalidPermissionException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else if (context.Exception is AlreadyExistsException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is CurrentUserException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (context.Exception is InvalidCredentialsException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (context.Exception is NoContentException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            else if (context.Exception is NotFoundException || context.Exception is NoStockException || context.Exception is OutOfStockException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else if (context.Exception is NoStockException || context.Exception is InstanceNotUniqueException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is PaymentMethodException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is UnhandledLogicException || context.Exception is DataAccessException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                context.Result = new ObjectResult(new { ErrorMessage = "Unexpected error: " + context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
