namespace Clean_CaDET.View
{
    using Clean_CaDET.Model;
    using Clean_CaDET.Model.Data;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

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
    }
}