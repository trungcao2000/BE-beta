using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IO;
using WebAPI_SQL.Interfaces;
using WebAPI_SQL.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_SQL.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly I_Folder _ifolder;

        public FolderController(I_Folder ifolder)
        {
            this._ifolder = ifolder;
        }

        // GET: api/<FolderController>

        [HttpGet]
        [SwaggerOperation("Gets all folder")]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var res = await _ifolder.GetFolderListAsync();
                var res1 = await _ifolder.GetUploadListAsync();
                return Ok(new ObjectFolderFile { Folders = res, Files = res1 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<FolderController>/5

        [HttpGet("{Folder_Id}")]
        [SwaggerOperation("Get folder by id")]
        public async Task<IActionResult> Get(int Folder_Id)
        {
            try
            {
                var res = await _ifolder.GetFolderByIdAsync(Folder_Id);
                var res1 = await _ifolder.GetUploadByIdAsync(Folder_Id);
                if (res != null)
                {
                    return Ok(new ObjectFolderFile { Folders = res, Files = res1 });
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

     

        [HttpGet("Search/{FileName}")]
        [SwaggerOperation("Search file name")]
        public async Task<IActionResult> SearchFile(string FileName)
        {
            try
            {
                var res = await _ifolder.SearchFileName(FileName);
                if (res != null)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet("GetFile/{id}")]
        //public async Task<IActionResult> GetFile(int id)
        //{
        //    string filePath = $"wwwroot/Uploads/{id}";

        //    if (!Directory.Exists(filePath))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        var files = from file in Directory.EnumerateFiles(filePath) select file;
        //        foreach (var file in files)
        //        {
        //            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
        //            return Ok(fs);
        //        }
        //    }
        //    return BadRequest();
        //    //if (!System.IO.File.Exists(filePath))
        //    //{
        //    //   return NotFound();
        //    //}

        //}


        [HttpPost("UploadFile")]
        [SwaggerOperation("Upload file to folder where id?")]
        public  IActionResult UploadFile(int Folder_Id, string FileName, IFormFile file)
        {
            try
            {
                if (Folder_Id is 0 || file.Length == 0 )
                {
                    return BadRequest("IdFile? File? is Null");
                }
                var res = _ifolder.UploadFile(Folder_Id, FileName, file);
                if (res != -1)
                {
                    return Ok("Tải lên thành công");
                }
                return NotFound("Id không tồn tại");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("CountFile")]
        [SwaggerOperation("Count File by FolderId?")]
        public IActionResult CountFile(int Folder_Id)
        {
            try
            {
                if (Folder_Id is 0)
                {
                    return BadRequest("FolderId? is Null");
                }
                var res = _ifolder.CountFile(Folder_Id);
               
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<FolderController>
        [HttpPost]
        [SwaggerOperation("Add folder")]
        public async Task<IActionResult> Post(Folder folder)
        {
            try
            {
                if (folder == null)
                {
                    return BadRequest();
                }
                var res = await _ifolder.InsertFolderAsync(folder);
                return Ok(res);
            }
            catch (Exception e)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
         
        }
       
        // PUT api/<FolderController>/5
        [HttpPut("{Folder_Id}")]
        [SwaggerOperation("Update set folder where id?")]
        public async Task<IActionResult> Put(int Folder_Id, Folder folder)
        {
            try
            {
                var res = await _ifolder.UpdateFolderAsync(Folder_Id, folder);
                if (res != null)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        
        // DELETE api/<FolderController>/5
        [HttpDelete("{FolderIdOld}/{FolderIdNew}")]
        [SwaggerOperation("Before deleting move all files to others folder")]
        public IActionResult Delete(int FolderIdOld, int FolderIdNew)
        {
            try
            {
                if (!_ifolder.DeleteFolderAsync(FolderIdOld, FolderIdNew))
                {
                    return NotFound();
                }
                return Ok("Deleted success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
