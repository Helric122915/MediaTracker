using MediaTracker.Classes;
using MediaTracker.Helper;
using System;
using System.Windows;

namespace MediaTracker.ViewModel
{
    class EditMovieViewModel : ViewModelBase
    {
        Log log;

        #region Binding Properties
        private Movie mEditMovie = new Movie();
        public Movie EditMovie
        {
            get { return mEditMovie; }
            set
            {
                if (mEditMovie != value)
                {
                    mEditMovie = value;
                    OnPropertyChanged("EditMovie");
                }
            }
        }
        #endregion

        public SimpleCommand ReturnMovie { get; private set; }
        public SimpleCommand UpdateDateUsedCommand { get; private set; }
        public SimpleCommand IncrementTimesUsedCommand { get; private set; }
        public SimpleCommand DecrementTimesUsedCommand { get; private set; }
        public SimpleCommand SetPersonalRatingCommand { get; private set; }

        public EditMovieViewModel(Movie movie, Log log)
        {
            EditMovie = movie;
            this.log = log;

            ReturnMovie = new SimpleCommand(ExecuteReturnMovie);
            UpdateDateUsedCommand = new SimpleCommand(ExecuteUpdateDateUsedCommand);
            IncrementTimesUsedCommand = new SimpleCommand(ExecuteIncrementTimesUsedCommand);
            DecrementTimesUsedCommand = new SimpleCommand(ExecuteDecrementTimesUsedCommand);
            SetPersonalRatingCommand = new SimpleCommand(ExecuteSetPersonalRatingCommand);
        }

        #region Commands
        private void ExecuteReturnMovie(object parameter)
        {
            Window window = (Window)parameter;
            window.DialogResult = true;
        }

        private void ExecuteUpdateDateUsedCommand(object parameter)
        {
            EditMovie.DateLastUsed = DateTime.Now;
            OnPropertyChanged("EditMovie");
        }

        private void ExecuteIncrementTimesUsedCommand(object parameter)
        {
            ++EditMovie.TimesUsed;
            OnPropertyChanged("EditMovie");
        }

        private void ExecuteDecrementTimesUsedCommand(object parameter)
        {
            if (EditMovie.TimesUsed > 0)
            {
                --EditMovie.TimesUsed;
                OnPropertyChanged("EditMovie");
            }
        }

        private void ExecuteSetPersonalRatingCommand(object parameter)
        {
            ushort? rating = ushort.Parse((string)parameter);

            if (rating != null)
            {
                if (EditMovie.PersonalRating == (ushort)rating)
                    EditMovie.PersonalRating = 0;
                else
                    EditMovie.PersonalRating = (ushort)rating;
                OnPropertyChanged("EditMovie");
            }
        }
        #endregion
    }
}
