using MediaTracker.Classes;
using MediaTracker.View;
using MediaTracker.ViewModel;

namespace MediaTracker.Helper
{
    public interface IWindowFactory
    {
        Movie CreateMovieWindow(Log log);
        bool EditMovieWindow(Movie movie, Log log);
        VideoGame CreateVideoGameWindow(Log log);
        bool EditVideoGameWindow(VideoGame videoGame, Log log);
        Music CreateMusicWindow(Log log);
        bool EditMusicWindow(Music music, Log log);
    }

    public class ProductionWindowFactory : IWindowFactory
    {
        public Movie CreateMovieWindow(Log log)
        {
            AddMovieViewModel viewModel = new AddMovieViewModel(log);

            AddMovieWindow window = new AddMovieWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return viewModel.ReturnedMovie;
            else
                return null;
        }

        public bool EditMovieWindow(Movie movie, Log log)
        {
            EditMovieViewModel viewModel = new EditMovieViewModel(movie, log);

            EditMovieWindow window = new EditMovieWindow { DataContext = viewModel };

            return (bool)window.ShowDialog();
        }

        public VideoGame CreateVideoGameWindow(Log log)
        {
            AddVideoGameViewModel viewModel = new AddVideoGameViewModel(log);

            AddVideoGameWindow window = new AddVideoGameWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return viewModel.ReturnedVideoGame;
            else
                return null;
        }

        public bool EditVideoGameWindow(VideoGame videoGame, Log log)
        {
            EditVideoGameViewModel viewModel = new EditVideoGameViewModel(videoGame, log);

            EditVideoGameWindow window = new EditVideoGameWindow { DataContext = viewModel };

            return (bool)window.ShowDialog();
        }

        public Music CreateMusicWindow(Log log)
        {
            AddMusicViewModel viewModel = new AddMusicViewModel(log);

            AddMusicWindow window = new AddMusicWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return viewModel.ReturnedMusic;
            else
                return null;
        }

        public bool EditMusicWindow(Music music, Log log)
        {
            EditMusicViewModel viewModel = new EditMusicViewModel(music, log);

            EditMusicWindow window = new EditMusicWindow { DataContext = viewModel };

            return (bool)window.ShowDialog();
        }
    }
}
