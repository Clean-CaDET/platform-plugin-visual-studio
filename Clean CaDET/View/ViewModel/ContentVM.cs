using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.Collections.Generic;

namespace Clean_CaDET.View.ViewModel
{
    public class ContentVM
    {
        public string Title { get; set; }
        public List<ChallengeHintDTO> ApplicableHints { get; }
        public LearningObjectDTO SolutionLO { get; }

        public ContentVM(string title, List<ChallengeHintDTO> applicableHints, LearningObjectDTO solutionLO)
        {
            Title = title;
            ApplicableHints = applicableHints;
            SolutionLO = solutionLO;
        }
    }
}
