using Clean_CaDET.Model;
using Clean_CaDET.Model.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace Clean_CaDET.View
{
    public partial class TutoringWindowControl : UserControl
    {
        private PlatformService _platform;
        public TutoringWindowControl()
        {
            InitializeComponent();
            _platform = new PlatformService();
        }

        private async void GetEducationalContent(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EduSnippet> content = await _platform.GetEducationalContentAsync();
                lvDataBinding.ItemsSource = content;
            } catch(HttpRequestException)
            {
                //TODO: Error display logic
            }
        }

        private async void SendCode(object sender, RoutedEventArgs e)
        {
            await _platform.SendAllCodeAsync();
            
            //TODO: Get code smells
        }
    }
}