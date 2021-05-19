using Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects;
using System.Windows;

namespace Clean_CaDET.View.LearningObject
{
    public class LearningObjectVM
    {
        public int Id { get; set; }
        public Visibility VideoVisibility { get; set; } = Visibility.Collapsed;
        public string Url { get; set; }
        public Visibility ImageVisibility { get; set; } = Visibility.Collapsed;
        public string Caption { get; set; }
        public Visibility TextVisibility { get; set; } = Visibility.Collapsed;
        public string Text { get; set; }
        public LearningObjectVM(LearningObjectDTO lo)
        {
            Id = lo.Id;
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

        public override bool Equals(object obj)
        {
            return obj is LearningObjectVM other
                   && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}