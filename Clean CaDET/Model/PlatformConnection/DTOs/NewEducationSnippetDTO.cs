using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection.DTOs
{
    public class NewEducationSnippetDTO
    {
        public int SnippetQuality { get; set; }
        public int SnippetDifficulty { get; set; }
        public SnippetType SnippetType { get; set; }
        public List<Tag> Tags { get; set; }
        public string Content { get; set; }
    }
}