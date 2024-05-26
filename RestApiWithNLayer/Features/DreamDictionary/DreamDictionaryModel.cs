namespace RestApiWithNLayer.Features.DreamDictionary;

public class DreamDictionaryModel
{
    public class MainDto
    {
        public BlogHeader[] BlogHeader { get; set; }
        public BlogDetail[] BlogDetail { get; set; }
    }

    public class BlogHeader
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
    }

    public class BlogDetail
    {
        public int BlogDetailId { get; set; }
        public int BlogId { get; set; }
        public string BlogContent { get; set; }
    }
}