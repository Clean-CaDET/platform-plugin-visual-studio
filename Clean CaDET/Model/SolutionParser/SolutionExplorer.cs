using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System.Linq;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.SolutionParser
{
    class SolutionExplorer
    {
        public async Task<string> CollectSourceCodeAsync(string filePath)
        {
            Solution solution = GetBaseSolution();
            foreach (var project in solution.Projects)
            {
                var doc = project.Documents.First(document => document.FilePath.Equals(filePath));
                SourceText sourceCode = await doc.GetTextAsync();
                return sourceCode.ToString();
            }

            return null;
        }

        private Solution GetBaseSolution()
        {
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();
            return workspace.CurrentSolution;
        }
    }
}
