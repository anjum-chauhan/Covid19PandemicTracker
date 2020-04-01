using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19PandemicTracker.Models
{
    public class Country
    {
        public string date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
    }
}
