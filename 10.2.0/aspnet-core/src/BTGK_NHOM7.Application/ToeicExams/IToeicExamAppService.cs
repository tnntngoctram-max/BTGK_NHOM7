using Abp.Application.Services;
using System.IO;
using System.Threading.Tasks;
using BTGK_NHOM7.ToeicExams.Dto;
using System.Collections.Generic;
using BTGK_NHOM7.Entities;

namespace BTGK_NHOM7.ToeicExams
{
    public interface IToeicExamAppService : IApplicationService
    {
        Task<int> UploadAndParseExamAsync(CreateExamFromWordDto input);
        Task<int> ImportToeicFromWord(Stream fileStream);
        Task<ResultDto> CalculateScore(List<UserAnswer> answers);
        Task<ToeicExam> GetExamForPreview(int id);
    }
}
