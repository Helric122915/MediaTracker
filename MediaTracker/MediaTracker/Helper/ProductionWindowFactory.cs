using MediaTracker.Classes;
using MediaTracker.View;
using MediaTracker.ViewModel;

namespace MediaTracker.Helper
{
    public interface IWindowFactory
    {
        Movie CreateMovieWindow(Log log);
        VideoGame CreateVideoGameWindow(Log log);
    }

    public class ProductionWindowFactory : IWindowFactory
    {
        public Movie CreateMovieWindow(Log log)
        {
            AddMovieViewModel viewModel = new AddMovieViewModel(log);

            AddMovieWindow window = new AddMovieWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return new Movie(viewModel.SelectedMovie);
            else
                return null;
        }

        public VideoGame CreateVideoGameWindow(Log log)
        {
            AddVideoGameViewModel viewModel = new AddVideoGameViewModel(log);

            AddVideoGameWindow window = new AddVideoGameWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return new VideoGame(viewModel.SelectedVideoGame);
            else
                return null;
        }
    }
}
