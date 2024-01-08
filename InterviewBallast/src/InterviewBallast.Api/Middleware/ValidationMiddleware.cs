using FluentValidation;
using Newtonsoft.Json;

namespace InterviewBallast.Api.Middleware
{
    public class ValidationMiddleware<T> where T : class
    {
        private readonly RequestDelegate _next;
        private readonly IValidator<T> _validator;

        public ValidationMiddleware(RequestDelegate next, IValidator<T> validator)
        {
            _next = next;
            _validator = validator;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "POST" || context.Request.Method == "PUT")
            {
                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var entity = JsonConvert.DeserializeObject<T>(requestBody);

                var validationResult = _validator.Validate(entity);

                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = 400; // Bad Request
                    context.Response.ContentType = "application/json";

                    var errors = JsonConvert.SerializeObject(validationResult.Errors);
                    await context.Response.WriteAsync(errors);
                    return;
                }
            }

            await _next(context);
        }
    }
}
