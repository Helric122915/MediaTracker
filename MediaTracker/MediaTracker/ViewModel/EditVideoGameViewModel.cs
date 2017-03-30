using MediaTracker.Classes;
using MediaTracker.Helper;
using System;
using System.Windows;

namespace MediaTracker.ViewModel
{
    class EditVideoGameViewModel : ViewModelBase
    {
        Log log;

        #region Binding Properties
        private VideoGame mEditVideoGame= new VideoGame();
        public VideoGame EditVideoGame
        {
            get { return mEditVideoGame; }
            set
            {
                if (mEditVideoGame != value)
                {
                    mEditVideoGame = value;
                    OnPropertyChanged("EditVideoGame");
                }
            }
        }
        #endregion

        public SimpleCommand ReturnVideoGame { get; private set; }
        public SimpleCommand UpdateDateUsedCommand { get; private set; }
        public SimpleCommand IncrementTimesUsedCommand { get; private set; }
        public SimpleCommand DecrementTimesUsedCommand { get; private set; }
        public SimpleCommand SetPersonalRatingCommand { get; private set; }

        public EditVideoGameViewModel(VideoGame videoGame, Log log)
        {
            EditVideoGame = videoGame;
            this.log = log;

            ReturnVideoGame = new SimpleCommand(ExecuteReturnVideoGame);
            UpdateDateUsedCommand = new SimpleCommand(ExecuteUpdateDateUsedCommand);
            IncrementTimesUsedCommand = new SimpleCommand(ExecuteIncrementTimesUsedCommand);
            DecrementTimesUsedCommand = new SimpleCommand(ExecuteDecrementTimesUsedCommand);
            SetPersonalRatingCommand = new SimpleCommand(ExecuteSetPersonalRatingCommand);
        }

        #region Commands
        private void ExecuteReturnVideoGame(object parameter)
        {
            Window window = (Window)parameter;
            if (window != null)
                window.DialogResult = true;
        }

        private void ExecuteUpdateDateUsedCommand(object parameter)
        {
            EditVideoGame.DateLastUsed = DateTime.Now;
            OnPropertyChanged("EditVideoGame");
        }

        private void ExecuteIncrementTimesUsedCommand(object parameter)
        {
            ++EditVideoGame.TimesUsed;
            OnPropertyChanged("EditVideoGame");
        }

        private void ExecuteDecrementTimesUsedCommand(object parameter)
        {
            if (EditVideoGame.TimesUsed > 0)
            {
                --EditVideoGame.TimesUsed;
                OnPropertyChanged("EditVideoGame");
            }
        }

        private void ExecuteSetPersonalRatingCommand(object parameter)
        {
            ushort? rating = ushort.Parse((string)parameter);

            if (rating != null) {
                if (EditVideoGame.PersonalRating == (ushort)rating)
                    EditVideoGame.PersonalRating = 0;
                else
                    EditVideoGame.PersonalRating = (ushort)rating;
                OnPropertyChanged("EditVideoGame");
            }
        }
        #endregion
    }
}
