using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class Log
    {
        private int currentDay;
        private StreamWriter logSW;
        private string myLogFileName;

        public Log(string logFileName)
        {
            myLogFileName = logFileName;
        }

        public void Open()
        {
            openLogFile(myLogFileName);
        }

        public void Close()
        {
            closeLogFile();
        }

        private void openLogFile(string logFileName)
        {
            currentDay = DateTime.Now.Day;

            string actualFileName = string.Format("{0}_{1}.txt", myLogFileName, currentDay);

            logSW = new StreamWriter(actualFileName, true);
        }

        private void closeLogFile()
        {
            logSW.Close();
        }

        public void LogIt(string textToLog)
        {
            this.Open();
            DateTime now = DateTime.Now;
            if ((int)now.Day != currentDay)
            {
                closeLogFile();
                openLogFile(myLogFileName);
            }
            logSW.WriteLine(String.Format("{0:MM/dd/yyyy HH:mm:ss} : {1}", now, textToLog));
            this.Close();
        }

        public void handleException(Exception e)
        {
            this.LogIt(e.Message);
            if (e.InnerException != null)
            {
                this.LogIt(e.InnerException.Message);
            }
        }
    }
}