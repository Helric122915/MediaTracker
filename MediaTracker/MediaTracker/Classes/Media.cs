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
        public ushort PersonalRating;
        public DateTime DateAdded;
        public string Genre; // Possibly split to each individual class and create enum?
        public DateTime ReleaseDate;
        public ushort TimesUsed;
        public DateTime DateLastUsed;
        public ushort MetacriticScore; // how to get metacritic score? Might not be able to 
    }
}