namespace Clean_CaDET.Model.SolutionParser.Data
{
    public class CSharpDocument
    {
        public string Name { get; }
        public string SourceCode { get; }

        public CSharpDocument(string name, string sourceCode)
        {
            Name = name;
            SourceCode = sourceCode;
        }
    }
}