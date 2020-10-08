using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Clean_CaDET.Model.PlatformConnection.DTOs;

namespace Clean_CaDET.View
{
    [Guid("0381f46d-804e-4919-ad26-fa160bfee532")]
    public class TutoringWindow : ToolWindowPane
    {
        public TutoringWindow() : base(null)
        {
            Caption = "Clean CaDET";
            Content = new TutoringWindowControl();
        }

        public void UpdateVMContent(EducationalContentDTO content, ClassMetricsDTO metrics)
        {
            var windowControl = Content as TutoringWindowControl;
            windowControl.ViewModel.Content = content;
            windowControl.ViewModel.Metrics = metrics;
        }
    }
}
