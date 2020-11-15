using System.Collections.Generic;

namespace HrvAnalyzer.UI.ViewModels
{
    public interface ITimeDomainViewModel
    {
        bool ProcessData(IEnumerable<double> values);
    }
}