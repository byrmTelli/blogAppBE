namespace blogAppBE.CORE.RequestModels.Post
{
    public class PostUpdateRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}