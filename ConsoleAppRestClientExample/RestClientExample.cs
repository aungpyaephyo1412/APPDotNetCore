using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleAppRestClientExample;

public class RestClientExample
{
    private readonly RestClient _client;

    public RestClientExample()
    {
        _client = new RestClient(new Uri("http://localhost:5116/api"));
    }

    public void Run()
    {
        Read();
    }

    private async void Read()
    {
        RestRequest request = new RestRequest("/blog");
        var result = await _client.GetAsync(request);
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = result.Content!;
            List<BlogModel> list = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
            foreach (var li in list)
            {
                Console.WriteLine(li);
            }
        }
    }

    private async void Edit(int id)
    {
        RestRequest request = new RestRequest($"/Blog/{id}");
        var result = await _client.GetAsync(request);
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = result.Content!;
            BlogModel item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine(item);
            return;
        }

        string message = result.Content!;
        Console.WriteLine(message);
    }

    private async void Delete(int id)
    {
        RestRequest request = new RestRequest($"/Blog/{id}", Method.Delete);
        var result = await _client.ExecuteAsync(request);
        if (result.IsSuccessStatusCode)
        {
            string jsonStr = result.Content!;
            Console.WriteLine(jsonStr);
            return;
        }

        string message = result.Content!;
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
        RestRequest request = new RestRequest("/blog", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(blogModel));
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content);
            return;
        }

        Console.WriteLine(response.Content);
    }

    private async void Update(int id, string title, string author, string content)
    {
        BlogModel blogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        RestRequest request = new RestRequest($"/blog{id}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(blogModel));
        var response = await _client.PostAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content);
        }

        Console.WriteLine(response.Content);
    }
}