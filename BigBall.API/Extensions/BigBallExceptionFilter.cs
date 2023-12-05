using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BigBall.Services.Contracts.Exeptions;
using BigBall.API.Exceptions;

namespace BigBall.API.Extensions
{
    /// <summary>
    /// Фильтр для обработки ошибок раздела администрирования
    /// </summary>
    public class BigBallExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as BigBallException;
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case BigBallValidationException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new ApiValidationExceptionDetail
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                case BigBallInvalidOperationException ex:
                    SetDataToContext(
                        new BadRequestObjectResult(new ApiExceptionDetail { Message = ex.Message, })
                        {
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        },
                        context);
                    break;

                case BigBallNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                    {
                        Message = ex.Message,
                    }), context);
                    break;

                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                    {
                        Message = $"Ошибка записи в БД (Проверьте индексы). {exception.Message}",
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}
