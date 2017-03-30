using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class Datum
    {
        public int id { get; set; }
        public string movie_id { get; set; }
        public string certification { get; set; }
        public string iso_3166_1 { get; set; }
        public string release_date { get; set; }
    }

    public class BODBReleaseDate
    {
        public string result { get; set; }
        public string error { get; set; }
        public List<Datum> data { get; set; }
        public int length { get; set; }
    }
}

