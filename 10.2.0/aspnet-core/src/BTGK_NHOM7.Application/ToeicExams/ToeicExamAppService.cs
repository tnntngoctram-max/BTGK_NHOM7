using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI; 
using NPOI.XWPF.UserModel;
using Microsoft.EntityFrameworkCore;
using BTGK_NHOM7.ToeicExams.Dto;
using BTGK_NHOM7.Entities;

namespace BTGK_NHOM7.ToeicExams
{
    public class ToeicExamAppService : BTGK_NHOM7AppServiceBase, IToeicExamAppService
    {
        private readonly IRepository<ToeicExam, int> _examRepository;
        private readonly IRepository<ToeicQuestion, int> _questionRepository;

        public ToeicExamAppService(
            IRepository<ToeicExam, int> examRepository,
            IRepository<ToeicQuestion, int> questionRepository)
        {
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }

        public async Task<int> UploadAndParseExamAsync(CreateExamFromWordDto input)
        {
            if (input.FileBytes == null || input.FileBytes.Length == 0)
                throw new UserFriendlyException("File dữ liệu trống!");

            using (var stream = new MemoryStream(input.FileBytes))
            {
                return await ImportToeicFromWord(stream); //trả ID về
            }
        }

        public async Task<int> ImportToeicFromWord(Stream stream)
        {
            XWPFDocument doc;
            try
            {
                doc = new XWPFDocument(stream); 
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Không thể đọc file.");
            }

            var newExam = new ToeicExam { Parts = new List<ToeicPart>() };
            ToeicPart currentPart = null;
            ToeicPassage currentPassage = null;
            ToeicQuestion currentQuestion = null;
            bool isReadingPassage = false;

            foreach (var para in doc.Paragraphs)
            {
                string text = para.ParagraphText.Trim();
                if (string.IsNullOrWhiteSpace(text)) continue;

                if (text.StartsWith("[EXAM_TITLE]"))
                {
                    newExam.Title = text.Replace("[EXAM_TITLE]", "").Trim();
                    continue;
                }
                if (text.StartsWith("[EXAM_TIME]"))
                {
                    if (int.TryParse(text.Replace("[EXAM_TIME]", "").Trim(), out int time))
                        newExam.TimeMinutes = time;
                    else
                        throw new UserFriendlyException("Lỗi: Thẻ [EXAM_TIME] không hợp lệ!");
                    continue;
                }

                if (text.StartsWith("[PART:"))
                {
                    ValidateQuestionHasKey(currentQuestion);
                    string partNumStr = text.Replace("[PART:", "").Replace("]", "").Trim();
                    if (!int.TryParse(partNumStr, out int partNum))
                        throw new UserFriendlyException($"Lỗi: Định dạng Part không hợp lệ: {text}");

                    currentPart = new ToeicPart 
                    { 
                        PartNumber = partNum, Title = "Part " + partNum,
                        Passages = new List<ToeicPassage>(), Questions = new List<ToeicQuestion>()
                    };
                    newExam.Parts.Add(currentPart);
                    currentPassage = null; currentQuestion = null;
                    continue;
                }

                if (text.StartsWith("[PASSAGE_START]"))
                {
                    isReadingPassage = true;
                    currentPassage = new ToeicPassage { Content = "", Questions = new List<ToeicQuestion>() };
                    currentPart?.Passages.Add(currentPassage);
                    continue;
                }
                if (text.StartsWith("[PASSAGE_END]")) { isReadingPassage = false; continue; }
                if (isReadingPassage && currentQuestion == null)
                {
                    if (currentPassage != null) currentPassage.Content += text + "\n";
                    continue;
                }

                if (text.StartsWith("[Q:"))
                {
                    ValidateQuestionHasKey(currentQuestion);
                    currentQuestion = new ToeicQuestion { Shuffle = true }; 
                    
                    string qLine = text;
                    if (qLine.Contains("[SHUFFLE: FALSE]")) { currentQuestion.Shuffle = false; qLine = qLine.Replace("[SHUFFLE: FALSE]", "").Trim(); }
                    else if (qLine.Contains("[SHUFFLE: TRUE]")) { qLine = qLine.Replace("[SHUFFLE: TRUE]", "").Trim(); }

                    int startIdx = qLine.IndexOf("[Q:") + 3;
                    int endIdx = qLine.IndexOf("]", startIdx);
                    if (startIdx >= 3 && endIdx > startIdx)
                    {
                        if (int.TryParse(qLine.Substring(startIdx, endIdx - startIdx), out int qNum))
                            currentQuestion.QuestionNumber = qNum;
                    }

                    currentPart?.Questions.Add(currentQuestion);
                    if (currentPassage != null) currentPassage.Questions.Add(currentQuestion);

                    string remainingText = qLine.Substring(endIdx + 1).Trim();
                    if (!string.IsNullOrEmpty(remainingText)) currentQuestion.QuestionText = remainingText + "\n";
                    continue;
                }

                if (currentQuestion != null)
                {
                    if (text.StartsWith("[A]")) { currentQuestion.OptionA = text.Replace("[A]", "").Trim(); continue; }
                    if (text.StartsWith("[B]")) { currentQuestion.OptionB = text.Replace("[B]", "").Trim(); continue; }
                    if (text.StartsWith("[C]")) { currentQuestion.OptionC = text.Replace("[C]", "").Trim(); continue; }
                    if (text.StartsWith("[D]")) { currentQuestion.OptionD = text.Replace("[D]", "").Trim(); continue; }
                    
                    if (text.StartsWith("[KEY:"))
                    {
                        string key = text.Replace("[KEY:", "").Replace("]", "").Trim();
                        if (string.IsNullOrEmpty(key) || !"ABCD".Contains(key.ToUpper()))
                            throw new UserFriendlyException($"Lỗi: Đáp án [KEY] câu {currentQuestion.QuestionNumber} không hợp lệ!");
                        
                        currentQuestion.CorrectAnswer = key.ToUpper();
                        continue;
                    }

                    if (!text.StartsWith("[")) currentQuestion.QuestionText += text + "\n";
                }
            }

            ValidateQuestionHasKey(currentQuestion);

            if (string.IsNullOrEmpty(newExam.Title))
                throw new UserFriendlyException("File Word bị thiếu thẻ [EXAM_TITLE].");
            if (newExam.Parts == null || !newExam.Parts.Any())
                throw new UserFriendlyException("Đề thi không có phần thi [PART:...] nào cả!");

            return await _examRepository.InsertAndGetIdAsync(newExam);
        }

