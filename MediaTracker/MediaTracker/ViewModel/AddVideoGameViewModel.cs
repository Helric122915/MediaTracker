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
                    OnPropertyChanged("VideoGameList");
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
            VideoGames = await igdb.GetRequest(Search);
        }

        private void ExecuteReturnVideoGame(object parameter)
        {
            Window window = (Window)parameter;
            window.DialogResult = true;
        }
    }
}
