using Clean_CaDET.Model.DTOs;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

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
