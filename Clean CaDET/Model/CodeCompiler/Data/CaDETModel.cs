namespace Clean_CaDET.Model.CodeCompiler.Data
{
    //TODO: This is the aggregate root of all code related to the project. Apart from the active solution it should host a Map<commit ID, Solution state>
    class CaDETModel
    {
        public CaDETSolution LatestSolutionState { get; private set; }

        public CaDETModel(CaDETSolution solution)
        {
            this.LatestSolutionState = solution;
        }
    }
}
