using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTGK_NHOM7.ToeicExams
{
    public class Question : FullAuditedEntity<int>
    {
        public int QuestionGroupId { get; set; }
        public int QuestionNo { get; set; } // Số thứ tự câu (VD: 101, 102)
        public string Content { get; set; }
        public bool IsShuffle { get; set; } = true; // Cho phép xáo trộn đáp án

        [ForeignKey("QuestionGroupId")]
        public QuestionGroup QuestionGroup { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}