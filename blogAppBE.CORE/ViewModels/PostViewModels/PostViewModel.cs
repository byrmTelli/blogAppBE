using blogAppBE.CORE.ViewModels.CategoryViewModels;

namespace blogAppBE.CORE.ViewModels.PostViewModels
{
    public class PostViewModel:BaseViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //category,creator
        public CategoryViewModel Category { get; set; }
    }
}