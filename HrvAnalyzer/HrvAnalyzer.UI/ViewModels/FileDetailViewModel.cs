namespace HrvAnalyzer.UI.ViewModels
{
    public class FileDetailViewModel : ViewModelBase, IFileDetailViewModel
    {
        private string _fileName;

        public FileDetailViewModel()
        {
            Initialize();
        }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private void Initialize()
        {
            _fileName = string.Empty;
        }
    }
}
