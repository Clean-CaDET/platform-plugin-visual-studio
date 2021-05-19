using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection
{
    public class MockConnection : IPlatformConnection
    {
        public async Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId)
        {
            return await Task.FromResult(new ChallengeEvaluationDTO
            {
                ApplicableHints = new List<ChallengeHintDTO>
                {
                    new ChallengeHintDTO
                    {
                        ApplicableToCodeSnippets = new List<string> {"snippet.snippet1", "snippet.snippet2"},
                        LearningObject = new ImageDTO
                        {
                            Id = 1,
                            Url = "https://i.ibb.co/1MBHhds/RS-semantic-cohesion.png", Caption = "Image caption 1"
                        },
                        Content = "This is the first hint for classes."
                    }
                },
                ChallengeCompleted = false,
                ChallengeId = 1,
                SolutionLO = new TextDTO {Content = "First text content"}
            });
        }

        public async Task<CodeEvaluationDTO> EvaluateCodeQualityAsync(string[] sourceCode)
        {
            return await Task.FromResult(new CodeEvaluationDTO
            {
                LearningObjects = new HashSet<LearningObjectDTO>
                {
                    new TextDTO { Content = "Kohezija modula se može definisati kao strukturalna metrika. Primeri jednostavnih strukturalnih metrika su broj linija koda klase (engl. *lines of code*; *LOC*) i broj metoda klase (engl. *number of methods defined*; *NMD*).\r\n\r\nKohezija ili nedostatak kohezije (engl. *Lack of cohesion of methods*; *LCOM*) je složenija strukturalna metrika koja određuje stepen \"umreženosti\" elemenata modula. Ovo podrazumeva brojanje veza između elemenata, gde bi primer veze kod klase bio pristup atributa od strane metode.\r\n\r\nVisoko-kohezivne klase imaju gustu mrežu veza, gde će većina metoda koristiti većinu atributa. Nisko-kohezivne klase imaju retku mrežu, gde će većina metoda pristupati manjem podskupu atributa.", Id = 1, LearningObjectSummaryId = 1},
                    new ImageDTO { Url = "https://i.ibb.co/w6T0Mg5/RS-structural-formula.png", Caption = "Izračunaj vrednost strukturalne kohezije za proizvoljan primer koda kako bi utemeljio razumevanje ove formule.", Id = 2, LearningObjectSummaryId = 2},
                    new VideoDTO { Url = "https://www.youtube.com/watch?v=qE-Gmu_YuQE", Id = 3, LearningObjectSummaryId = 3},
                    new ImageDTO { Url = "https://i.ibb.co/rFJK6Z8/RS-Methods-Hierarchy.png", Caption = "Kažemo da dobre metode treba da rade na jednom zadatku. Pitanje je kako definišemo zadatak. Uzmi po jednu metodu svake boje - opiši u rečenici šta rade, a šta ne rade. Da li uočavaš razliku u apstraktnosti tvog opisa?", Id = 4, LearningObjectSummaryId = 4},
                    new TextDTO { Content = "Funkcija predstavlja *imenovani* blok koda koji enkapsulira smislen zadatak. Ona predstavlja najjednostavniju grupaciju koda koja može samostalno da postoji. U objektno-orijentisanom programiranju funkcije su često metode koje definišu ponašanje objekta nad kojim se pozivaju. Principi koje poštujemo za formiranje čistih funkcija su jednako primenjivi na metode.\r\n\r\nČista funkcija je *fokusirana na jedan zadatak*. Ovaj zadatak je jasno opisan kroz imena zaglavlja funkcije, što uključuje samo ime funkcije i imena njenih parametra. Čista funkcija ima jednostavno telo i sastoji se od koda koji zahteva malo mentalnog napora da se razume. Kao posledica, ovakve funkcije često sadrže mali broj linija koda.", Id = 5, LearningObjectSummaryId = 5}
                },
                CodeSnippetIssueAdvice = new Dictionary<string, List<IssueAdviceDTO>>
                {
                    { "snippet.snippet1", new List<IssueAdviceDTO>
                    {
                        new IssueAdviceDTO { IssueType = "GOD_CLASS", SummaryIds = new List<int> {1,2,3}}
                    } },
                    { "snippet.snippet2", new List<IssueAdviceDTO>
                    {
                        new IssueAdviceDTO { IssueType = "GOD_CLASS", SummaryIds = new List<int> {1,2,3}}
                    } },
                    { "snippet.snippet1.method1()", new List<IssueAdviceDTO>
                    {
                        new IssueAdviceDTO { IssueType = "LONG_METHOD", SummaryIds = new List<int> {4,5}}
                    } }
                }
            });
        }
    }
}