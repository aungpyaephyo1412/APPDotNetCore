using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace RestApiWithNLayer.Features.LatHtaukBayDin;
[Route("api/[controller]")]
public class LatHtaukBayDinController : ControllerBase
{
     private async Task<LatHtaukBayDinModel.LatHtaukBayDin> GetData()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
        var model = JsonConvert.DeserializeObject<LatHtaukBayDinModel.LatHtaukBayDin>(jsonStr);
        return model;
    }
    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var model = await GetData();
        return Ok(model.questions);
    }

    [HttpGet("number-lists")]
    public async Task<IActionResult> GetNumberLists()
    {
        var model = await GetData();
        return Ok(model.numberList);
    }

    [HttpGet("{questionNo}/{answerNo}")]
    public async Task<IActionResult> GetAnswer(int questionNo, int answerNo)
    {
        var model = await GetData();
        return Ok(model.answers.FirstOrDefault(x=>x.questionNo == questionNo && x.answerNo == answerNo));
    }
}