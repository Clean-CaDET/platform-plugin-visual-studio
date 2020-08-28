namespace Clean_CaDET.View
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    [Guid("0381f46d-804e-4919-ad26-fa160bfee532")]
    public class TutoringWindow : ToolWindowPane
    {
        public TutoringWindow() : base(null)
        {
            this.Caption = "Clean CaDET Tutor";
            this.Content = new TutoringWindowControl();
        }
    }
}
