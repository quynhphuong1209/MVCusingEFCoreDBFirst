using Microsoft.AspNetCore.Mvc;
using MVCusingEFCoreDBFirst.Models;
using System.Diagnostics;

namespace MVCusingEFCoreDBFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //khai báo biến chứa thông tin tạm thời khi tạo mới 1 blog
        private static Blog? temp;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        //đáp ứng  hành động tạo blog
        [HttpGet]
        public IActionResult CreateBlog()
        {
            return View();
        }
        //đáp hành động submit blog
        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            temp = blog;
            return RedirectToAction(nameof(Details));
        }
        //đáp ứng hành động hiển thị thông tin blog sau khi submit
        public IActionResult Details()
        {
            return View(temp);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

    }
}
