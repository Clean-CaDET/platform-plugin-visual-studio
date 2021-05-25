using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace Clean_CaDET.View.TutoringPanel
{
    [Guid("0381f46d-804e-4919-ad26-fa160bfee532")]
    public sealed class TutoringWindow : ToolWindowPane
    {
        public TutoringWindow() : base(null)
        {
            Caption = "CCaDET Challenge Analysis";
            Content = new TutoringWindowControl();
        }

        public void UpdateVmContent(string selectedPath, string serverUrl)
        {
            if (Content is TutoringWindowControl windowControl) windowControl.ViewModel.Update(selectedPath, serverUrl);
        }
    }
}
