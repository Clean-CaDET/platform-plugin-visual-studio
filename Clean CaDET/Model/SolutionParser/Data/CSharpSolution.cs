using System.Collections.Generic;

namespace Clean_CaDET.Model.SolutionParser.Data
{
    public class CSharpSolution
    {
        public IEnumerable<CSharpProject> Projects { get; }

        public CSharpSolution(IEnumerable<CSharpProject> compiledProjects)
        {
            Projects = compiledProjects;
        }
    }
}