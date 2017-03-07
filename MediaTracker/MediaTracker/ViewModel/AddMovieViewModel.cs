using MediaTracker.API;
using MediaTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;
using unirest_net.http;
using System.Windows.Input;
using System.Windows;

namespace MediaTracker.ViewModel
{
    public class AddMovieViewModel : ViewModelBase
    {
        public Log log;
        public BoxOfficeDB bodb;

        #region Binding Properties
        private string mSearch = "";
        public string Search
        {
            get { return mSearch; }
            set
            {
                if (mSearch != value)
                {
                    mSearch = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        private List<BoxOfficeMovie> mMovies = new List<BoxOfficeMovie>();
        public List<BoxOfficeMovie> Movies
        {
            get { return mMovies; }
            set
            {
                if (mMovies != value)
                {
                    mMovies = value;
                    OnPropertyChanged("Movies");
                }
            }

        }

        private BoxOfficeMovie mSelectedMovie;
        public BoxOfficeMovie SelectedMovie
        {
            get
            {
                if (mSelectedMovie == null)
                    return null;
                return mSelectedMovie;
            }
            set
            {
                if (mSelectedMovie != value)
                {
                    mSelectedMovie = value;
                    OnPropertyChanged("SelectedMovie");
                }
            }
        }
        #endregion

        public SimpleCommand SearchMovie { get; private set; }
        public SimpleCommand ReturnMovie { get; private set; }

        public AddMovieViewModel(Log log)
        {
            this.log = log;
            bodb = new BoxOfficeDB();

            SearchMovie = new SimpleCommand(ExecuteSearchMovie);
            ReturnMovie = new SimpleCommand(ExecuteReturnMovie);
        }

        private async void ExecuteSearchMovie(object paramater)
        {
            Movies = await bodb.GetRequest(Search);
        }

        private void ExecuteReturnMovie(object parameter)
        {
            Window window = (Window)parameter;
            window.DialogResult = true;
        }
    }
}
