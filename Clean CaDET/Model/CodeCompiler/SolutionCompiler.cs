using Clean_CaDET.Model.CodeCompiler.Data;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.CodeCompiler
{
    class SolutionCompiler
    {
        public async Task<CaDETSolution> CompileBaseSolutionAsync()
        {
            Solution solution = GetBaseSolution();
            List<CaDETProject> compiledProjects = new List<CaDETProject>();
            foreach(Project project in solution.Projects)
            {
                compiledProjects.Add(await CompileProjectAsync(project));
            }
            return new CaDETSolution(solution.FilePath, compiledProjects);
        }

        private Solution GetBaseSolution()
        {
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            var workspace = componentModel.GetService<VisualStudioWorkspace>();
            return workspace.CurrentSolution ?? throw new NullReferenceException();
        }

        private async Task<CaDETProject> CompileProjectAsync(Project project)
        {
            List<CaDETDocument> compiledDocuments = new List<CaDETDocument>();
            foreach (Document document in project.Documents)
            {
                compiledDocuments.Add(await CompileDocumentAsync(document));
            }
            return new CaDETProject(project.Name, compiledDocuments);
        }

        private async Task<CaDETDocument> CompileDocumentAsync(Document document)
        {
            SourceText sourceCode = await document.GetTextAsync();
            return new CaDETDocument(document.Name, sourceCode.ToString());
        }
    }
}
