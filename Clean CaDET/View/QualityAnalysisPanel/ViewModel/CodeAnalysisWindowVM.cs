using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using System.ComponentModel;

namespace Clean_CaDET.View.QualityAnalysisPanel.ViewModel
{
    public class CodeAnalysisWindowVM: INotifyPropertyChanged
    {
        private CodeEvaluationVM _content;
        public CodeEvaluationVM Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public CodeAnalysisWindowVM()
        {
            Content = new CodeEvaluationVM();
        }

        public void UpdateContent(CodeEvaluationDTO content)
        {
            Content = new CodeEvaluationVM(content.CodeSnippetIssueAdvice, content.LearningObjects);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}