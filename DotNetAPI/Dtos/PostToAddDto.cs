namespace DotNetApi.Dtos
{
    public partial class PostToAddDto
    {

        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public PostToAddDto()
        {
            PostTitle = PostTitle == null ? "" : PostTitle;
            PostContent = PostContent == null ? "" : PostContent;
        }
    }
}