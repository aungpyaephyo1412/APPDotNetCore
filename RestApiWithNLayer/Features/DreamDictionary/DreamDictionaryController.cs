using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestApiWithNLayer.Features.DreamDictionary;

[Route("api/[controller]")]
public class DreamDictionaryController : ControllerBase
{
    [HttpGet("dreams")]
    public async Task<IActionResult> GetDreamsLists()
    {
        var model = await GetData();
        return Ok(model.BlogHeader);
    }
    [HttpGet("dreams/{id}")]
    public async Task<IActionResult> GetDreamDetails(int id)
    {
        var model = await GetData();
        return Ok(model.BlogDetail.Where(x=>x.BlogId == id));
    }
    private async Task<DreamDictionaryModel.MainDto> GetData()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("data-2.json");
        var model = JsonConvert.DeserializeObject<DreamDictionaryModel.MainDto>(jsonStr);
        return model;
    }
}