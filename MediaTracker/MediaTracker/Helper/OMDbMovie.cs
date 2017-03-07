using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public class OMDbMovie
    {
        #region Properties
        private string mSearch = "Enter Movie Title";
        public string Search
        {
            get { return mSearch; }
            set
            {
                if (mSearch != value)
                {
                    mSearch = value;
                }
            }
        }

        private string mTitle = "";
        public string Title
        {
            get { return mTitle; }
            set
            {
                if (mTitle != value)
                {
                    mTitle = value;
                }
            }
        }

        private UInt16 mYear = 0;
        public UInt16 Year
        {
            get { return mYear; }
            set
            {
                if (mYear != value)
                {
                    mYear = value;
                }
            }
        }

        private string mRunTime = "";
        public string RunTime
        {
            get { return mRunTime; }
            set
            {
                if (mRunTime != value)
                {
                    mRunTime = value;
                }
            }
        }

        private string mGenre = "";
        public string Genre
        {
            get { return mGenre; }
            set
            {
                if (mGenre != value)
                {
                    mGenre = value;
                }
            }
        }

        private string mReleased = "";
        public string Released
        {
            get { return mReleased; }
            set
            {
                if (mReleased != value)
                {
                    mReleased = value;
                }
            }
        }

        private string mDirector = "";
        public string Director
        {
            get { return mDirector; }
            set
            {
                if (mDirector != value)
                {
                    mDirector = value;
                }
            }
        }

        private string mPoster = "";
        public string Poster
        {
            get { return mPoster; }
            set
            {
                if (mPoster != value)
                {
                    mPoster = value;
                }
            }
        }

        private string mIMDbId = "";
        public string IMDbId
        {
            get { return mIMDbId; }
            set
            {
                if (mIMDbId != value)
                {
                    mIMDbId = value;
                }
            }
        }
        #endregion

        //public Movie() {}
    }
}