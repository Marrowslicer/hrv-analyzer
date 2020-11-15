using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using LiveCharts;
using LiveCharts.Wpf;

namespace HrvAnalyzer.UI.ViewModels
{
    public class TimeDomainViewModel : ViewModelBase, ITimeDomainViewModel
    {
        private string[] labels;
        private Func<double, string> formatter;

        public TimeDomainViewModel()
        {
            Formatter = value => value.ToString();
            SeriesCollection = new SeriesCollection();
        }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> Formatter
        {
            get => formatter;
            set => SetProperty(ref formatter, value); 
        }

        public string[] Labels
        {
            get => labels;
            set => SetProperty(ref labels, value);
        }

        public bool ProcessData(IEnumerable<double> data)
        {
            var groups = data.GroupBy(d => d);
            var values = groups.Select(g => Convert.ToDouble(g.Count()));

            SeriesCollection.Clear();
            SeriesCollection.Add(
                new ColumnSeries
                {
                    Title = "Counts",
                    Fill = Brushes.Red,
                    DataLabels = true,
                    Values = new ChartValues<double>(values)
                }
            );

            var keys = groups.Select(g => g.Key).ToList();
            keys.Sort();
            Labels = keys.Select(k => k.ToString("F4")).ToArray();

            return true;
        }
    }
}
