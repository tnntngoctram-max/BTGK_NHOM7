using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using BTGK_NHOM7.Entities;

public class ToeicPart : Entity<int>
{
    public int PartNumber { get; set; } // 5, 6 hoặc 7
    public string Title { get; set; }
    public int ExamId { get; set; }
    [ForeignKey("ExamId")]
    public virtual ToeicExam Exam { get; set; }
    public virtual ICollection<ToeicPassage> Passages { get; set; }
    public virtual ICollection<ToeicQuestion> Questions { get; set; }
}