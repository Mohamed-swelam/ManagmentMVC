using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Mapping;
using lab1.MiddleWares;
using lab1.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );


            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(Repository<>));
            builder.Services.AddScoped<IStudentRepo, StudentRepository>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepoistory>();
            builder.Services.AddScoped<ICourseRepo, CourseRepository>();
            builder.Services.AddScoped<IInstructorRepo, InstructorRepository>();
            builder.Services.AddScoped<IStud_CoursesRepo, Stud_CoursesRepository>();

            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            MapsterConfig.RegisterMappings();
            builder.Services.AddMapster();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseMiddleware<CustomLoggingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Student}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
