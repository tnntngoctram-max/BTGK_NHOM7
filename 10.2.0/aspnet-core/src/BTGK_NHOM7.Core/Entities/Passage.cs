using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

public class ToeicPassage : Entity<int>
{
    public string Content { get; set; } // Nội dung giữa [PASSAGE_START] và [PASSAGE_END]
    public int PartId { get; set; }
    [ForeignKey("PartId")]
    public virtual ToeicPart Part { get; set; }
    public virtual ICollection<ToeicQuestion> Questions { get; set; }
}