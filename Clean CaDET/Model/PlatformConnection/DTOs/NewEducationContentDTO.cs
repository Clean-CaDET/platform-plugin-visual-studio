using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection.DTOs
{
    public class NewEducationContentDTO
    {
        public int ContentQuality { get; set; }
        public int ContentDifficulty { get; set; }
        public ObservableCollection<NewEducationSnippetDTO> EducationSnippets { get; set; }
    }
}