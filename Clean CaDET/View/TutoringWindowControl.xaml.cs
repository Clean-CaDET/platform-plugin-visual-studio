using Clean_CaDET.Model;
using Clean_CaDET.View.ViewModel;
using System.Windows;
using System.Windows.Controls;
using Clean_CaDET.Model.DTOs;

namespace Clean_CaDET.View
{
    public partial class TutoringWindowControl : UserControl
    {
        private PlatformService _platform;
        public TutoringWindowVM ViewModel { get; set; }

        public TutoringWindowControl()
        {
            InitializeComponent();
            _platform = new PlatformService();
            ViewModel = new TutoringWindowVM
            {
                Content = new EducationalContentDTO
                {
                    Title = "Low cohesion in class",
                    Image = "https://thebojansblog.files.wordpress.com/2015/04/your-email-1.jpg",
                    GeneralDescription =
                        "Cohesion refers to the degree to which the elements inside a module belong together. For classes, it measures the strength of the relationship between the methods and data of a class - signaling how well the class represents a unified concept.",
                    DetectedIssue =
                        "Our analysis shows that this class has low cohesion. This signals that there might be two or more concepts represented by this class. Looking at the figure, we see how the class can be split into three classes, each containing a method and related field. Importantly, we presume the methods are not accessors (getters and setters).",
                    Recommendations =
                        "See if you can split the class by grouping the fields (or their subset) with methods that utilize them. If you can find suitable names for the two groups than you should perform the Extract Class refactoring. If one group is hard to define and sufficiently large, this might mean you have even more classes to extract."
                }
            };
            
            DataContext = ViewModel;
        }
    }
}