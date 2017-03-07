using MediaTracker.Helper;
using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using MediaTracker.Classes;

namespace MediaTracker.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public Log log;
        private readonly IWindowFactory mWindowFactory;

        private List<Movie> mMovieList = new List<Movie>();
        public List<Movie> MovieList
        {
            get { return mMovieList; }
            set
            {
                if (mMovieList != value)
                {
                    mMovieList = value;
                }
            }
        }

        private List<VideoGame> mVideoGameList = new List<VideoGame>();
        public List<VideoGame> VideoGameList
        {
            get { return mVideoGameList; }
            set
            {
                if (mVideoGameList != value)
                {
                    mVideoGameList = value;
                }
            }
        }

        #region Commanding Declaration
        public SimpleCommand AddMovieCommand { get; private set; }
        public SimpleCommand AddVideoGameCommand { get; private set; }
        #endregion

        public ViewModel(IWindowFactory windowFactory)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            log = new Log("MediaTracker");
            log.LogIt("Application Started");

            mWindowFactory = windowFactory;

            AddMovieCommand = new SimpleCommand(ExecuteAddMovieCommand);
            AddVideoGameCommand = new SimpleCommand(ExecuteAddVideoGameCommand);

            // Read in all of the xml for each media type and load it into the corresponding List.
            ReadXML readXML = new ReadXML();

            if (File.Exists(currentDirectory + "/MovieList.xml"))
                MovieList = readXML.ReadMovie(Directory.GetCurrentDirectory() + "/MovieList.xml");
            if (File.Exists(currentDirectory + "/VideoGameList.xml"))
                VideoGameList = readXML.ReadVideoGame(Directory.GetCurrentDirectory() + "/VideoGameList.xml");
        }

        #region Commands
        private void ExecuteAddMovieCommand(object paramter)
        {
            Movie result = mWindowFactory.CreateMovieWindow(log);
            InsertMovie(result);
        }

        private void ExecuteAddVideoGameCommand(object parameter)
        {
            VideoGame result = mWindowFactory.CreateVideoGameWindow(log);
            InsertVideoGame(result);
        }
        #endregion

        private void InsertMovie(Movie movie)
        {
            if (movie != null)
            {
                MovieList.Add(movie);

                //Movies.Sort((x, y) => String.Compare(x.Title, y.Title));
                MovieList = MovieList.OrderBy(o => o.Title).ToList();
            }
        }

        private void InsertVideoGame(VideoGame game)
        {
            if (game != null)
            {
                VideoGameList.Add(game);

                VideoGameList = VideoGameList.OrderBy(o => o.Title).ToList();
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
            writeXML.WriteMovie(MovieList, Directory.GetCurrentDirectory() + "/MovieList.xml");

            // Save inportant values
            Environment.Exit(0);
        }
    }
}