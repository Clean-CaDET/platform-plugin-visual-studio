using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using Clean_CaDET.View.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Clean_CaDET.View.TutoringPanel.ViewModel
{
    public class ContentVM: INotifyPropertyChanged
    {
        public string Title { get; set; }
        public List<ChallengeHintVM> ApplicableHints { get; set; }
        public LearningObjectVM Solution { get; set; }

        private Visibility _hintPanelVisibility;
        public Visibility HintPanelVisibility
        {
            get => _hintPanelVisibility;
            set
            {
                _hintPanelVisibility = value;
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

        public ContentVM(string title, List<ChallengeHintDTO> applicableHints, LearningObjectDTO solutionLO)
        {
            Title = title;
            ApplicableHints = applicableHints?.Select(hint => new ChallengeHintVM(hint)).ToList();
            Solution = new LearningObjectVM(solutionLO);

            HintPanelVisibility = ApplicableHints?.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            HintVisibility = Visibility.Collapsed;
            SolutionVisibility = Visibility.Collapsed;
        }

        public ContentVM()
        {
            HintPanelVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
