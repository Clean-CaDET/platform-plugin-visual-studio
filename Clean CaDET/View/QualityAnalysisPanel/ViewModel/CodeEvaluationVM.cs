using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Clean_CaDET.View.LearningObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Clean_CaDET.View.QualityAnalysisPanel.ViewModel
{
    public class CodeEvaluationVM
    {
        public string NoIssues { get; set; }
        public ObservableCollection<CodeIssueVM> Issues { get; set; }

        public CodeEvaluationVM()
        {
            Issues = new ObservableCollection<CodeIssueVM>();
        }
        public CodeEvaluationVM(Dictionary<string, List<IssueAdviceDTO>> codeSnippetIssueAdvice, ISet<LearningObjectDTO> learningObjects)
        {
            if (codeSnippetIssueAdvice.Count == 0)
            {
                NoIssues = "According to our analysis, the submitted code does not suffer from any code smells.";
                Issues = new ObservableCollection<CodeIssueVM>();
                return;
            }
            var issueMap = GroupAdviceAroundIssues(codeSnippetIssueAdvice, learningObjects);
            Issues = new ObservableCollection<CodeIssueVM>(issueMap.Values);
        }

        private static Dictionary<string, CodeIssueVM> GroupAdviceAroundIssues(Dictionary<string, List<IssueAdviceDTO>> codeSnippetIssueAdvice, ISet<LearningObjectDTO> learningObjects)
        {
            var issueMap = new Dictionary<string, CodeIssueVM>();
            foreach (var codeSnippetId in codeSnippetIssueAdvice.Keys)
            {
                var issueAdvice = codeSnippetIssueAdvice[codeSnippetId];
                foreach (var issue in issueAdvice)
                {
                    if (!issueMap.ContainsKey(issue.IssueType)) issueMap.Add(issue.IssueType, new CodeIssueVM(issue.IssueType));
                    issueMap[issue.IssueType].ApplicableSnippets.Add(codeSnippetId);
                    issueMap[issue.IssueType].LearningObjects.UnionWith(DetermineApplicableLOs(learningObjects, issue));
                }
            }

            return issueMap;
        }

        private static IEnumerable<LearningObjectVM> DetermineApplicableLOs(ISet<LearningObjectDTO> learningObjects, IssueAdviceDTO issue)
        {
            return learningObjects
                .Where(lo => issue.SummaryIds.Contains(lo.LearningObjectSummaryId))
                .Select(lo => new LearningObjectVM(lo));
        }
    }
}