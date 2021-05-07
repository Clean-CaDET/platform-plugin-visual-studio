using Clean_CaDET.Model;
using Clean_CaDET.View.ViewModel;

namespace Clean_CaDET.View
{
    public partial class TutoringWindowControl
    {
        private PlatformService _platform;
        public TutoringWindowVM ViewModel { get; set; }

        public TutoringWindowControl()
        {
            InitializeComponent();
            _platform = new PlatformService();
            
            DataContext = ViewModel;
        }
    }
}