using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class IGDBReleaseDate
    {
        public int id { get; set; }
        public int game { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public int category { get; set; }
        public int platform { get; set; }
        public long date { get; set; }
        public int y { get; set; }
        public int m { get; set; }
        public string human { get; set; }
    }
}
