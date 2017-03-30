using MediaTracker.Helper;
using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using MediaTracker.Classes;
using System.Collections.ObjectModel;

namespace MediaTracker.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public Log log;
        private readonly IWindowFactory WindowFactory;

        #region Bindable Properties
        private ObservableCollection<Movie> mMovieList = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> MovieList
        {
            get { return mMovieList; }
            set
            {
                if (mMovieList != value)
                {
                    mMovieList = value;
                    OnPropertyChanged("MovieList");
                }
            }
        }

        private Movie mSelectedMovie;
        public Movie SelectedMovie
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

        private MovieSort mMovieSorting = MovieSort.Title;
        public MovieSort MovieSorting
        {
            get
            {
                if (mMovieSorting == MovieSort.None)
                    MovieSorting = MovieSort.Title;
                return mMovieSorting;
            }
            set
            {
                if (mMovieSorting != value)
                {
                    mMovieSorting = value;
                    OnPropertyChanged("MovieSorting");

                    switch (value)
                    {
                        case MovieSort.Title:
                            MovieList = new ObservableCollection<Movie>(MovieList.OrderBy(o => o.Title).ToList());
                            break;
                        case MovieSort.PersonalRating:
                            MovieList = new ObservableCollection<Movie>(MovieList.OrderByDescending(o => o.PersonalRating).ToList());
                            break;
                        case MovieSort.Director:
                            MovieList = new ObservableCollection<Movie>(MovieList.OrderBy(o => o.Director).ToList());
                            break;
                    }
                }
            }
        }

        private ObservableCollection<VideoGame> mVideoGameList = new ObservableCollection<VideoGame>();
        public ObservableCollection<VideoGame> VideoGameList
        {
            get { return mVideoGameList; }
            set
            {
                if (mVideoGameList != value)
                {
                    mVideoGameList = value;
                    OnPropertyChanged("VideoGameList");
                }
            }
        }

        private VideoGame mSelectedVideoGame;
        public VideoGame SelectedVideoGame
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

        private VideoGameSort mVideoGameSorting = VideoGameSort.Title;
        public VideoGameSort VideoGameSorting
        {
            get
            {
                if (mVideoGameSorting == VideoGameSort.None)
                    VideoGameSorting = VideoGameSort.Title;
                return mVideoGameSorting;
            }
            set
            {
                if (mVideoGameSorting != value)
                {
                    mVideoGameSorting = value;
                    OnPropertyChanged("VideoGameSorting");

                    switch (value)
                    {
                        case VideoGameSort.Title:
                            VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderBy(o => o.Title).ToList());
                            break;
                        case VideoGameSort.PersonalRating:
                            VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderByDescending(o => o.PersonalRating).ToList());
                            break;
                        case VideoGameSort.ESRB:
                            VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderBy(o => o.ESRB).ToList());
                            break;
                    }
                }
            }
        }
        #endregion

        #region Commanding Declaration
        public SimpleCommand AddMovieCommand { get; private set; }
        public SimpleCommand EditMovieCommand { get; private set; }
        public SimpleCommand RemoveMovieCommand { get; private set; }
        public SimpleCommand AddVideoGameCommand { get; private set; }
        public SimpleCommand EditVideoGameCommand { get; private set; }
        public SimpleCommand RemoveVideoGameCommand { get; private set; }
        #endregion

        public ViewModel(IWindowFactory windowFactory)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            log = new Log("MediaTracker");
            log.LogIt("Application Started");

            WindowFactory = windowFactory;

            AddMovieCommand = new SimpleCommand(ExecuteAddMovieCommand);
            EditMovieCommand = new SimpleCommand(ExecuteEditMovieCommand);
            RemoveMovieCommand = new SimpleCommand(ExecuteRemoveMovieCommand);
            AddVideoGameCommand = new SimpleCommand(ExecuteAddVideoGameCommand);
            EditVideoGameCommand = new SimpleCommand(ExecuteEditVideoGameCommand);
            RemoveVideoGameCommand = new SimpleCommand(ExecuteRemoveVideoGameCommand);

            // Read in all of the xml for each media type and load it into the corresponding List.
            ReadXML readXML = new ReadXML();

            if (File.Exists(currentDirectory + "/MovieList.xml"))
                MovieList = new ObservableCollection<Movie>(readXML.ReadMovie(Directory.GetCurrentDirectory() + "/MovieList.xml").OrderBy(o => o.Title));
            if (File.Exists(currentDirectory + "/VideoGameList.xml"))
                VideoGameList =  new ObservableCollection<VideoGame>(readXML.ReadVideoGame(Directory.GetCurrentDirectory() + "/VideoGameList.xml").OrderBy(o => o.Title));
        }

        #region Commands
        private void ExecuteAddMovieCommand(object parameter)
        {
            Movie result = WindowFactory.CreateMovieWindow(log);

            if (result != null)
            {
                if (MovieList.Where(o => o.Title == result.Title).Count() < 1)
                {
                    MovieList.Add(result);
                    SortList(MediaType.Movie);
                }
                else
                    MessageBox.Show("Movie Title Already in List", "Error Adding Movie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteEditMovieCommand(object parameter)
        {
            if (SelectedMovie != null)
            {
                Movie temp = ObjectCopier.CloneJson(SelectedMovie);

                bool result = WindowFactory.EditMovieWindow(temp, log);

                if (result)
                    MovieList[MovieList.IndexOf(SelectedMovie)] = ObjectCopier.CloneJson(temp);
            }
        }

        private void ExecuteRemoveMovieCommand(object parameter)
        {
            MovieList.Remove(SelectedMovie);
            OnPropertyChanged("MovieList");
        }

        private void ExecuteAddVideoGameCommand(object parameter)
        {
            VideoGame result = WindowFactory.CreateVideoGameWindow(log);

            if (result != null)
            {
                if (VideoGameList.Where(o => o.Title == result.Title).Count() < 1)
                {
                    VideoGameList.Add(result);
                    SortList(MediaType.VideoGame);
                }
                else
                    MessageBox.Show("Video Game Title Already in List", "Error Adding Video Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExecuteEditVideoGameCommand(object parameter)
        {
            if (SelectedVideoGame != null)
            {
                VideoGame temp = ObjectCopier.CloneJson(SelectedVideoGame);

                bool result = WindowFactory.EditVideoGameWindow(temp, log);

                if (result)
                    VideoGameList[VideoGameList.IndexOf(SelectedVideoGame)] = ObjectCopier.CloneJson(temp);
            }
        }

        public void ExecuteRemoveVideoGameCommand(object parameter)
        {
            VideoGameList.Remove(SelectedVideoGame);
            OnPropertyChanged("VideoGameList");
        }
        #endregion

        private void SortList(MediaType media)
        {
            switch (media)
            {
                case MediaType.Movie:
                    {
                        switch (MovieSorting)
                        {
                            case MovieSort.Title:
                                MovieList = new ObservableCollection<Movie>(MovieList.OrderBy(o => o.Title).ToList());
                                break;
                            case MovieSort.PersonalRating:
                                MovieList = new ObservableCollection<Movie>(MovieList.OrderByDescending(o => o.PersonalRating).ToList());
                                break;
                            case MovieSort.Director:
                                MovieList = new ObservableCollection<Movie>(MovieList.OrderBy(o => o.Director).ToList());
                                break;
                        }
                        break;
                    }
                case MediaType.VideoGame:
                    {
                        switch (VideoGameSorting)
                        {
                            case VideoGameSort.Title:
                                VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderBy(o => o.Title).ToList());
                                break;
                            case VideoGameSort.PersonalRating:
                                VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderByDescending(o => o.PersonalRating).ToList());
                                break;
                            case VideoGameSort.ESRB:
                                VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderBy(o => o.ESRB).ToList());
                                break;
                        }
                        break;
                    }
                case MediaType.Music: break;
            }
        }

        private void handleException(Exception e, bool withMessage)
        {
            if (withMessage)
            {
                MessageBox.Show("Error", "The Error: " + e.Message + " Occured." + Environment.NewLine
                                + "The Log can be found at: " + Environment.NewLine
                                + Directory.GetCurrentDirectory(), MessageBoxButtons.OK);
            }
            log.handleException(e);
        }

        internal void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // Sweep through each list of media and write all of the files to xml for persistant storage.
            WriteXML writeXML = new WriteXML();
            writeXML.WriteMovie(MovieList.ToList(), Directory.GetCurrentDirectory() + "/MovieList.xml");
            writeXML.WriteVideoGame(VideoGameList.ToList(), Directory.GetCurrentDirectory() + "/VideoGameList.xml");
            //writeXML.WriteMusic(MusicList, Directory.GetCurrentDirectory() + "/MusicList.xml");
            Environment.Exit(0);
        }
    }
}