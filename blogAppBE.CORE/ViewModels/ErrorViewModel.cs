namespace blogAppBE.CORE.ViewModels
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; }
        public bool ShowError { get; set; }

        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public ErrorViewModel(string _error,bool _showError)
        {
            Errors = new List<string>();
            Errors.Add(_error);
            ShowError = _showError;
        }
        
        public ErrorViewModel(List<string> _errors, bool _showError)
        {
            Errors = new List<string>();
            Errors.AddRange(_errors);
            ShowError = _showError;
        }
    }
}