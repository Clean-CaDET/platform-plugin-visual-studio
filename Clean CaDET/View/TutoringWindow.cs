using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace Clean_CaDET.View
{
    [Guid("0381f46d-804e-4919-ad26-fa160bfee532")]
    public sealed class TutoringWindow : ToolWindowPane
    {
        public TutoringWindow() : base(null)
        {
            Caption = "Clean CaDET";
            Content = new TutoringWindowControl();
        }

        public void UpdateVmContent(ChallengeEvaluationDTO content)
        {
            if (Content is TutoringWindowControl windowControl) windowControl.ViewModel.UpdateContent(content);
        }
    }
}
