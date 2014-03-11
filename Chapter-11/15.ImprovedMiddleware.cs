using AppFunc = Func<IDictionary<string, object>, Task>;
    
public class ImprovedMiddleware
{
    private readonly AppFunc next;
    private readonly GreetingOptions options;

    public ImprovedMiddleware(AppFunc next, GreetingOptions options)
    {
        this.next = next;
        this.options = options;
    }

    public async Task Invoke(IDictionary<string, object> env)
    {
        IOwinContext context = new IOwinContext(env);

        string message = this.options.Message;
        if(this.options.IsHtml)
        {
            message = String.Format("<h1>{0}</h1>", message);
        }

        byte[] bytes = Encoding.UTF8.GetBytes(message);

        await context.Response.WriteAsync(bytes);
        await this.next(env);
    }
}

public class GreetingOptions
{
    public string Message { get; set; }
    public bool IsHtml { get; set; }
}


