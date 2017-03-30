using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class IGDBCompany
    {
        public int id { get; set; }
        public string name { get; set; }
        public object created_at { get; set; }
        public object updated_at { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int start_date_category { get; set; }
        public int change_date_category { get; set; }
        public List<int> published { get; set; }
        public List<int?> developed { get; set; }
    }
}

