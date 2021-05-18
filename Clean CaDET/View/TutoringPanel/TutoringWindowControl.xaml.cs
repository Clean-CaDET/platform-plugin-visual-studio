using Clean_CaDET.Model;
using Clean_CaDET.View.TutoringPanel.ViewModel;
using System.Windows;

namespace Clean_CaDET.View.TutoringPanel
{
    public partial class TutoringWindowControl
    {
        private PlatformService _platform;
        public TutoringWindowVM ViewModel { get; set; }

        public TutoringWindowControl()
        {
            InitializeComponent();
            _platform = new PlatformService();
            ViewModel = new TutoringWindowVM();
            DataContext = ViewModel;
        }

        private void ShowHints_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Content.HintVisibility = Visibility.Visible;
            ViewModel.Content.HintButtonVisibility = Visibility.Collapsed;
        }

        private void ShowSolution_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Content.SolutionVisibility = Visibility.Visible;
            ViewModel.Content.SolutionButtonVisibility = Visibility.Collapsed;
        }
    }
}