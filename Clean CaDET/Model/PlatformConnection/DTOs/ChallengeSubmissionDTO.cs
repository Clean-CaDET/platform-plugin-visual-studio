namespace Clean_CaDET.Model.PlatformConnection.DTOs
{
    internal class ChallengeSubmissionDTO
    {
        public string[] SourceCode { get; set; }
        public int ChallengeId { get; set; }
        public int LearnerId { get; set; }

        public ChallengeSubmissionDTO(string[] sourceCode, int challengeId, int learnerId)
        {
            SourceCode = sourceCode;
            ChallengeId = challengeId;
            LearnerId = learnerId;
        }
    }
}