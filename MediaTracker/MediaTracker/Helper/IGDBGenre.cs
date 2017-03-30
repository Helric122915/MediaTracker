using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class IGDBGenre
    {
        public int id { get; set; }
        public string name { get; set; }
        public object created_at { get; set; }
        public object updated_at { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
    }
}
