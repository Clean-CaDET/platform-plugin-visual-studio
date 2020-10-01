namespace Clean_CaDET.View
{
    using Microsoft.VisualStudio.Shell;
    using System.Runtime.InteropServices;

    [Guid("0381f46d-804e-4919-ad26-fa160bfee532")]
    public class TutoringWindow : ToolWindowPane
    {
        public TutoringWindow() : base(null)
        {
            Caption = "Clean CaDET";
            Content = new TutoringWindowControl();
        }
    }
}
