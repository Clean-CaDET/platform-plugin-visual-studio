using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Clean_CaDET.Model.SolutionParser
{
    internal class SolutionExplorer
    {
        public async Task<string[]> CollectSourceCodeAsync(string filePath)
        {
            var solution = GetBaseSolution();
            foreach (var project in solution.Projects)
            {
                var documents = project.Documents.Where(document => document.FilePath.Contains(filePath)).ToList();
                if (documents.Count == 0) continue;

                var codes = await Task.WhenAll(documents.Select(d => d.GetTextAsync()));
                return codes.Select(code => code.ToString()).ToArray();
            }

            return null;
        }

        private static Solution GetBaseSolution()
        {
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();
            return workspace.CurrentSolution;
        }
    }
}
