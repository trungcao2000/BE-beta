using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.IO;
using Microsoft.EntityFrameworkCore;
using WebAPI_SQL.Data;
using WebAPI_SQL.Interfaces;
using WebAPI_SQL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;


namespace WebAPI_SQL.Repositories
{
    public class ResFolder: I_Folder
    {
        private readonly DBContextClass _dbContext;

        public ResFolder(DBContextClass dbContext)
        {
            this._dbContext = dbContext;
        }
  
        public async Task<IEnumerable<FolderVM>> GetFolderListAsync()
        {
            var res = await _dbContext.TB_FOLDER.ToListAsync();
            return res;
        }
        public async Task<IEnumerable<UploadFile>> GetUploadListAsync()
        {
            var res = await _dbContext.TB_FILE.ToListAsync();
            return res;
        }
        public async Task<IEnumerable<UploadFile>> GetUploadByIdAsync(int FolderId)
        {
            var res = await _dbContext.TB_FILE.Where(f=>f.FolderId== FolderId).ToListAsync();
            return res;
        }
        public async Task<FolderVM> GetFolderByIdAsync(int id)
        {
            var res = await _dbContext.TB_FOLDER.FindAsync(id);
            if (res != null)
            {
                return res;
            }
            return null;
        }

        public async Task<IEnumerable<UploadFile>> SearchFileName(string FileName)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                var res = await _dbContext.TB_FILE.Where(f => f.FileName.Contains(FileName)).ToListAsync();
                return res;
            }
            return null;
        }
        public int CountFile(int FolderId)
        {
            var _count = _dbContext.TB_FILE.Count(f=>f.FolderId== FolderId);
            return _count;
        }
        public int UploadFile(int FolderId,string FileName, IFormFile file)
        {
            var _updateFile = _dbContext.TB_FOLDER.Find(FolderId);
            int result = -1;
            if (_updateFile != null)
            {
                FileInfo fi = new FileInfo(file.FileName);
                string FileExtension = fi.Extension;
                string uniqueFileName = Guid.NewGuid().ToString()+FileExtension;

                var filePath = Directory.GetCurrentDirectory() + $"/wwwroot/File/{FolderId}";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var path = Path.Combine(filePath, Path.GetFileName(uniqueFileName));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var _addfile = new UploadFile
                {
                    FolderId = FolderId,
                    FileName= FileName,
                    FilePath = $"File/{FolderId}/{uniqueFileName}",
                    FileType = $"{FileExtension}",
                    DateUpload = DateTime.Now
                };
                _dbContext.TB_FILE.Add(_addfile);
                _dbContext.SaveChanges();
                result = 1;
            }
            return result;
        }
        public async Task<FolderVM> InsertFolderAsync(Folder folder)
        {
            var _addfolder = new FolderVM
            {
                FolderName=folder.FolderName,
                DateCreate=DateTime.Now,
                DateEdit=DateTime.Now
            };
            await _dbContext.TB_FOLDER.AddAsync(_addfolder);
            await _dbContext.SaveChangesAsync();
            return _addfolder;
        }

        public async Task<FolderVM> UpdateFolderAsync(int id, Folder folder)
        {
            var _updateFolder = await _dbContext.TB_FOLDER.FindAsync(id);
            if (_updateFolder != null)
            {
                _updateFolder.FolderName = folder.FolderName;
                _updateFolder.DateEdit= DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return _updateFolder;
            }
            return null;
        }

        public bool DeleteFolderAsync(int FolderIdOld, int FolderIdNew)
        {
            var deleteFolder = _dbContext.TB_FOLDER.Find(FolderIdOld);
            if (deleteFolder != null)
            {
                var updateFile = _dbContext.TB_FILE.SingleOrDefault(f=>f.FolderId == FolderIdOld);
                if(updateFile != null)
                {
                updateFile.FolderId = FolderIdNew;
                }
                _dbContext.TB_FOLDER.Remove(deleteFolder);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
