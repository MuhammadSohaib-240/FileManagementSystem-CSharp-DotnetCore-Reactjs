using fms.Models.DTOs;
using fms.Security;
using fms.Services;
using fms.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace fms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        public FilesController(IFileService fileService, JwtService jwtService, IUserService userService)
        {
            _fileService = fileService;
            _jwtService = jwtService;
            _userService = userService;
        }

        private int GetUserIdFromToken()
        {
            if (HttpContext.Items["UserId"] is string userId)
            {
                return int.Parse(userId);
            }

            throw new UnauthorizedAccessException("You need to register an account to access this resource");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileUploadDto)
        {
            try
            {
                int userId = GetUserIdFromToken();
                var fileDetail = await _fileService.UploadFile(fileUploadDto, userId);
                return Ok(fileDetail);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{username}/{folder}/{fileName}")]
        public IActionResult GetFile(string folder, string fileName)
        {
            try
            {
                int userId = GetUserIdFromToken();
                var fileBytes = _fileService.GetFile(folder, fileName, userId);
                var contentType = FileUtils.GetContentType(fileName);
                return File(fileBytes, contentType, fileName);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("")]
        public IActionResult GetAllFiles()
        {
            try
            {
                int userId = GetUserIdFromToken();
                var files = _fileService.GetAllFiles(userId);
                return Ok(files);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("rename/{id}")]
        public async Task<IActionResult> RenameFile(int id, [FromBody] FileRenameDto fileRenameDto)
        {
            try
            {
                int userId = GetUserIdFromToken();
                var fileDetail = await _fileService.RenameFile(id, fileRenameDto.NewFileName, userId);
                return Ok(fileDetail);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                int userId = GetUserIdFromToken();
                await _fileService.DeleteFile(id, userId);
                return Ok("File deleted successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
