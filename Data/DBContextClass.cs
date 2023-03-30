using Microsoft.EntityFrameworkCore;
using WebAPI_SQL.Models;

namespace WebAPI_SQL.Data
{
    public class DBContextClass:DbContext
    {
        public DBContextClass(DbContextOptions<DBContextClass> opt) : base(opt){}
        public DbSet<FolderVM> TB_FOLDER { get; set; }
        public DbSet<UploadFile> TB_FILE { get; set; }
    }
}
