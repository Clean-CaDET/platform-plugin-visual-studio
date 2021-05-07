using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using System.Collections.Generic;

namespace Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation
{
    public class ChallengeHintDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public LearningObjectDTO LearningObject { get; set; }
        public List<string> ApplicableToCodeSnippets { get; set; }
    }
}