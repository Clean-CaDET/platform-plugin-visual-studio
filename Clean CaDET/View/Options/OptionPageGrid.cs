using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace Clean_CaDET.View.Options
{
    public class OptionPageGrid : DialogPage
    {
        [Category("Clean CaDET")]
        [DisplayName("Server API URL")]
        [Description("The URL of the Clean CaDET platform API.")]
        public string ServerUrl { get; set; } = "https://localhost:44333/api/";
    }
}
