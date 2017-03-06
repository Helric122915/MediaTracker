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
        public string Title;
        public double PersonalRating;
        public DateTime DateAdded;
        public string Genre; // Possibly split to each individual class and create enum?
        public DateTime ReleaseDate;
        public UInt16 TimesUsed;
        public DateTime DateLastUsed;
        public UInt16 MetacriticScore; // how to get metacritic score?

        public virtual void writeXML() { }
    }
}