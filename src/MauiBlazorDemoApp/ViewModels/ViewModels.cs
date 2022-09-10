using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorDemoApp.ViewModels
{

    public class TodayItem
    {
        public string StartTime { get; set; } = "00:00";
        public string EndTime { get; set; } = "00:00";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

    }

    public class Today
    {
        public List<TodayItem> Items { get; set; } = new List<TodayItem>();
        public string SchoolStartTime { get; set; } = "";
        public string SchoolEndTime { get; set; } = "";
        public bool Sportsbag { get; set; }
    }

}
