using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.ViewModels
{
    class ReportViewModel
    {
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public Uri Img { get; set; }
    }
}
