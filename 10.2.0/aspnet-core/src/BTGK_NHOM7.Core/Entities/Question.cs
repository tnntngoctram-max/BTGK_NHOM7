using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

public class ToeicQuestion : Entity<int>
{
    public string QuestionText { get; set; }
    public int QuestionNumber { get; set; } // Ví dụ: 101, 102...
    public bool Shuffle { get; set; } // Lấy từ thẻ [SHUFFLE]
    public string CorrectAnswer { get; set; } // A, B, C hoặc D
    
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }

    public int PartId { get; set; }
    public int? PassageId { get; set; } // Null nếu là Part 5
    
    [ForeignKey("PartId")]
    public virtual ToeicPart Part { get; set; }
    [ForeignKey("PassageId")]
    public virtual ToeicPassage Passage { get; set; }

}