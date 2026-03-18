using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using BTGK_NHOM7.Controllers;
using BTGK_NHOM7.ToeicExams;
using BTGK_NHOM7.ToeicExams.Dto;
using Abp.UI;
using DocumentFormat.OpenXml.Office2010.Excel;
using BTGK_NHOM7.Entities;

namespace BTGK_NHOM7.Web.Controllers
{
    public class ToeicExamsController : BTGK_NHOM7ControllerBase
    {
        private readonly IToeicExamAppService _toeicExamAppService;
        public ToeicExamsController(IToeicExamAppService toeicExamAppService)
        {
            _toeicExamAppService = toeicExamAppService;
        }

        //Hiển thị giao diện tải trang web lên
        public ActionResult Index()
        {
            return View();
        }

        //Khi Giảng viên bấm nút "Tải lên"
        [HttpPost]
        public async Task<ActionResult> UploadExam(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "Vui lòng chọn file!";
                    return RedirectToAction("Index");
                }

                if (Path.GetExtension(file.FileName).ToLower() != ".docx")
                {
                    TempData["Error"] = "Chỉ chấp nhận file .docx!";
                    return RedirectToAction("Index");
                }

                int? examId = null;

                //Đọc file
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    var dto = new CreateExamFromWordDto
                    {
                        FileName = file.FileName,
                        FileBytes = memoryStream.ToArray()
                    };

                    //TẠM THỜI: nếu service chưa return ID thì giữ nguyên dòng này
                    //await _toeicExamAppService.UploadAndParseExamAsync(dto);

                    //Sau này nếu service trả ID thì mở dòng này
                    examId = await _toeicExamAppService.UploadAndParseExamAsync(dto);
                }

                TempData["Success"] = "Upload & bóc tách thành công!";

                //Nếu có ID → đi Preview
                if (examId.HasValue)
                {
                    return RedirectToAction("Preview", new { id = examId.Value });
                }

                //Nếu chưa có → quay về Index
                return RedirectToAction("Index");
            }
            catch (UserFriendlyException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Có lỗi xảy ra khi xử lý file!";
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Preview(int id)
            {
                var exam = await _toeicExamAppService.GetExamForPreview(id);
                return View(exam);
            }

        public async Task<ActionResult> List()
        {
            var exams = await _toeicExamAppService.GetExamListAsync();
            return View(exams);
        }

        public async Task<ActionResult> Exam(int id)
        {
            var exam = await _toeicExamAppService.GetExamForPreview(id);
            return View(exam);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitExam(int examId, List<UserAnswer> answers)
        {
            var result = await _toeicExamAppService.CalculateScore(examId, answers);
            return View("Result", result);
        }
    }
}