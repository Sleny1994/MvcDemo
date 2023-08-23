using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using MvcDemo.Models;
using Microsoft.AspNetCore.Http.Features;

namespace MvcDemo.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OneFileUpload(IFormFile file)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads", string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return Ok("上传成功");
        }

        public IActionResult MoreFileUpload(IFormFile[] files)
        {
            foreach (var file in files)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads", string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            return Ok("上传成功");
        }

        public IActionResult FileWithContentUpload(Product product)
        {
            var file = product.File;
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads", string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return Ok($"{product.Name} 上传成功");
        }

        /// 大文件上传
        [DisableRequestSizeLimit]
        public async Task<IActionResult> BigFileUploadAsync()
        {
            var contentType = Request.ContentType;
            if (!MultipartRequestHelper.IsMultipartContentType(contentType))
            {
                ModelState.AddModelError("File",
                    $"上传文件类型不对.");
                return BadRequest(ModelState);
            }
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");

            var _defaultFormOptions = new FormOptions();
            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var fileName = MultipartRequestHelper.GetFileName(contentDisposition);
                        var loadBufferBytes = 1024;//这个是每一次从Http请求的section中读出文件数据的大小，单位是Byte即字节，这里设置为1024的意思是，每次从Http请求的section数据流中读取出1024字节的数据到服务器内存中，然后写入下面targetFileStream的文件流中，可以根据服务器的内存大小调整这个值。这样就避免了一次加载所有上传文件的数据到服务器内存中，导致服务器崩溃。

                        using (var targetFileStream = new FileStream(path + "\\" + string.Format("{0}_{1}", DateTime.Now.Ticks, fileName), FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (section.Body)
                            {
                                //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据（如果section.Body.Read方法返回0，表示数据流已经到末尾，数据已经全部都读取完了），再将数据写入到targetFileStream
                                await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                            }
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            return Ok("上传成功");
        }
    }
}
