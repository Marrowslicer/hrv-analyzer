using Microsoft.Win32;

namespace HrvAnalyzer.UI.Services
{
    public class OpenFileService : IOpenFileService
    {
        private const string DefaultFileExtension = ".txt";
        private const string DefaultFileFilter = "Text documents(.txt)|*.txt;";

        private OpenFileDialog _openFileDialog;
        private string _fileName;

        public OpenFileService()
        {
            Initialize();
        }

        public string FileName => _fileName;

        public bool? OpenFile()
        {
            _openFileDialog.DefaultExt = DefaultFileExtension;
            _openFileDialog.Filter = DefaultFileFilter;

            var result = _openFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _fileName = _openFileDialog.FileName;
            }

            return result;
        }

        private void Initialize()
        {
            _openFileDialog = new OpenFileDialog();
            _fileName = string.Empty;
        }
    }
}
