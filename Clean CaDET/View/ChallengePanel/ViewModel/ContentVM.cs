using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Clean_CaDET.View.LearningObject;

namespace Clean_CaDET.View.ChallengePanel.ViewModel
{
    public class ContentVM: INotifyPropertyChanged
    {
        public string Title { get; set; }
        public List<ChallengeHintVM> ApplicableHints { get; set; }
        public LearningObjectVM Solution { get; set; }
        
        public string HintText { get; set; }

        public string SolutionText { get; set; }

        private Visibility _mainPanelVisibility;
        public Visibility MainPanelVisibility
        {
            get => _mainPanelVisibility;
            set
            {
                _mainPanelVisibility = value;
                OnPropertyChanged("HintPanelVisibility");
            }
        }

        private Visibility _hintVisibility;
        public Visibility HintVisibility
        {
            get => _hintVisibility;
            set
            {
                _hintVisibility = value;
                OnPropertyChanged("HintVisibility");
            }
        }

        private Visibility _hintButtonVisibility;
        public Visibility HintButtonVisibility
        {
            get => _hintButtonVisibility;
            set
            {
                _hintButtonVisibility = value;
                OnPropertyChanged("HintButtonVisibility");
            }
        }

        private Visibility _solutionVisibility;
        public Visibility SolutionVisibility
        {
            get => _solutionVisibility;
            set
            {
                _solutionVisibility = value;
                OnPropertyChanged("SolutionVisibility");
            }
        }

        private Visibility _solutionButtonVisibility;
        public Visibility SolutionButtonVisibility
        {
            get => _solutionButtonVisibility;
            set
            {
                _solutionButtonVisibility = value;
                OnPropertyChanged("SolutionButtonVisibility");
            }
        }

        public ContentVM(bool challengeCompleted, List<ChallengeHintDTO> applicableHints, LearningObjectDTO solutionLO)
        {
            Title = challengeCompleted //TODO: Location of Heisenbug when moving VS from one monitor to another.
                ? "Congratulations, you completed the challenge!"
                : "Your submission is not yet there, keep going!";
            ApplicableHints = applicableHints?.Select(hint => new ChallengeHintVM(hint)).ToList();
            Solution = new LearningObjectVM(solutionLO);

            if(challengeCompleted) HintText = "You can view all the available hints for this completed challenge.";
            else if(ApplicableHints?.Count == 1) HintText = "You have 1 hint available.";
            else HintText = "You have " + ApplicableHints.Count + " hints available.";

            SolutionText = "Feel free to view our solution, but only after you've attempted to produce your own. Note that your solution can differ from ours in some aspects.";

            MainPanelVisibility = ApplicableHints?.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            HintVisibility = Visibility.Collapsed;
            SolutionVisibility = Visibility.Collapsed;
        }

        public ContentVM()
        {
            MainPanelVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
