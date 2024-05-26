using System.Net.Mime;
using System.Text;
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

    private async void Edit(int id)
    {
        var result = await _client.GetAsync($"/Blog/{id}");
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = await result.Content.ReadAsStringAsync();
            BlogModel item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine(item);
            return;
        }

        string message = await result.Content.ReadAsStringAsync();
        Console.WriteLine(message);
    }

    private async void Delete(int id)
    {
        var result = await _client.DeleteAsync($"/Blog/{id}");
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = await result.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
            return;
        }

        string message = await result.Content.ReadAsStringAsync();
        Console.WriteLine(message);
    }

    private async void Create(string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(blogModel), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var response = await _client.PostAsync("/blog", httpContent);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content.ReadAsStringAsync());
        }

        Console.WriteLine(response.Content.ReadAsStringAsync());
    }

    private async void Update(int id, string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(blogModel), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var response = await _client.PostAsync($"/blog{id}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content.ReadAsStringAsync());
        }

        Console.WriteLine(response.Content.ReadAsStringAsync());
    }
}