using MediaTracker.API;
using MediaTracker.Classes;
using MediaTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MediaTracker.ViewModel
{
    public class AddMusicViewModel : ViewModelBase
    {
        public Log log;
        public MusicGraph musicGraph;

        #region Binding Properties
        private string mSearchAlbum = "";
        public string SearchAlbum
        {
            get { return mSearchAlbum; }
            set
            {
                if (mSearchAlbum != value)
                {
                    mSearchAlbum = value;
                    mSearchArtist = "";
                    OnPropertyChanged("SearchArtist");
                    OnPropertyChanged("SearchAlbum");
                }
            }
        }

        private string mSearchArtist = "";
        public string SearchArtist
        {
            get { return mSearchArtist; }
            set
            {
                if (mSearchArtist != value)
                {
                    mSearchArtist = value;
                    mSearchAlbum = "";
                    OnPropertyChanged("SearchAlbum");
                    OnPropertyChanged("SearchArtist");
                }
            }
        }

        private List<MusicGraphAlbum> mAlbums = new List<MusicGraphAlbum>();
        public List<MusicGraphAlbum> Albums
        {
            get { return mAlbums; }
            set
            {
                if (mAlbums != value)
                {
                    mAlbums = value;
                    OnPropertyChanged("Albums");
                }
            }
        }

        private MusicGraphAlbum mSelectedAlbum;
        public MusicGraphAlbum SelectedAlbum
        {
            get
            {
                if (mSelectedAlbum == null)
                    return null;
                return mSelectedAlbum;
            }
            set
            {
                if (mSelectedAlbum != value)
                {
                    mSelectedAlbum = value;
                    OnPropertyChanged("SelectedAlbum");
                }
            }
        }

        private Music mReturnedMusic;
        public Music ReturnedMusic
        {
            get
            {
                if (mReturnedMusic == null)
                    return null;
                return mReturnedMusic;
            }
            set
            {
                if (mReturnedMusic != value)
                {
                    mReturnedMusic = value;
                }
            }
        }
        #endregion

        public SimpleCommand SearchMusic { get; private set; }
        public SimpleCommand ReturnMusic { get; private set; }

        public AddMusicViewModel(Log log)
        {
            this.log = log;
            musicGraph = new MusicGraph();

            SearchMusic = new SimpleCommand(ExecuteSearchMusic);
            ReturnMusic = new SimpleCommand(ExecuteReturnMusic);
        }

        private async void ExecuteSearchMusic(object paramater)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            if (SearchAlbum != "")
            {
                Albums = await musicGraph.GetAlbumRequest(SearchAlbum);
                if (Albums.Count == 0)
                    MessageBox.Show("No Albums Found!", "Album Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SearchArtist != "")
            {
                Albums = await musicGraph.GetAlbumByArtistRequest(SearchArtist);
                if (Albums.Count == 0)
                    MessageBox.Show("No Albums Found!", "Album Search Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Please enter a search into one of the fields.", "Album Search Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private async void ExecuteReturnMusic(object parameter)
        {
            ReturnedMusic = new Music(SelectedAlbum);

            List<Datum_Track> meta = await musicGraph.GetAlbumMetaData(SelectedAlbum.id);

            int sum = meta.Sum(o => o.duration);
            ReturnedMusic.Length = (sum / 60) + ":" + (sum % 60);

            ReturnedMusic.TrackList = meta.Select(o => o.title).ToList();

            Window window = (Window)parameter;
            if (window != null)
                window.DialogResult = true;
        }
    }
}
