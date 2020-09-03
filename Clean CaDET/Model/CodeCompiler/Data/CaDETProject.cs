using System.Collections.Generic;

namespace Clean_CaDET.Model.CodeCompiler.Data
{
    public class CaDETProject
    {
        public string FileName { get; private set; }
        public IEnumerable<CaDETDocument> CompiledProjectItems { get; private set; }

        public CaDETProject(string fileName, IEnumerable<CaDETDocument> compiledProjectItems)
        {
            FileName = fileName;
            CompiledProjectItems = compiledProjectItems;
        }
    }
}