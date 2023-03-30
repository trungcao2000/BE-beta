using Microsoft.AspNetCore.Mvc;
using WebAPI_SQL.Models;

namespace WebAPI_SQL.Interfaces
{
    public interface I_Proc_Profile
    {
        public Task<List<Folder>> GetProfileListAsync();
        public Task<IEnumerable<Folder>> GetProfileByIdAsync(int Id);
        public Task<int> AddProfileAsync(Folder profile);
        public Task<int> UpdateProfileAsync(int Id, Folder profile);
        public Task<int> DeleteProfileAsync(int Id);
    }
}
