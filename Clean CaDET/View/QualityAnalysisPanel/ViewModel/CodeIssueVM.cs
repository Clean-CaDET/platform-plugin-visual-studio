using System.Collections.Generic;
using Clean_CaDET.View.LearningObject;

namespace Clean_CaDET.View.QualityAnalysisPanel.ViewModel
{
    public class CodeIssueVM
    {
        public string Issue { get; set; }
        public ISet<string> ApplicableSnippets { get; set; }
        public ISet<LearningObjectVM> LearningObjects { get; set; }

        public CodeIssueVM(string issueType)
        {
            Issue = issueType;
            ApplicableSnippets = new HashSet<string>();
            LearningObjects = new HashSet<LearningObjectVM>();
        }
    }
}