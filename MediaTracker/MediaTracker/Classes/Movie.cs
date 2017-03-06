using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Classes
{
    public class Movie : Media
    {
        public string MPAA;
        public string Studio;
        public string IMDB;
        public string Director;
        public string Starring;

        public override void writeXML()
        {
        }
    }
}