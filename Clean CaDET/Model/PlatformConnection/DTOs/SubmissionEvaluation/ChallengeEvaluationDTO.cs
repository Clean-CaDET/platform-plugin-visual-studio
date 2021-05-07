using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using System.Collections.Generic;

namespace Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation
{
    public class ChallengeEvaluationDTO
    {
        public int ChallengeId { get; set; }
        public bool ChallengeCompleted { get; set; }
        public List<ChallengeHintDTO> ApplicableHints { get; set; }
        public LearningObjectDTO SolutionLO { get; set; }
    }
}