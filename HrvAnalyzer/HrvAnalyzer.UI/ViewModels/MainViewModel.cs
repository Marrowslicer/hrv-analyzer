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
            IFileDetailViewModel fileDetailViewModel,
            ITimeDomainViewModel timeDomainViewModel)
        {
            OpenFileService = openFileService;
            FileDetailViewModel = fileDetailViewModel;
            TimeDomainViewModel = timeDomainViewModel;
            RegisterCommandHandlers();
        }

        public IFileDetailViewModel FileDetailViewModel { get; }
        public ITimeDomainViewModel TimeDomainViewModel { get; }
        public IOpenFileService OpenFileService { get; }
        public ICommand OpenFileCommand { get; private set; }
        public ICommand StartAnalysisCommand { get; private set; }

        private void RegisterCommandHandlers()
        {
            OpenFileCommand = new DelegateCommand(OnOpenFileExecute);
            StartAnalysisCommand = new DelegateCommand(OnStartAnalysisExecute,
                OnStartAnalysisCanExecute);
        }

        private void OnOpenFileExecute()
        {
            var result = OpenFileService.OpenFile();

            if (result.HasValue && result.Value)
            {
                FileDetailViewModel.FileName = OpenFileService.FileName;
                ((DelegateCommand)StartAnalysisCommand).RaiseCanExecuteChanged();
            }
        }

        private bool OnStartAnalysisCanExecute()
        {
            return !string.IsNullOrEmpty(FileDetailViewModel.FileName);
        }

        private void OnStartAnalysisExecute()
        {
            LoadData();
        }

        private void LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
