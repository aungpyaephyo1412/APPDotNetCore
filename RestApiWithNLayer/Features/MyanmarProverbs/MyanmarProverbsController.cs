using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestApiWithNLayer.Features.MyanmarProverbs;

[Route("api/[controller]")]
public class MyanmarProverbsController : ControllerBase
{
    private async Task<MyanmarProverbsDto?> GetDataFromApi()
    {
        // HttpClient httpClient = new HttpClient();
        // var response = await httpClient.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
        // if (response.IsSuccessStatusCode)
        // {
        //     string jsonStr = await response.Content.ReadAsStringAsync();
        //     var obj = JsonConvert.DeserializeObject<MyanmarProverbsDto>(jsonStr)!;
        //     return obj;
        // }
        // return null;
        string jsonStr = await System.IO.File.ReadAllTextAsync("data-3.json");
        var model = JsonConvert.DeserializeObject<MyanmarProverbsDto>(jsonStr);
        return model;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await GetDataFromApi();
        if (data is null)
        {
            return NotFound();
        }

        return Ok(data.Tbl_MMProverbsTitle);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Get(string slug)
    {
        var data = await GetDataFromApi();
        if (data is null)
        {
            return NotFound();
        }

        var item = data.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == slug);
        if (item is null)
        {
            return NotFound();
        }

        var titleId = item.TitleId;
        var proverbs = data.Tbl_MMProverbs.Where(x => x.TitleId == titleId).ToList();
        return Ok(proverbs);
    }

    [HttpGet("{titleId}/{proverbId}")]
    public async Task<IActionResult> Get(int titleId, int proverbId)
    {
        var data = await GetDataFromApi();
        var proverbs = data!.Tbl_MMProverbs.Where(x => x.TitleId == titleId && x.ProverbId == proverbId);
        return Ok(proverbs);
    }
}