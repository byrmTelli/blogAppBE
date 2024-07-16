namespace blogAppBE.CORE.DBModels
{
    public class Post:BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public Category PostCategory { get; set; }
        public bool IsPublished { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
    }
}