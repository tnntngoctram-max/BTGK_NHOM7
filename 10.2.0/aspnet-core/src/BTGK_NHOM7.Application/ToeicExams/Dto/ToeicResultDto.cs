using System.Collections.Generic;

namespace BTGK_NHOM7.ToeicExams.Dto
{
    public class ResultDto
    {
        public int Score { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int SkipCount { get; set; }
        public List<PartAnalysisDto> PartAnalysis { get; set; } = new List<PartAnalysisDto>();
    }

    public class PartAnalysisDto
    {
        public int PartNumber { get; set; }
        public int CorrectCount { get; set; }
        public int TotalCount { get; set; }
        public int Percentage => TotalCount > 0 ? (CorrectCount * 100) / TotalCount : 0;
    }

    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
        public string SelectedValue { get; set; } // A, B, C hoặc D
    }
}