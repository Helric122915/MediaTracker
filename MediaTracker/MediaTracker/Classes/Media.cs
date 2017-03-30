using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Classes
{
    public abstract class Media
    {
        public string Title { get; set; }
        public ushort PersonalRating { get; set; }
        public DateTime DateAdded { get; set; }
        public string Genre { get; set; } // Possibly split to each individual class and create enum?
        public DateTime ReleaseDate { get; set; }
        public ushort TimesUsed { get; set; }
        public DateTime DateLastUsed { get; set; }
    }
}