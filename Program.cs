using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using WebAPI_SQL.Data;
using WebAPI_SQL.Interfaces;
using WebAPI_SQL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{ 
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Demo V1", Version = "v1" });
});
// them 3 dong duoi

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddScoped<I_Proc_Profile, ResProcProfile>();
builder.Services.AddScoped<I_Folder, ResFolder>();
builder.Services.AddDbContext<DBContextClass>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApi"));
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
});
app.UseCors("corspolicy");
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot/File")),
    RequestPath = "/File"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot/Iconfile")),
    RequestPath = "/Icon"
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
