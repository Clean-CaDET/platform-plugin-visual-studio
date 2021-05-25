using Clean_CaDET.Model;
using System.ComponentModel;

namespace Clean_CaDET.View.TutoringPanel.ViewModel
{
    public class TutoringWindowVM: INotifyPropertyChanged
    {
        private PlatformService _platform;
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

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        private int _challengeId;
        public int ChallengeId
        {
            get => _challengeId;
            set
            {
                _challengeId = value;
                OnPropertyChanged("ChallengeId");
            }
        }

        private int _learnerId;
        public int LearnerId
        {
            get => _learnerId;
            set
            {
                _learnerId = value;
                OnPropertyChanged("LearnerId");
            }
        }

        public TutoringWindowVM()
        {
            Content = new ContentVM();
        }

        public void Update(string path, string serverUrl)
        {
            Path = path;
            _platform ??= new PlatformService(serverUrl);
        }

        public async void SubmitChallengeAsync()
        {
            if (string.IsNullOrEmpty(Path) || ChallengeId == 0 || LearnerId == 0) return;
            var content = await _platform.SubmitChallengeAsync(_path, ChallengeId, LearnerId);
            Content = new ContentVM(content.ChallengeCompleted, content.ApplicableHints, content.SolutionLO);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
