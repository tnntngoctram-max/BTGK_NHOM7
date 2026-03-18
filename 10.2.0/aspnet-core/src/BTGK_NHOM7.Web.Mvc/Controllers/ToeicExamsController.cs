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
using Microsoft.AspNetCore.Authorization;

namespace BTGK_NHOM7.Web.Controllers
{
    public class ToeicExamsController : BTGK_NHOM7ControllerBase
    {
        private readonly IToeicExamAppService _toeicExamAppService;
        public ToeicExamsController(IToeicExamAppService toeicExamAppService)
        {
            _toeicExamAppService = toeicExamAppService;
        }
        [Authorize(Roles = "Admin,GiangVien,HocVien")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("GiangVien"))
            {
                return View();
            }

            if (User.IsInRole("HocVien"))
            {
                return RedirectToAction("List");
            }

            return Unauthorized();
        }
        //Hiển thị giao diện tải trang web lên
        

        //Khi Giảng viên bấm nút "Tải lên"
        [HttpPost]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> UploadExam(IFormFile file, string title, int? timeMinutes)
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
                    examId = await _toeicExamAppService.UploadAndParseExamAsync(dto, title, timeMinutes ?? 0);
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
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> Preview(int id)
            {
                var exam = await _toeicExamAppService.GetExamForPreview(id);
                return View(exam);
            }

        [Authorize(Roles = "Admin,GiangVien,HocVien")]
        public async Task<ActionResult> List()
        {
            var exams = await _toeicExamAppService.GetExamListAsync();
            return View(exams);
        }

        [Authorize(Roles = "HocVien")]
        public async Task<ActionResult> Exam(int id)
        {
            var exam = await _toeicExamAppService.GetExamForPreview(id);
            return View(exam);
        }

        [HttpPost]
        [Authorize(Roles = "HocVien")]
        public async Task<ActionResult> SubmitExam(int examId, int timeTakenSeconds, List<UserAnswer> answers)
        {
            var result = await _toeicExamAppService.CalculateAndSaveScore(examId, answers, timeTakenSeconds);
            ViewBag.TimeTakenSeconds = timeTakenSeconds;
            return View("Result", result);
        }

    // 1. Action hiển thị trang chỉnh sửa
        [HttpGet]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> Edit(int id)
        {
            var exam = await _toeicExamAppService.GetExamForPreview(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam); // Bạn cần có file View Edit.cshtml tương ứng
        }

        // 2. Action xử lý lưu sau khi chỉnh sửa
        [HttpPost]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> Edit(int id, ToeicExam input)
        {
            // Tạm ẩn đến khi bạn khai báo interface:
            // await _toeicExamAppService.UpdateExamAsync(id, input);
            TempData["Success"] = "Cập nhật đề thi thành công!";
            return RedirectToAction("Preview", new { id = id });
        }

        // 3. Action xử lý xuất bản đề thi
        // Thay đổi để dùng được cho cả thẻ <a> (GET) và <form> (POST)
        [HttpGet, HttpPost]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> Publish(int id)
        {
            // Giả sử thực thể ToeicExam của bạn có trường IsPublished
            // Tạm ẩn đến khi bạn khai báo interface:
            // await _toeicExamAppService.PublishExamAsync(id);
            TempData["Success"] = "Xuất bản đề thi thành công!";
            return RedirectToAction("List");
        }

        // Action lấy thông tin câu hỏi để hiển thị ra View sửa
        [HttpGet]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> EditQuestion(int id, int examId)
        {
            var question = await _toeicExamAppService.GetQuestionAsync(id);
            if (question == null) return NotFound();
            ViewBag.ExamId = examId;
            return View(question);
        }

        // Action lưu thông tin câu hỏi
        [HttpPost]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> EditQuestion(int id, int examId, ToeicQuestion input)
        {
            await _toeicExamAppService.UpdateQuestionAsync(id, input);
            TempData["Success"] = "Cập nhật câu hỏi thành công!";
            return RedirectToAction("Preview", "ToeicExams", new { id = examId }, "question_" + id);
        }

        // 6. Action Xóa đề thi
        [HttpPost]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<ActionResult> Delete(int id)
        {
            await _toeicExamAppService.DeleteExamAsync(id);
            TempData["Success"] = "Xóa đề thi thành công!";
            return RedirectToAction("List");
        }

        // 7. Action Xem lịch sử làm bài (Học viên)
        [HttpGet]
        [Authorize(Roles = "HocVien")]
        public async Task<ActionResult> History()
        {
            var userHistory = await _toeicExamAppService.GetUserHistoryAsync();
            return View(userHistory);
        }
    }
}