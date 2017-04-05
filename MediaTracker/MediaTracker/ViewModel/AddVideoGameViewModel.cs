using MediaTracker.API;
using MediaTracker.Helper;
using System.Collections.Generic;
using System.Windows;
using System;
using MediaTracker.Classes;

namespace MediaTracker.ViewModel
{
    public class AddVideoGameViewModel : ViewModelBase
    {
        public Log log;
        public IGDB igdb;

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

        private List<IGDBVideoGame> mVideoGames = new List<IGDBVideoGame>();
        public List<IGDBVideoGame> VideoGames
        {
            get { return mVideoGames; }
            set
            {
                if (mVideoGames != value)
                {
                    mVideoGames = value;
                    OnPropertyChanged("VideoGames");
                }
            }

        }

        private IGDBVideoGame mSelectedVideoGame;
        public IGDBVideoGame SelectedVideoGame
        {
            get
            {
                if (mSelectedVideoGame == null)
                    return null;
                return mSelectedVideoGame;
            }
            set
            {
                if (mSelectedVideoGame != value)
                {
                    mSelectedVideoGame = value;
                    OnPropertyChanged("SelectedVideoGame");
                }
            }
        }

        private VideoGame mReturnedVideoGame;
        public VideoGame ReturnedVideoGame
        {
            get
            {
                if (mReturnedVideoGame == null)
                    return null;
                return mReturnedVideoGame;
            }
            set
            {
                if (mReturnedVideoGame != value)
                {
                    mReturnedVideoGame = value;
                }
            }
        }
        #endregion

        public SimpleCommand SearchVideoGame { get; private set; }
        public SimpleCommand ReturnVideoGame { get; private set; }

        public AddVideoGameViewModel(Log log)
        {
            this.log = log;
            igdb = new IGDB();

            SearchVideoGame = new SimpleCommand(ExecuteSearchVideoGame);
            ReturnVideoGame = new SimpleCommand(ExecuteReturnVideoGame);
        }

        private async void ExecuteSearchVideoGame(object paramater)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            if (Search != "")
            {
                VideoGames = await igdb.GetVideoGameRequest(Search);
                if (VideoGames.Count == 0)
                    MessageBox.Show("No Video Games Found!", "Video Game Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Please enter a search into one of the fields.", "Video Game Search Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private async void ExecuteReturnVideoGame(object parameter)
        {
            ReturnedVideoGame = new VideoGame(SelectedVideoGame);
            ReturnedVideoGame.Publisher = (SelectedVideoGame.publishers != null ? await igdb.GetCompanyRequest(string.Join(",", SelectedVideoGame.publishers.ToArray())) : "");
            ReturnedVideoGame.Studio = (SelectedVideoGame.developers != null ? await igdb.GetCompanyRequest(string.Join(",", SelectedVideoGame.developers.ToArray())) : "");
            ReturnedVideoGame.Genre = (SelectedVideoGame.genres != null ? await igdb.GetGenreRequest(string.Join(",", SelectedVideoGame.genres.ToArray())) : "");

            try
            {
                ReturnedVideoGame.ReleaseDate = DateTime.Parse(SelectedVideoGame.release_dates.Count > 0 ? SelectedVideoGame.release_dates[0].human : "");
            }
            catch
            {
                ReturnedVideoGame.ReleaseDate = new DateTime();
            }

            Window window = (Window)parameter;
            if (window != null)
                window.DialogResult = true;
        }
    }
}
