//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using WebAPI_SQL.Interfaces;
//using WebAPI_SQL.Models;
//using WebAPI_SQL.Repositories;

//namespace WebAPI_SQL.Controllers
//{
//    [Route("/[controller]")]
//    [ApiController]
//    public class ProcedureProfileController : ControllerBase
//    {
//        private readonly I_Proc_Profile _iprofile;

//        public ProcedureProfileController(I_Proc_Profile iprofile)
//        {
//            this._iprofile = iprofile;
//        }

//        [HttpGet]
//        public async Task<List<ProfileModel>> GetProfileListAsync()
//        {
//            try
//            {
//                return await _iprofile!.GetProfileListAsync();
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IEnumerable<ProfileModel>> GetProfileByIdAsync(int id)
//        {
//            try
//            {
//                var response = await _iprofile!.GetProfileByIdAsync(id);

//                if (response == null)
//                {
//                    return null;
//                }

//                return response;
//            }
//            catch
//            {
//                throw;
//            }
//        }
//        //[HttpPost]
//        //public async Task<IActionResult> UploadImage(IFormFile file)
//        //{
//        //    string FileName = file.FileName;
//        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
//        //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Images/", uniqueFileName);
//        //    file.CopyTo(new FileStream(imagePath, FileMode.Create));
        
//        //    return Ok(uniqueFileName);
//        //}

//        [HttpPost]
//        public async Task<IActionResult> AddProfileAsync(ProfileModel profile)
//        {
//            if (profile == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                var response = await _iprofile!.AddProfileAsync(profile);

//                return Ok(response);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateProfileAsync(int id, ProfileModel profile)
//        {
//            if (profile == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                var result = await _iprofile!.UpdateProfileAsync(id, profile);
//                return Ok(result);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProfileAsync(int id)
//        {
//            try
//            {
//                var response = await _iprofile!.DeleteProfileAsync(id);
//                return Ok(response);
//            }
//            catch
//            {
//                throw;
//            }
//        }
//    }
//}
