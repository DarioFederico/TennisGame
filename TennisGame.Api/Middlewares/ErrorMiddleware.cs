namespace TennisGame.Api.Middlewares;

public class ErrorInfo
{
    public int StatusCode { get; set; } = 0;
    public string ErrorMessage { get; set; } = string.Empty;
}


public class ErrorHandler
{
    private readonly RequestDelegate requestDelegate;
       
    public ErrorHandler(RequestDelegate requestDelegate)
    {
        this.requestDelegate = requestDelegate;
          
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;

            var errorInfo = new ErrorInfo
            { 
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            await context.Response.WriteAsJsonAsync<ErrorInfo>(errorInfo);
        }
    }
}