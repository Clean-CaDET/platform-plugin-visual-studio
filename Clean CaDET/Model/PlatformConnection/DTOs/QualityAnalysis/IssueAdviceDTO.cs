using System.Collections.Generic;

namespace Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis
{
    public class IssueAdviceDTO
    {
        public string IssueType { get; set; }
        public List<int> SummaryIds { get; set; }
    }
}