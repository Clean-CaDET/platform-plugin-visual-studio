using System.Windows.Controls;
using System.Windows.Navigation;

namespace Clean_CaDET.View.LearningObject
{
    public partial class LearningObjectPanel : UserControl
    {
        public LearningObjectPanel()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
