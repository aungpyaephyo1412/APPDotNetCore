using Refit;

namespace ConsoleAppRefitExample;

public class RefitExample
{
    private readonly IBlogApi _api =  RestService.For<IBlogApi>("https://localhost:5116");
    
    public async Task ReadAsync()
    {
        var result = await _api.GetBlogs();
        Console.WriteLine(result);
    }
}