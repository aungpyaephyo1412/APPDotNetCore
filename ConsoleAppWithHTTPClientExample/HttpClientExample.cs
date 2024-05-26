using Newtonsoft.Json;

namespace ConsoleAppWithHTTPClientExample;

public class HttpClientExample
{
    private readonly HttpClient _client;

    public HttpClientExample()
    {
        _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5116/api") };
    }

    public void Run()
    {
        Read();
    }

    private async void Read()
    {
        var result = await _client.GetAsync("/Blog");
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = await result.Content.ReadAsStringAsync();
            List<BlogModel> list = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
            foreach (var li in list)
            {
                Console.WriteLine(li);
            }
        }
    }
}