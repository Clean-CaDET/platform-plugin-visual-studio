using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;

namespace Clean_CaDET.View.ViewModel
{
    public class ChallengeHintVM
    {
        public string Content { get; set; }
        public LearningObjectVM LearningObject { get; set; }
        public string ApplicableTo { get; set; }

        public ChallengeHintVM(ChallengeHintDTO hint)
        {
            Content = hint.Content;
            LearningObject = new LearningObjectVM(hint.LearningObject);
            ApplicableTo = string.Join("\n", hint.ApplicableToCodeSnippets);
        }
    }
}