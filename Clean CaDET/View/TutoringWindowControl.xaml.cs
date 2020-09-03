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
        public TutoringWindowControl()
        {
            InitializeComponent();
        }

        private async void GetEducationalContent(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EduSnippet> content = await PlatformService.Instance.GetEducationalContentAsync();
                lvDataBinding.ItemsSource = content;
            } catch(HttpRequestException)
            {
                //TODO: Error display logic
            }
        }

        private void SendCode(object sender, RoutedEventArgs e)
        {
            PlatformService.Instance.SendCodeAsync();
            
            //TODO: Get code smells
        }
    }
}