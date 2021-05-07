using System.ComponentModel;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;

namespace Clean_CaDET.View.ViewModel
{
    public class TutoringWindowVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //TODO: Should create a ViewModel class eventually, for basic examples this will suffice.
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateContent(ChallengeEvaluationDTO content)
        {
            var title = content.ChallengeCompleted
                ? "Congratulations, you completed the challenge!"
                : "Your submission is not yet there.";
            Content = new ContentVM(title, content.ApplicableHints, content.SolutionLO);
        }
    }
}
