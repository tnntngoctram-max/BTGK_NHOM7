using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace BTGK_NHOM7.ToeicExams
{
    public class Exam : FullAuditedEntity<int>
    {
        public string Title { get; set; }
        public int TimeInMinutes { get; set; }
        public string Description { get; set; }
        
        public ICollection<QuestionGroup> QuestionGroups { get; set; }
    }
}