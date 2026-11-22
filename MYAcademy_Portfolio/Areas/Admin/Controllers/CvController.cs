using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CvController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CvController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string CvPath => Path.Combine(_env.WebRootPath, "files", "cv.pdf");

        public IActionResult Index()
        {
            if (System.IO.File.Exists(CvPath))
            {
                var info = new FileInfo(CvPath);
                ViewBag.HasCv = true;
                ViewBag.UploadDate = info.LastWriteTime.ToString("dd MMMM yyyy HH:mm");
                ViewBag.SizeKb = (info.Length / 1024.0).ToString("0");
            }
            else
            {
                ViewBag.HasCv = false;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(IFormFile cvFile)
        {
            if (cvFile == null || cvFile.Length == 0)
            {
                TempData["Error"] = "Lütfen bir dosya seçin.";
                return RedirectToAction("Index");
            }

            var extension = Path.GetExtension(cvFile.FileName).ToLowerInvariant();
            if (extension != ".pdf" || cvFile.ContentType != "application/pdf")
            {
                TempData["Error"] = "Yalnızca PDF dosyası yükleyebilirsiniz.";
                return RedirectToAction("Index");
            }

            if (cvFile.Length > 5 * 1024 * 1024)
            {
                TempData["Error"] = "Dosya en fazla 5 MB olabilir.";
                return RedirectToAction("Index");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(CvPath)!);
            using (var stream = new FileStream(CvPath, FileMode.Create))
            {
                cvFile.CopyTo(stream);
            }

            TempData["Success"] = "CV güncellendi.";
            return RedirectToAction("Index");
        }
    }
}