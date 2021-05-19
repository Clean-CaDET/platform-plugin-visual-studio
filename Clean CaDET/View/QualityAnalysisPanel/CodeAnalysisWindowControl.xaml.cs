using System.Windows.Controls;
using Clean_CaDET.View.QualityAnalysisPanel.ViewModel;

namespace Clean_CaDET.View.QualityAnalysisPanel
{
    public partial class CodeAnalysisWindowControl : UserControl
    {
        public CodeAnalysisWindowVM ViewModel { get; internal set; }
        public CodeAnalysisWindowControl()
        {
            InitializeComponent();
            ViewModel = new CodeAnalysisWindowVM();
            DataContext = ViewModel;
        }
    }
}