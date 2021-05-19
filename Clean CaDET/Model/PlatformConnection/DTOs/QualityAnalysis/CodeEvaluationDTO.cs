using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using System.Collections.Generic;

namespace Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis
{
    public class CodeEvaluationDTO
    {
        public Dictionary<string, List<IssueAdviceDTO>> CodeSnippetIssueAdvice { get; set; }
        public ISet<LearningObjectDTO> LearningObjects { get; set; }
    }
}