using System.ComponentModel;
using Clean_CaDET.Model.PlatformConnection.DTOs;

namespace Clean_CaDET.View.ViewModel
{
    public class TutoringWindowVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //TODO: Should create a ViewModel class eventually, for basic examples this will suffice.
        private EducationalContentDTO _content;
        public EducationalContentDTO Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        private ClassMetricsDTO _metrics;

        public ClassMetricsDTO Metrics
        {
            get => _metrics;
            set
            {
                _metrics = value;
                OnPropertyChanged("Metrics");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