        private void ValidateQuestionHasKey(ToeicQuestion question)
        {
            if (question != null && string.IsNullOrEmpty(question.CorrectAnswer))
                throw new UserFriendlyException($"Câu hỏi số {question.QuestionNumber} đang thiếu đáp án [KEY:x] !");
        }

        public async Task<ToeicExam> GetExamForPreview(int id)
        {
            var exam = await _examRepository.GetAll()
                .Include(e => e.Parts)
                    .ThenInclude(p => p.Passages)
                .Include(e => e.Parts)
                    .ThenInclude(p => p.Questions)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
            {
                throw new UserFriendlyException("Không tìm thấy đề thi này!");
            }

            return exam;
        }

        public async Task<ResultDto> CalculateScore(List<UserAnswer> answers)
        {
            var result = new ResultDto();
            if (answers == null || !answers.Any()) return result;

            var questionIds = answers.Select(a => a.QuestionId).ToList();
            var dbQuestions = await _questionRepository.GetAllListAsync(q => questionIds.Contains(q.Id));

            foreach (var ans in answers)
            {
                var question = dbQuestions.FirstOrDefault(q => q.Id == ans.QuestionId);
                if (question == null) continue;

                string userChoice = ans.SelectedAnswer?.Trim().ToUpper();

                if (string.IsNullOrEmpty(userChoice))
                {
                    result.SkipCount++;
                }
                else if (userChoice == question.CorrectAnswer)
                {
                    result.CorrectCount++;
                }
                else
                {
                    result.WrongCount++;
                }
            }

            return result;
        }
    }
}