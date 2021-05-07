using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean_CaDET.Model.SolutionParser.Data;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;

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

        public async Task<CSharpSolution> CompileBaseSolutionAsync()
        {
            Solution solution = GetBaseSolution();
            List<CSharpProject> compiledProjects = new List<CSharpProject>();
            foreach(Project project in solution.Projects)
            {
                compiledProjects.Add(await CompileProjectAsync(project));
            }
            return new CSharpSolution(compiledProjects);
        }

        private Solution GetBaseSolution()
        {
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();
            return workspace.CurrentSolution;
        }

        private async Task<CSharpProject> CompileProjectAsync(Project project)
        {
            List<CSharpDocument> compiledDocuments = new List<CSharpDocument>();
            foreach (Document document in project.Documents)
            {
                compiledDocuments.Add(await CompileDocumentAsync(document));
            }
            return new CSharpProject(new Guid(), compiledDocuments);
        }

        private async Task<CSharpDocument> CompileDocumentAsync(Document document)
        {
            SourceText sourceCode = await document.GetTextAsync();
            return new CSharpDocument(document.Name, sourceCode.ToString());
        }
    }
}
