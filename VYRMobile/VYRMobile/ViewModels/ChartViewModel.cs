using System;
using System.Collections.Generic;
using System.Text;
using Microcharts;
using SkiaSharp;

namespace VYRMobile.ViewModels
{

    class ChartViewModel
    {

        public Chart BarChartSample => new BarChart()
        {
            Entries = new[]
               {
                new Entry(100)
                {
                    Color = SKColor.Parse("#005EB2"),
                    Label = "Respeto",
                    ValueLabel = "75%"
                },

                new Entry(25)
                {
                    Color = SKColor.Parse("#005EB2"),
                    Label = "Educacion",
                    ValueLabel = "50%"
                },
               },
            LabelTextSize = 45
        };
    }
}
