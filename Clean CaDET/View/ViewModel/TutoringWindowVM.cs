using System.ComponentModel;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;

namespace Clean_CaDET.View.ViewModel
{
    public class TutoringWindowVM: INotifyPropertyChanged
    {
        private ContentVM _content;
        public ContentVM Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public TutoringWindowVM()
        {
            Content = new ContentVM();
        }

        public void UpdateContent(ChallengeEvaluationDTO content)
        {
            var title = content.ChallengeCompleted
                ? "Congratulations, you completed the challenge!"
                : "Your submission is not yet there.";
            Content = new ContentVM(title, content.ApplicableHints, content.SolutionLO);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
