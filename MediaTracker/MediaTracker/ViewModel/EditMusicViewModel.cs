using MediaTracker.Classes;
using MediaTracker.Helper;
using System;
using System.Windows;

namespace MediaTracker.ViewModel
{
    class EditMusicViewModel : ViewModelBase
    {
        Log log;

        #region Binding Properties
        private Music mEditMusic = new Music();
        public Music EditMusic
        {
            get { return mEditMusic; }
            set
            {
                if (mEditMusic != value)
                {
                    mEditMusic = value;
                    OnPropertyChanged("EditMusic");
                }
            }
        }
        #endregion

        public SimpleCommand ReturnMusic { get; private set; }
        public SimpleCommand UpdateDateUsedCommand { get; private set; }
        public SimpleCommand IncrementTimesUsedCommand { get; private set; }
        public SimpleCommand DecrementTimesUsedCommand { get; private set; }
        public SimpleCommand SetPersonalRatingCommand { get; private set; }

        public EditMusicViewModel(Music music, Log log)
        {
            EditMusic = music;
            this.log = log;

            ReturnMusic = new SimpleCommand(ExecuteReturnMusic);
            UpdateDateUsedCommand = new SimpleCommand(ExecuteUpdateDateUsedCommand);
            IncrementTimesUsedCommand = new SimpleCommand(ExecuteIncrementTimesUsedCommand);
            DecrementTimesUsedCommand = new SimpleCommand(ExecuteDecrementTimesUsedCommand);
            SetPersonalRatingCommand = new SimpleCommand(ExecuteSetPersonalRatingCommand);
        }

        #region Commands
        private void ExecuteReturnMusic(object parameter)
        {
            Window window = (Window)parameter;
            if (window != null)
                window.DialogResult = true;
        }

        private void ExecuteUpdateDateUsedCommand(object parameter)
        {
            EditMusic.DateLastUsed = DateTime.Now;
            OnPropertyChanged("EditMusic");
        }

        private void ExecuteIncrementTimesUsedCommand(object parameter)
        {
            ++EditMusic.TimesUsed;
            OnPropertyChanged("EditMusic");
        }

        private void ExecuteDecrementTimesUsedCommand(object parameter)
        {
            if (EditMusic.TimesUsed > 0)
            {
                --EditMusic.TimesUsed;
                OnPropertyChanged("EditMusic");
            }
        }

        private void ExecuteSetPersonalRatingCommand(object parameter)
        {
            ushort? rating = ushort.Parse((string)parameter);

            if (rating != null)
            {
                if (EditMusic.PersonalRating == (ushort)rating)
                    EditMusic.PersonalRating = 0;
                else
                    EditMusic.PersonalRating = (ushort)rating;
                OnPropertyChanged("EditMusic");
            }
        }
        #endregion
    }
}
