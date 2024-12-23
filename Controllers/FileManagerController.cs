using elFinder.NetCore.Drivers.FileSystem;
using elFinder.NetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MVCusingEFCoreDBFirst.Controllers
{
    [Route("quan-ly-tep-tin")]
    public class FileManagerController : Controller
    {
        readonly IWebHostEnvironment _env;
        public FileManagerController(IWebHostEnvironment env) => _env = env;
        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            var result = await connector.ProcessAsync(Request);
            return result;
        }
        // Hiển thị ảnh thumbnail
        // /el-finder-file-system/thumb
        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }
        private Connector GetConnector()
        {
            // Thư mục gốc lưu trữ là wwwwroot/files (đảm bảo có tạo thư mục này)
            string pathroot = "files";
            var driver = new FileSystemDriver();
            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);
            // .. ... wwwwroot/files
            string rootDirectory = Path.Combine(_env.WebRootPath, pathroot);
            // https://localhost:5001/files/
            string url = $"{uri.Scheme}://{uri.Authority}/{pathroot}/";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/quan-ly-tep-tin/thumb/";
            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "files", // Beautiful name given to the wwwroot/files folder
                                 //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                                 //LockedFolders = new List<string>(new string[] { "Folder1" }
                ThumbnailSize = 100,
            };
            driver.AddRoot(root);
            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }

    }
}
