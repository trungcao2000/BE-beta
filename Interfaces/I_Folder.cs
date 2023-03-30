using WebAPI_SQL.Models;

namespace WebAPI_SQL.Interfaces
{
    public interface I_Folder
    {
        public Task<IEnumerable<FolderVM>> GetFolderListAsync();
        public Task<IEnumerable<UploadFile>> GetUploadListAsync();
        public Task<IEnumerable<UploadFile>> GetUploadByIdAsync(int Id);
        public Task<FolderVM> GetFolderByIdAsync(int Id);
        public Task<IEnumerable<UploadFile>> SearchFileName(string FileName);
        public Task<FolderVM> InsertFolderAsync(Folder profile);
        public Task<FolderVM> UpdateFolderAsync(int id, Folder profile);
        public bool DeleteFolderAsync(int FolderIdOld, int FolderIdNew);
        public int UploadFile(int Id, string FileName, IFormFile file);
        public int CountFile(int FolderId);

    }
}
