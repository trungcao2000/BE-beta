//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using System;
//using WebAPI_SQL.Data;
//using WebAPI_SQL.Interfaces;
//using WebAPI_SQL.Models;

//namespace WebAPI_SQL.Repositories
//{
//    public class ResProcProfile : I_Proc_Profile
//    {
//        private readonly DBContextClass _dbContext;
//        private readonly IWebHostEnvironment _environment;

//        public ResProcProfile(DBContextClass dbContext)
//        {
//            this._dbContext = dbContext;
//        }

//        public async Task<List<Profile>> GetProfileListAsync()
//        {
//            return await _dbContext.Profiles
//                .FromSqlRaw<Profile>("P_PROFILES_SELECT_ALL")
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<Profile>> GetProfileByIdAsync(int id)
//        {
           
//            var param = new SqlParameter("@Id", id);

//            var profileDetails = await Task.Run(() => _dbContext.Profiles
//                            .FromSqlRaw(@"exec P_PROFILES_SELECT_ID @Id", param).ToListAsync());

//            return profileDetails;
//        }

//        public async Task<int> AddProfileAsync(Profile profile)
//        {
//            var parameter = new List<SqlParameter>();
//            parameter.Add(new SqlParameter("@Name", profile.name));
//            parameter.Add(new SqlParameter("@Avatar", profile.avatar));
//            parameter.Add(new SqlParameter("@Sex", profile.sex));
//            parameter.Add(new SqlParameter("@Phone", profile.phone));
//            parameter.Add(new SqlParameter("@Email", profile.email));


//            var result = await Task.Run(() => _dbContext.Database
//           .ExecuteSqlRawAsync(@"exec P_PROFILES_INSERT @Name, @Avatar, @Sex, @Phone, @Email", parameter.ToArray()));

//            return result;
//        }

//        public async Task<int> UpdateProfileAsync(int id, Profile profile)
//        {
//            var parameter = new List<SqlParameter>();
//            parameter.Add(new SqlParameter("@Id", id));
//            parameter.Add(new SqlParameter("@Name", profile.name));
//            parameter.Add(new SqlParameter("@Avatar", profile.avatar));
//            parameter.Add(new SqlParameter("@Sex", profile.sex));
//            parameter.Add(new SqlParameter("@Phone", profile.phone));
//            parameter.Add(new SqlParameter("@Email", profile.email));

//            var result = await Task.Run(() => _dbContext.Database
//            .ExecuteSqlRawAsync(@"exec P_PROFILES_UPDATE @Id, @Name, @Avatar, @Sex, @Phone, @Email", parameter.ToArray()));
//            return result;
//        }

//        public async Task<int> DeleteProfileAsync(int id)
//        {
//            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"P_PROFILES_DELETE {id}"));
//        }
        
//    }
//}
