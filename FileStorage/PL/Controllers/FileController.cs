using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    /// <summary>
    /// File controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {

        private IFileService _fileService;
        private IAutorizationService _authorizationService;

        public FileController(IFileService fileService, IAutorizationService authorizationService)
        {
            _fileService = fileService;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Method for authorize user for create new file
        /// </summary>
        /// <param name="Username">User login</param>
        /// <param name="SecureLevelName">File secure type (private, public)</param>
        /// <returns>Ok if success and bad request if not</returns>
        [HttpPost("CreateFile/{Username}/{SecureLevelName}")]
        [Authorize]
        public async Task<IActionResult> CreateFile(string Username, string SecureLevelName)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string[] fileNameArray = file.FileName.Split('.');
                            string extention = fileNameArray[fileNameArray.Length - 1];
                            string fileName = "";
                            for (int i = 0; i < fileNameArray.Length - 1; i++)
                            {
                                fileName += fileNameArray[i];
                            }
                            await _fileService.AddAsync(new FullFileInfo
                            {
                                File = new DAL.Entities.File
                                {
                                    FileName = fileName,
                                    FileStreamCol = fileBytes,
                                    FileCreateDate = DateTime.Now,
                                    FileType = new FileType
                                    {
                                        TypeName = extention
                                    }
                                },
                                FileSecureLevel = new FileSecureLevel
                                {
                                    SecureLevelName = SecureLevelName
                                },
                                User = _authorizationService.GetUserByLogin(Username)
                            });

                        }
                    }
                }
                return Ok("Success");
            }
            catch (Exception)
            {
                return BadRequest("File creating error");
            }
        }
        /// <summary>
        /// Get collection models of files without array of bytes 
        /// </summary>
        /// <returns>Files collection if success and bad request if not</returns>
        [HttpGet("GetSceletonOfPublicFiles")]
        public IActionResult GetSceletonOfPublicFiles()
        {
            try
            {
                var tmp = _fileService.GetAllPublicFiles().ToList();
                List<SceletonFileModel> result = CreateSceletonFromFullFileInfo(tmp);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get file for download by id
        /// </summary>
        /// <param name="id">File id</param>
        /// <returns>File if success and bad request if not</returns>
        [HttpGet("GetPublicFile/{id}")]
        public IActionResult GetPublicFile(int id)
        {
            try
            {

                var tmp = _fileService.GetAllPublicFiles();
                var result = tmp.FirstOrDefault(tmp => tmp.Id == id);
                if (result == null)
                {
                    return BadRequest("This file dont exist");
                }
                return File(result.File.FileStreamCol, "application/octet-stream", $"{result.File.FileName}.{result.File.FileType.TypeName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get collection models of private files without array of bytes by user login for authorize user
        /// </summary>
        /// <param name="Username">User login</param>
        /// <returns>Files collection if success and bad request if not</returns>
        [HttpGet("GetSceletonOfPrivateFiles/{Username}")]
        [Authorize]
        public IActionResult GetSceletonOfPrivateFiles(string Username)
        {
            try
            {
                var tmp = _fileService.GetAllPrivateByUser(_authorizationService.GetUserByLogin(Username)).ToList();
                List<SceletonFileModel> result = CreateSceletonFromFullFileInfo(tmp);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get collection models of all files without array of bytes by user login for authorize user
        /// </summary>
        /// <param name="Username">User login</param>
        /// <returns>Files collection if success and bad request if not</returns>
        [HttpGet("GetSceletonOfAllFiles/{Username}")]
        [Authorize]
        public IActionResult GetSceletonOfAllFiles(string Username)
        {
            try
            {
                var tmp = _fileService.GetAllByUser(_authorizationService.GetUserByLogin(Username)).ToList();
                List<SceletonFileModel> result = CreateSceletonFromFullFileInfo(tmp);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get private file for download by id and user login for authorize user
        /// </summary>
        /// <param name="id">File id</param>
        /// <param name="Username">User login</param>
        /// <returns>File if success and bad request if not</returns>
        [HttpGet("GetPrivateFile/{Username}/{id}")]
        [Authorize]
        public IActionResult GetPrivateFile(int id, string Username)
        {
            try
            {

                var tmp = _fileService.GetAllByUser(_authorizationService.GetUserByLogin(Username));
                var result = tmp.FirstOrDefault(tmp => tmp.Id == id);
                if (result == null)
                {
                    return BadRequest("This file dont exist");
                }
                return File(result.File.FileStreamCol, "application/octet-stream", $"{result.File.FileName}.{result.File.FileType.TypeName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete file by id and user login for authorize user
        /// </summary>
        /// <param name="Username">User login</param>
        /// <param name="id">File id</param>
        /// <returns>Ok if success and bad request if not</returns>
        [HttpDelete("DeleteFile/{Username}/{id}")]
        [Authorize]
        public IActionResult DeleteFileById(string Username, int id)
        {
            try
            {
                if (_authorizationService.GetUserByLogin(Username) != null)
                {
                    _fileService.DeleteAsync(id);
                    return Ok("Success");
                }
                return BadRequest("This user is not exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Create file models without array of bytes 
        /// </summary>
        /// <param name="files">List of real files</param>
        /// <returns>Collection models of files without array of bytes</returns>
        private static List<SceletonFileModel> CreateSceletonFromFullFileInfo(List<FullFileInfo> files)
        {
            List<SceletonFileModel> result = new List<SceletonFileModel>();
            foreach (var item in files)
            {
                result.Add(new SceletonFileModel
                {
                    Id = item.Id,
                    Username = item.User.Login,
                    FileName = item.File.FileName + "." + item.File.FileType.TypeName,
                    CreateDate = item.File.FileCreateDate.ToString(),
                    FileSecureLevel = item.FileSecureLevel.SecureLevelName,
                    Size = item.File.FileStreamCol.Length
                });
            }

            return result;
        }
    }
}
