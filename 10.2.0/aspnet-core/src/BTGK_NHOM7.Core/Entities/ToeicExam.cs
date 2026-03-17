using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities;

namespace BTGK_NHOM7.Entities
{
    public class ToeicExam : Entity<int>
    {
        [Required]
        public string Title { get; set; }
        public int TimeMinutes { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual ICollection<ToeicPart> Parts { get; set; } = new List<ToeicPart>();

        [NotMapped] // Gom tất cả các câu hỏi từ các Part lại để View dễ dàng truy cập
        public ICollection<ToeicQuestion> Questions => Parts?.SelectMany(p => p.Questions ?? new List<ToeicQuestion>()).ToList() ?? new List<ToeicQuestion>();

        public ToeicExam()
        {
            CreationTime = DateTime.Now;
        }
    }
}