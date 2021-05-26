using Clean_CaDET.Model;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Clean_CaDET.View.ChallengePanel.ViewModel
{
    public class ChallengeWindowVM: INotifyPropertyChanged
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

        public string FullPath { get; set; }
        private string _shortPath;
        public string ShortPath
        {
            get => _shortPath;
            set
            {
                _shortPath = value;
                OnPropertyChanged("ShortPath");
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

        public ChallengeWindowVM()
        {
            Content = new ContentVM();
        }

        public void Update(string path, string serverUrl)
        {
            FullPath = path;
            var pathElements = path.Split(Path.DirectorySeparatorChar);
            ShortPath = "~" + string.Join(Path.DirectorySeparatorChar.ToString(), pathElements.Reverse().Take(5).Reverse());
            _platform ??= new PlatformService(serverUrl);
        }

        public async void SubmitChallengeAsync()
        {
            if (string.IsNullOrEmpty(ShortPath) || ChallengeId == 0 || LearnerId == 0)
            {
                Error =
                    "Missing important fields. Ensure the Challenge path is set (through the Submit Challenge Evaluation command) and that the Challenge and Learner Id are not 0.";
                return;
            }
            try
            {
                Error = "";
                var content = await _platform.SubmitChallengeAsync(FullPath, ChallengeId, LearnerId);
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
