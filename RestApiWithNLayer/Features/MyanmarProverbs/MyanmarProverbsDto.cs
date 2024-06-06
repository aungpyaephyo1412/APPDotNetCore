namespace RestApiWithNLayer.Features.MyanmarProverbs;

public class MyanmarProverbsDto
{
    public Tbl_MMProverbsTitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_MMProverbs[] Tbl_MMProverbs { get; set; }
}

public class Tbl_MMProverbsTitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_MMProverbs
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}