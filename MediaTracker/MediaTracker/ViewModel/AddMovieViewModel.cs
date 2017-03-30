using MediaTracker.API;
using MediaTracker.Classes;
using MediaTracker.Helper;
using System;
using System.Collections.Generic;
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

        private Movie mReturnedMovie;
        public Movie ReturnedMovie
        {
            get
            {
                if (mReturnedMovie == null)
                    return null;
                return mReturnedMovie;
            }
            set
            {
                if (mReturnedMovie != value)
                {
                    mReturnedMovie = value;
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
            if (Movies.Count == 0)
                MessageBox.Show("No Movies Found!", "Movie Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void ExecuteReturnMovie(object parameter)
        {
            ReturnedMovie = new Movie(SelectedMovie);

            string releaseDate = await bodb.GetReleaseDateRequest(SelectedMovie.Id);

            ReturnedMovie.ReleaseDate = (releaseDate != "" ? DateTime.Parse(releaseDate) : DateTime.MinValue);

            Window window = (Window)parameter;
            if (window != null)
                window.DialogResult = true;
        }
    }
}
