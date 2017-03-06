using MediaTracker.Helper;
using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace MediaTracker.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public Log log;
        private readonly IWindowFactory mWindowFactory;

        private List<BoxOfficeMovie> mMovies = new List<BoxOfficeMovie>();
        public List<BoxOfficeMovie> Movies
        {
            get { return mMovies; }
            set
            {
                if (mMovies != value)
                {
                    mMovies = value;
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
                }
            }
        }

        #region Commanding Declaration
        public SimpleCommand AddMovieCommand { get; private set; }
        public SimpleCommand AddVideoGameCommand { get; private set; }
        #endregion

        public ViewModel(IWindowFactory windowFactory)
        {
            log = new Log("MediaTracker");
            log.LogIt("Application Started");

            mWindowFactory = windowFactory;

            AddMovieCommand = new SimpleCommand(ExecuteAddMovieCommand);
            AddVideoGameCommand = new SimpleCommand(ExecuteAddVideoGameCommand);

            // Read in all of the xml for each media type and load it into the corresponding List.
        }

        #region Commands
        private void ExecuteAddMovieCommand(object paramter)
        {
            BoxOfficeMovie result = mWindowFactory.CreateMovieWindow(log);
            InsertMovie(result);
        }

        private void ExecuteAddVideoGameCommand(object parameter)
        {
            IGDBVideoGame result = mWindowFactory.CreateVideoGameWindow(log);
            InsertVideoGame(result);
        }
        #endregion

        private void InsertMovie(BoxOfficeMovie movie)
        {
            if (movie != null)
            {
                Movies.Add(movie);

                Movies = Movies.OrderBy(o => o.Title).ToList();
            }
        }

        private void InsertVideoGame(IGDBVideoGame game)
        {
            if (game != null)
            {
                VideoGames.Add(game);

                VideoGames = VideoGames.OrderBy(o => o.name).ToList();
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

            // Save inportant values
            Environment.Exit(0);
        }
    }
}