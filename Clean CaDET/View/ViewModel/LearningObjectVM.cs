using System.Windows;
using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;

namespace Clean_CaDET.View.ViewModel
{
    public class LearningObjectVM
    {
        public Visibility VideoVisibility { get; set; } = Visibility.Collapsed;
        public string Url { get; set; }
        public Visibility ImageVisibility { get; set; } = Visibility.Collapsed;
        public string Caption { get; set; }
        public Visibility TextVisibility { get; set; } = Visibility.Collapsed;
        public string Text { get; set; }
        public LearningObjectVM(LearningObjectDTO lo)
        {
            switch (lo)
            {
                case TextDTO text:
                    TextVisibility = Visibility.Visible;
                    Text = text.Content;
                    break;
                case ImageDTO image:
                    ImageVisibility = Visibility.Visible;
                    Url = image.Url;
                    Caption = image.Caption;
                    break;
                case VideoDTO video:
                    VideoVisibility = Visibility.Visible;
                    Url = video.Url;
                    break;
            }
        }
    }
}