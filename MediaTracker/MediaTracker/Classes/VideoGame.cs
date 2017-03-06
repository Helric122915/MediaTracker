using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Classes
{
    public class VideoGame : Media
    {
        public string ESRB;
        public string Platform;
        public string Publisher;
        public string Studio;

        public override void writeXML()
        {

        }
    }
}