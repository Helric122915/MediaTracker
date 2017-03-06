using MediaTracker.View;
using MediaTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public interface IWindowFactory
    {
        BoxOfficeMovie CreateMovieWindow(Log log);
        IGDBVideoGame CreateVideoGameWindow(Log log);
    }

    public class ProductionWindowFactory : IWindowFactory
    {
        public BoxOfficeMovie CreateMovieWindow(Log log)
        {
            AddMovieViewModel viewModel = new AddMovieViewModel(log);

            AddMovieWindow window = new AddMovieWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return viewModel.SelectedMovie;
            else
                return null;
        }

        public IGDBVideoGame CreateVideoGameWindow(Log log)
        {
            AddVideoGameViewModel viewModel = new AddVideoGameViewModel(log);

            AddVideoGameWindow window = new AddVideoGameWindow { DataContext = viewModel };

            bool ReturnResult = (bool)window.ShowDialog();

            if (ReturnResult)
                return viewModel.SelectedVideoGame;
            else
                return null;
        }
    }
}
