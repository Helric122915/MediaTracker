using MediaTracker.Helper;
using System;
using System.Windows;


namespace MediaTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel.ViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            mainViewModel = new ViewModel.ViewModel(new ProductionWindowFactory());
            this.DataContext = mainViewModel;

            Closing += mainViewModel.OnWindowClosing;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;

            MessageBox.Show("Uncaught Thread Exception", ex.Message, MessageBoxButton.OK);

            mainViewModel.log.handleException(ex);
        }
    }
}
