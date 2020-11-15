namespace HrvAnalyzer.UI.Services
{
    public interface IOpenFileService
    {
        string FileName { get; }

        bool? OpenFile();
    }
}