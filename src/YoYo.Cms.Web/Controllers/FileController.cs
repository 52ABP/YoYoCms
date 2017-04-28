using Abp.Auditing;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using YoYo.Cms.Dto;
using System.IO;
using System.Web.Mvc;

namespace YoYo.Cms.Web.Controllers
{
    [AbpMvcAuthorize]
    public class FileController : CmsControllerBase
    {
        private readonly IAppFolders _appFolders;
        public FileController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        [AbpMvcAuthorize]
        [DisableAuditing]
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }
    }
}