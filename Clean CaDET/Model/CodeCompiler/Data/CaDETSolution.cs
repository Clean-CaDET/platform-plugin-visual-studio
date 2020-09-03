using System.Collections.Generic;

namespace Clean_CaDET.Model.CodeCompiler.Data
{
    public class CaDETSolution
    {
        public IEnumerable<CaDETProject> Projects { get; private set; }
        public string FileName { get; private set; }

        public CaDETSolution(string fileName, IEnumerable<CaDETProject> compiledProjects)
        {
            FileName = fileName;
            Projects = compiledProjects;
        }
    }
}