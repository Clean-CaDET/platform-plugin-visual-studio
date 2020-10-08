using System;
using System.Collections.Generic;

namespace Clean_CaDET.Model.SolutionParser.Data
{
    public class CSharpProject
    {
        public Guid Id { get; }
        public IEnumerable<CSharpDocument> ProjectDocuments { get; }

        public CSharpProject(Guid id, IEnumerable<CSharpDocument> projectDocuments)
        {
            Id = id;
            ProjectDocuments = projectDocuments;
        }
    }
}