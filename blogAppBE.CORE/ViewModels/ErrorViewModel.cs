namespace blogAppBE.CORE.ViewModels
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; }

        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public ErrorViewModel(string _error)
        {
            Errors = new List<string>();
            Errors.Add(_error);
        }
        
        public ErrorViewModel(List<string> _errors)
        {
            Errors = new List<string>();
            Errors.AddRange(_errors);
        }
    }
}