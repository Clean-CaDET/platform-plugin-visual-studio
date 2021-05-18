using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.ComponentModel;

namespace Clean_CaDET.View.TutoringPanel.ViewModel
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
            Content = new ContentVM(content.ChallengeCompleted, content.ApplicableHints, content.SolutionLO);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
