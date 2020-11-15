using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

using HrvAnalyzer.UI.Services;

using Prism.Commands;

namespace HrvAnalyzer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusText;
        private bool isTimeDomainProcessing;

        public MainViewModel(
            IOpenFileService openFileService,
            IFileDetailViewModel fileDetailViewModel,
            ITimeDomainViewModel timeDomainViewModel)
        {
            OpenFileService = openFileService;
            FileDetailViewModel = fileDetailViewModel;
            TimeDomainViewModel = timeDomainViewModel;
            StatusText = "The program is running";
            RegisterCommandHandlers();
        }

        public string StatusText
        {
            get => statusText;
            set => SetProperty(ref statusText, value);
        }

        public bool IsTimeDomainProcessing
        {
            get => isTimeDomainProcessing;
            set => SetProperty(ref isTimeDomainProcessing, value);
        }

        public IFileDetailViewModel FileDetailViewModel { get; }
        public string SourceFileName => FileDetailViewModel.FileName;
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
                StatusText = "File opened successfully";
                ((DelegateCommand)StartAnalysisCommand).RaiseCanExecuteChanged();
            }
        }

        private bool OnStartAnalysisCanExecute()
        {
            return !string.IsNullOrEmpty(FileDetailViewModel.FileName);
        }

        private void OnStartAnalysisExecute()
        {
            StatusText = "Analysis started";
            var datas = LoadData();

            if (datas == null)
            {
                return;
            }

            IsTimeDomainProcessing = TimeDomainViewModel.ProcessData(datas);
        }

        private IEnumerable<double> LoadData()
        {
            StatusText = "Loading data";
            string[] lines = null;

            if (File.Exists(SourceFileName))
            {
                try
                {
                    lines = File.ReadAllLines(SourceFileName);
                }
                catch (Exception)
                {
                    StatusText = "Data read error";
                    return null;
                }
            }

            var values = new List<double>();

            foreach (var line in lines)
            {
                if (double.TryParse(line, out double result))
                {
                    values.Add(result);
                }
                else
                {
                    StatusText = "Data conversion error";
                    return null;
                }
            }

            if (values.Count == 0)
            {
                StatusText = "Insufficient data for analysis";
                return null;
            }

            StatusText = "Data loaded successfully";
            return values;
        }
    }
}
