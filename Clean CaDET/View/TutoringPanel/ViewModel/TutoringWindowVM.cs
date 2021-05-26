using Clean_CaDET.Model;
using System.ComponentModel;
using System.Net.Http;

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

        private string _error;

        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged("Error");
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
            if (string.IsNullOrEmpty(Path) || ChallengeId == 0 || LearnerId == 0)
            {
                Error =
                    "Missing important fields. Ensure the Challenge path is set (through the Submit Challenge Evaluation command) and that the Challenge and Learner Id are not 0.";
                return;
            }
            try
            {
                Error = "";
                var content = await _platform.SubmitChallengeAsync(_path, ChallengeId, LearnerId);
                Content = new ContentVM(content.ChallengeCompleted, content.ApplicableHints, content.SolutionLO);
            }
            catch (HttpRequestException e)
            {
                Error = e.Message + "\n" + e.InnerException?.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
