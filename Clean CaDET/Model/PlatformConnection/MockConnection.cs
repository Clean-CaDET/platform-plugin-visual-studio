using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection
{
    internal class MockConnection : IPlatformConnection
    {
        public Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId)
        {
            return Task.FromResult(new ChallengeEvaluationDTO
            {
                ApplicableHints = new List<ChallengeHintDTO>
                {
                    new ChallengeHintDTO
                    {
                        ApplicableToCodeSnippets = new List<string> {"snippet.snippet1", "snippet.snippet2"},
                        LearningObject = new ImageDTO { Url= "https://i.ibb.co/1MBHhds/RS-semantic-cohesion.png", Caption = "Image caption 1"},
                        Content = "This is the first hint for classes."
                    }
                },
                ChallengeCompleted = false,
                ChallengeId = 1,
                SolutionLO = new TextDTO { Content = "First text content"}
            });
        }
    }
}