using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace Clean_CaDET.View.QualityAnalysisPanel
{
    [Guid("d4e62e42-5172-4eac-84d5-4daa5a39f02b")]
    public class CodeAnalysisWindow : ToolWindowPane
    {
        public CodeAnalysisWindow() : base(null)
        {
            Caption = "CCaDET Quality Analysis";
            Content = new CodeAnalysisWindowControl();
        }

        public void UpdateVmContent(CodeEvaluationDTO evaluation)
        {
            if (Content is CodeAnalysisWindowControl windowControl) windowControl.ViewModel.UpdateContent(evaluation);
        }
    }
}
