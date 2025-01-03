namespace DotNetApi.Dtos
{
    public partial class PostToEditDto
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public PostToEditDto()
        {
            PostTitle = PostTitle == null ? "" : PostTitle;
            PostContent = PostContent == null ? "" : PostContent;
        }
    }
}