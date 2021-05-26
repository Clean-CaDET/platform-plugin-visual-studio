using System.Windows;
using Clean_CaDET.View.ChallengePanel.ViewModel;

namespace Clean_CaDET.View.ChallengePanel
{
    public partial class TutoringWindowControl
    {
        public ChallengeWindowVM ViewModel { get; set; }

        public TutoringWindowControl()
        {
            InitializeComponent();
            ViewModel = new ChallengeWindowVM();
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

        private void SubmitChallenge_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SubmitChallengeAsync();
        }
    }
}