// See https://aka.ms/new-console-template for more information

using ConsoleAppWithHTTPClientExample;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");
HttpClientExample httpClientExample = new HttpClientExample();
httpClientExample.Run();
Console.ReadKey();

