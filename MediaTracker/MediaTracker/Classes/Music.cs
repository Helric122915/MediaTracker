using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Classes
{
    public class Music : Media
    {
        public string RIAA;
        public string Artist;
        public string Label;
        public string Length; // possibly time span?

        public override void writeXML()
        {

        }
    }
}