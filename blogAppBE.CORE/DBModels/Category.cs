namespace blogAppBE.CORE.DBModels
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}