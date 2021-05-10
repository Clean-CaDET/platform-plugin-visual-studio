using JsonSubTypes;
using Newtonsoft.Json;

namespace Clean_CaDET.Model.PlatformConnection.DTOs.LearningObjects
{
    [JsonConverter(typeof(JsonSubtypes), "typeDiscriminator")]
    [JsonSubtypes.KnownSubType(typeof(TextDTO), "text")]
    [JsonSubtypes.KnownSubType(typeof(ImageDTO), "image")]
    [JsonSubtypes.KnownSubType(typeof(VideoDTO), "video")]
    public class LearningObjectDTO
    {
        public int Id { get; set; }
        public int LearningObjectSummaryId { get; set; }
    }
}