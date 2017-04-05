using MediaTracker.Helper;
using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using MediaTracker.Classes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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

                    SortList(MediaType.Movie);
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

                    SortList(MediaType.VideoGame);
                }
            }
        }

        private ObservableCollection<Music> mMusicList = new ObservableCollection<Music>();
        public ObservableCollection<Music> MusicList
        {
            get { return mMusicList; }
            set
            {
                if (mMusicList != value)
                {
                    mMusicList = value;
                    OnPropertyChanged("MusicList");
                }
            }
        }

        private Music mSelectedMusic;
        public Music SelectedMusic
        {
            get
            {
                if (mSelectedMusic == null)
                    return null;
                return mSelectedMusic;
            }
            set
            {
                if (mSelectedMusic != value)
                {
                    mSelectedMusic = value;
                    OnPropertyChanged("SelectedMusic");
                }
            }
        }

        private MusicSort mMusicSorting = MusicSort.Title;
        public MusicSort MusicSorting
        {
            get
            {
                if (mMusicSorting == MusicSort.None)
                    mMusicSorting = MusicSort.Title;
                return mMusicSorting;
            }
            set
            {
                if (mMusicSorting != value)
                {
                    mMusicSorting = value;
                    OnPropertyChanged("MusicSorting");

                    SortList(MediaType.Music);
                }
            }
        }
        #endregion

        #region Commanding Declaration
        public SimpleCommand AddMovieCommand { get; private set; }
        public SimpleCommand EditMovieCommand { get; private set; }
        public SimpleCommand RandomMovieCommand { get; private set; }
        public SimpleCommand RemoveMovieCommand { get; private set; }
        public SimpleCommand AddVideoGameCommand { get; private set; }
        public SimpleCommand EditVideoGameCommand { get; private set; }
        public SimpleCommand RandomVideoGameCommand { get; private set; }
        public SimpleCommand RemoveVideoGameCommand { get; private set; }
        public SimpleCommand AddMusicCommand { get; private set; }
        public SimpleCommand EditMusicCommand { get; private set; }
        public SimpleCommand RandomMusicCommand { get; private set; }
        public SimpleCommand RemoveMusicCommand { get; private set; }
        #endregion

        public ViewModel(IWindowFactory windowFactory)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            log = new Log("MediaTracker");
            log.LogIt("Application Started");

            WindowFactory = windowFactory;

            AddMovieCommand = new SimpleCommand(ExecuteAddMovieCommand);
            EditMovieCommand = new SimpleCommand(ExecuteEditMovieCommand);
            RandomMovieCommand = new SimpleCommand(ExecuteRandomMovieCommand);
            RemoveMovieCommand = new SimpleCommand(ExecuteRemoveMovieCommand);
            AddVideoGameCommand = new SimpleCommand(ExecuteAddVideoGameCommand);
            EditVideoGameCommand = new SimpleCommand(ExecuteEditVideoGameCommand);
            RandomVideoGameCommand = new SimpleCommand(ExecuteRandomVideoGameCommand);
            RemoveVideoGameCommand = new SimpleCommand(ExecuteRemoveVideoGameCommand);
            AddMusicCommand = new SimpleCommand(ExecuteAddMusicCommand);
            EditMusicCommand = new SimpleCommand(ExecuteEditMusicCommand);
            RandomMusicCommand = new SimpleCommand(ExecuteRandomMusicCommand);
            RemoveMusicCommand = new SimpleCommand(ExecuteRemoveMusicCommand);

            // Read in all of the xml for each media type and load it into the corresponding List.
            ReadXML readXML = new ReadXML();

            if (File.Exists(currentDirectory + "/MovieList.xml"))
                MovieList = new ObservableCollection<Movie>(readXML.ReadMovie(Directory.GetCurrentDirectory() + "/MovieList.xml").OrderBy(o => o.Title));
            if (File.Exists(currentDirectory + "/VideoGameList.xml"))
                VideoGameList =  new ObservableCollection<VideoGame>(readXML.ReadVideoGame(Directory.GetCurrentDirectory() + "/VideoGameList.xml").OrderBy(o => o.Title));
            if (File.Exists(currentDirectory + "/AlbumList.xml"))
                MusicList = new ObservableCollection<Music>(readXML.ReadMusic(Directory.GetCurrentDirectory() + "/AlbumList.xml").OrderBy(o => o.Title));

            // Load the Sort settings.
            MovieSort tempMovie = MovieSort.None;
            Enum.TryParse(Properties.Settings.Default.MovieSort, out tempMovie);
            MovieSorting = tempMovie;

            VideoGameSort tempVideoGame = VideoGameSort.None;
            Enum.TryParse(Properties.Settings.Default.VideoGameSort, out tempVideoGame);
            VideoGameSorting = tempVideoGame;

            MusicSort tempMusic = MusicSort.None;
            Enum.TryParse(Properties.Settings.Default.AlbumSort, out tempMusic);
            MusicSorting = tempMusic;
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
                {
                    MovieList[MovieList.IndexOf(SelectedMovie)] = temp;
                    SortList(MediaType.Movie);
                }
            }
        }

        private void ExecuteRandomMovieCommand(object parameter)
        {
            List<int> movieRank = new List<int>();
            Random num = new Random(DateTime.Now.Second);

            long earliestWatch = MovieList.Min(o => o.DateLastUsed.Ticks);
            long watchSpan = MovieList.Max(o => o.DateLastUsed.Ticks) - earliestWatch;

            int leastWatch = MovieList.Min(o => o.TimesUsed);
            int watchAmount = MovieList.Max(o => o.TimesUsed) - leastWatch;

            foreach (Movie movie in MovieList)
            {
                int dateValue, watchValue;

                if (movie.DateLastUsed.Ticks == earliestWatch)
                    dateValue = 5;
                else if (movie.DateLastUsed.Ticks < watchSpan * 0.25)
                    dateValue = 4;
                else if (movie.DateLastUsed.Ticks < watchSpan * 0.5)
                    dateValue = 3;
                else if (movie.DateLastUsed.Ticks < watchSpan * 0.75)
                    dateValue = 2;
                else if (movie.DateLastUsed.Ticks < watchSpan)
                    dateValue = 1;
                else
                    dateValue = 0;

                if (movie.TimesUsed == leastWatch)
                    watchValue = 5;
                else if (movie.TimesUsed < watchAmount * 0.25)
                    watchValue = 4;
                else if (movie.TimesUsed < watchAmount * 0.5)
                    watchValue = 3;
                else if (movie.TimesUsed < watchAmount * 0.75)
                    watchValue = 2;
                else if (movie.TimesUsed < watchAmount)
                    watchValue = 1;
                else
                    watchValue = 0;

                movieRank.Add(num.Next(0, 5) + movie.PersonalRating + dateValue + watchValue); // Computes the priority value of each movie.
            }

            // Sets the selected movie to the index of the max ranked movie.
            DialogResult result = DialogResult.None;
            while (result != DialogResult.Yes && movieRank.Max() != 0 && result != DialogResult.Cancel)
            {
                SelectedMovie = MovieList[movieRank.IndexOf(movieRank.Max())];
                result = MessageBox.Show("Would you like to watch: " + SelectedMovie.Title + "?", "Random Movie", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    movieRank[movieRank.IndexOf(movieRank.Max())] = 0;
            }

            if (result == DialogResult.Yes)
            {
                SelectedMovie.DateLastUsed = DateTime.Now;
                SelectedMovie.TimesUsed = ++SelectedMovie.TimesUsed;
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
                {
                    VideoGameList[VideoGameList.IndexOf(SelectedVideoGame)] = temp;
                    SortList(MediaType.VideoGame);
                }
            }
        }

        public void ExecuteRandomVideoGameCommand(object parameter)
        {
            List<int> videoGameRank = new List<int>();
            Random num = new Random(DateTime.Now.Second);

            long earliestPlay = VideoGameList.Min(o => o.DateLastUsed.Ticks);
            long playSpan = VideoGameList.Max(o => o.DateLastUsed.Ticks) - earliestPlay;

            int leastPlay = VideoGameList.Min(o => o.TimesUsed);
            int playAmount = VideoGameList.Max(o => o.TimesUsed) - leastPlay;

            foreach (VideoGame videoGame in VideoGameList)
            {
                int dateValue, playValue;

                if (videoGame.DateLastUsed.Ticks == earliestPlay)
                    dateValue = 5;
                else if (videoGame.DateLastUsed.Ticks < playSpan * 0.25)
                    dateValue = 4;
                else if (videoGame.DateLastUsed.Ticks < playSpan * 0.5)
                    dateValue = 3;
                else if (videoGame.DateLastUsed.Ticks < playSpan * 0.75)
                    dateValue = 2;
                else if (videoGame.DateLastUsed.Ticks < playSpan)
                    dateValue = 1;
                else
                    dateValue = 0;

                if (videoGame.TimesUsed == leastPlay)
                    playValue = 5;
                else if (videoGame.TimesUsed < playAmount * 0.25)
                    playValue = 4;
                else if (videoGame.TimesUsed < playAmount * 0.5)
                    playValue = 3;
                else if (videoGame.TimesUsed < playAmount * 0.75)
                    playValue = 2;
                else if (videoGame.TimesUsed < playAmount)
                    playValue = 1;
                else
                    playValue = 0;

                videoGameRank.Add(num.Next(0, 5) + videoGame.PersonalRating + dateValue + playValue); // Computes the priority value of each movie.
            }

            // Sets the selected movie to the index of the max ranked movie.
            DialogResult result = DialogResult.None;
            while (result != DialogResult.Yes && videoGameRank.Max() != 0 && result == DialogResult.Cancel)
            {
                SelectedVideoGame = VideoGameList[videoGameRank.IndexOf(videoGameRank.Max())];
                result = MessageBox.Show("Would you like to play: " + SelectedVideoGame.Title + "?", "Random Video Game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    videoGameRank[videoGameRank.IndexOf(videoGameRank.Max())] = 0;
            }

            if (result == DialogResult.Yes)
            {
                SelectedVideoGame.DateLastUsed = DateTime.Now;
                SelectedVideoGame.TimesUsed = ++SelectedVideoGame.TimesUsed;
            }
        }

        public void ExecuteRemoveVideoGameCommand(object parameter)
        {
            VideoGameList.Remove(SelectedVideoGame);
            OnPropertyChanged("VideoGameList");
        }

        public void ExecuteAddMusicCommand(object parameter)
        {
            Music result = WindowFactory.CreateMusicWindow(log);

            if (result != null)
            {
                if (MusicList.Where(o => o.Title == result.Title).Count() < 1)
                {
                    MusicList.Add(result);
                    SortList(MediaType.Music);
                }
                else
                    MessageBox.Show("Album Title Already in List", "Error Adding Album", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExecuteEditMusicCommand(object parameter)
        {
            if (SelectedMusic != null)
            {
                Music temp = ObjectCopier.CloneJson(SelectedMusic);

                bool result = WindowFactory.EditMusicWindow(temp, log);

                if (result)
                {
                    MusicList[MusicList.IndexOf(SelectedMusic)] = temp;
                    SortList(MediaType.Music);
                }
            }
        }

        public void ExecuteRandomMusicCommand(object parameter)
        {
            List<int> musicRank = new List<int>();
            Random num = new Random(DateTime.Now.Second);

            long earliestListen = MovieList.Min(o => o.DateLastUsed.Ticks);
            long listenSpan = MovieList.Max(o => o.DateLastUsed.Ticks) - earliestListen;

            int leastListen = MovieList.Min(o => o.TimesUsed);
            int listenAmount = MovieList.Max(o => o.TimesUsed) - leastListen;

            foreach (Music music in MusicList)
            {
                int dateValue, listenValue;

                if (music.DateLastUsed.Ticks == earliestListen)
                    dateValue = 5;
                else if (music.DateLastUsed.Ticks < listenSpan * 0.25)
                    dateValue = 4;
                else if (music.DateLastUsed.Ticks < listenSpan * 0.5)
                    dateValue = 3;
                else if (music.DateLastUsed.Ticks < listenSpan * 0.75)
                    dateValue = 2;
                else if (music.DateLastUsed.Ticks < listenSpan)
                    dateValue = 1;
                else
                    dateValue = 0;

                if (music.TimesUsed == leastListen)
                    listenValue = 5;
                else if (music.TimesUsed < listenAmount * 0.25)
                    listenValue = 4;
                else if (music.TimesUsed < listenAmount * 0.5)
                    listenValue = 3;
                else if (music.TimesUsed < listenAmount * 0.75)
                    listenValue = 2;
                else if (music.TimesUsed < listenAmount)
                    listenValue = 1;
                else
                    listenValue = 0;

                musicRank.Add(num.Next(0, 5) + music.PersonalRating + dateValue + listenValue); // Computes the priority value of each movie.
            }

            // Sets the selected album to the index of the max ranked movie.
            DialogResult result = DialogResult.None;
            while (result != DialogResult.Yes && musicRank.Max() != 0 && result != DialogResult.Cancel)
            {
                SelectedMusic = MusicList[musicRank.IndexOf(musicRank.Max())];
                result = MessageBox.Show("Would you like to listen to: " + SelectedMusic.Title + "?", "Random Album", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    musicRank[musicRank.IndexOf(musicRank.Max())] = 0;
            }

            if (result == DialogResult.Yes)
            {
                SelectedMusic.DateLastUsed = DateTime.Now;
                SelectedMusic.TimesUsed = ++SelectedMusic.TimesUsed;
            }
        }

        public void ExecuteRemoveMusicCommand(object parameter)
        {
            MusicList.Remove(SelectedMusic);
            OnPropertyChanged("MusicList");
        }
        #endregion

        #region Helper Functions
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
                            case MovieSort.LeastRecentlyUsed:
                                MovieList = new ObservableCollection<Movie>(MovieList.OrderBy(o => o.DateLastUsed).ToList());
                                break;
                            case MovieSort.MostRecentlyUsed:
                                MovieList = new ObservableCollection<Movie>(MovieList.OrderByDescending(o => o.DateLastUsed).ToList());
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
                            case VideoGameSort.LeastRecentlyUsed:
                                VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderBy(o => o.DateLastUsed).ToList());
                                break;
                            case VideoGameSort.MostRecentlyUsed:
                                VideoGameList = new ObservableCollection<VideoGame>(VideoGameList.OrderByDescending(o => o.DateLastUsed).ToList());
                                break;
                        }
                        break;
                    }
                case MediaType.Music:
                    {
                        switch (MusicSorting)
                        {
                            case MusicSort.Title:
                                MusicList = new ObservableCollection<Music>(MusicList.OrderBy(o => o.Title).ToList());
                                break;
                            case MusicSort.PersonalRating:
                                MusicList = new ObservableCollection<Music>(MusicList.OrderByDescending(o => o.PersonalRating).ToList());
                                break;
                            case MusicSort.LeastRecentlyUsed:
                                MusicList = new ObservableCollection<Music>(MusicList.OrderBy(o => o.DateLastUsed).ToList());
                                break;
                            case MusicSort.MostRecentlyUsed:
                                MusicList = new ObservableCollection<Music>(MusicList.OrderByDescending(o => o.DateLastUsed).ToList());
                                break;
                            case MusicSort.None:
                                break;
                        }
                    }
                    break;
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
            //SchemaValidation validation = new SchemaValidation(log);

            writeXML.WriteMovie(MovieList.ToList(), Directory.GetCurrentDirectory() + @"\MovieList.xml");
            //validation.validate(Directory.GetCurrentDirectory() + @"\MovieList.xml", MediaType.Movie, true);

            writeXML.WriteVideoGame(VideoGameList.ToList(), Directory.GetCurrentDirectory() + @"\VideoGameList.xml");
            //validation.validate(Directory.GetCurrentDirectory() + @"\VideoGameList.xml", MediaType.VideoGame, true);

            writeXML.WriteMusic(MusicList.ToList(), Directory.GetCurrentDirectory() + @"\AlbumList.xml");
            //validation.validate(Directory.GetCurrentDirectory() + @"\AlbumList.xml", MediaType.Music, true);

            // Store the Sort settings.
            Properties.Settings.Default.MovieSort = MovieSorting.ToString();
            Properties.Settings.Default.VideoGameSort = VideoGameSorting.ToString();
            Properties.Settings.Default.AlbumSort = MusicSorting.ToString();
            Properties.Settings.Default.Save();

            Environment.Exit(0);
        }
        #endregion
    }
}