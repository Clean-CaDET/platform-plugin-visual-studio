using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using System.Collections.Generic;
using System.Linq;
using Clean_CaDET.View.LearningObject;

namespace Clean_CaDET.View.QualityAnalysisPanel.ViewModel
{
    public class CodeEvaluationVM
    {
        public string NoIssues { get; set; }
        public List<CodeIssueVM> Issues { get; set; }

        public CodeEvaluationVM(Dictionary<string, List<IssueAdviceDTO>> codeSnippetIssueAdvice, ISet<LearningObjectDTO> learningObjects)
        {
            if (codeSnippetIssueAdvice.Count == 0)
            {
                NoIssues = "According to our analysis, the submitted code does not suffer from any code smells.";
                Issues = new List<CodeIssueVM>();
                return;
            }
            var issueMap = OrganizeAdviceAroundIssues(codeSnippetIssueAdvice, learningObjects);
            Issues = issueMap.Values.ToList();
        }

        private static Dictionary<string, CodeIssueVM> OrganizeAdviceAroundIssues(Dictionary<string, List<IssueAdviceDTO>> codeSnippetIssueAdvice, ISet<LearningObjectDTO> learningObjects)
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

        public CodeEvaluationVM()
        {
            Issues = new List<CodeIssueVM>();
        }
    }
}