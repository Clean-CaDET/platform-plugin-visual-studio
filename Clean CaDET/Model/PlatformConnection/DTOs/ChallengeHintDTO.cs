using System.Collections.Generic;

namespace Clean_CaDET.Model.PlatformConnection.DTOs
{
    public class ChallengeHintDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public LearningObjectDTO LearningObject { get; set; }
        public List<string> ApplicableToCodeSnippets { get; set; }
    }
}