using System.Collections.Generic;

namespace BTGK_NHOM7.ToeicExams.Dto
{
    public class ResultDto
    {
        public int Score { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int SkipCount { get; set; }
    }

    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
        public string SelectedValue { get; set; } // A, B, C hoặc D
    }
}