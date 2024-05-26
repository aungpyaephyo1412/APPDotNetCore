using Newtonsoft.Json;

string jsonStr = await File.ReadAllTextAsync("data.json");
Console.WriteLine(jsonStr);
Console.ReadLine();

var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);
Console.WriteLine(model);
public class MainDto
{
    public Questions[] questions { get; set; }
    public Answers[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Questions
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answers
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}

