using System;
using System.Windows.Input;

using HrvAnalyzer.UI.Services;

using Prism.Commands;

namespace HrvAnalyzer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            IOpenFileService openFileService,
            IFileDetailViewModel fileDetailViewModel)
        {
            OpenFileService = openFileService;
            FileDetailViewModel = fileDetailViewModel;

            RegisterCommandHandlers();
        }

        public IFileDetailViewModel FileDetailViewModel { get; }

        public IOpenFileService OpenFileService { get; }

        public ICommand OpenFileCommand { get; private set; }

        private void RegisterCommandHandlers()
        {
            OpenFileCommand = new DelegateCommand(OnOpenFileExecute);
        }

        private void OnOpenFileExecute()
        {
            var result = OpenFileService.OpenFile();

            if (result.HasValue && result.Value)
            {
                FileDetailViewModel.FileName = OpenFileService.FileName;
            }
        }
    }
}
