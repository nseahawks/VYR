using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;

namespace VYRMobile.Models
{
    public class Data
    {
        public ObservableCollection<ChartDataPoint> Radar { get; set; }

        public Data()
        {
            Radar = new ObservableCollection<ChartDataPoint>();

            Radar.Add(new ChartDataPoint("Educacion", 50));
            Radar.Add(new ChartDataPoint("Respeto", 60));
            Radar.Add(new ChartDataPoint("Irrespeto", 70));
            Radar.Add(new ChartDataPoint("Puntualidad", 80));
            Radar.Add(new ChartDataPoint("Tardanza", 90));
            Radar.Add(new ChartDataPoint("Bonificacion", 100));

        }
    }
}
