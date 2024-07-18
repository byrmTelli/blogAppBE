using blogAppBE.CORE.DBModels;

namespace blogAppBE.CORE.RequestModels.Post
{
    public class PostRequestModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}