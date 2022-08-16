
namespace HRSystem
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<SecurityStampValidatorOptions>(option =>
{
    option.ValidationInterval = TimeSpan.Zero;
});
            //DBContext + Identity Dbcontext Injaction
            builder.Services.AddDbContext<HRDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("HrDB")));
            builder.Services.AddSignalR();
            builder.Services.AddIdentity<Hr, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            }).AddEntityFrameworkStores<HRDbContext>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepatrmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IHrRepository, HrRepository>();
            builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
            builder.Services.AddScoped<IGeneralSettingRepository, GeneralSettingRepository>();
            builder.Services.AddScoped<IWeeklyHolidayRepository, WeeklyHolidayRepository>();
            builder.Services.AddScoped<IExceptionRepository, ExceptionRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IHrService, HrService>();
            builder.Services.AddScoped<ISalaryService, SalaryService>();
            builder.Services.AddScoped<IGeneralSettingService, GeneralSettingService>();
            builder.Services.AddScoped<IWeeklyHolidayService, WeeklyHolidayService>();
            builder.Services.AddScoped<IExceptionService, ExceptionService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<IChatService, ChatService>();
            //this two services for authorization , please don't delete them
            //**************
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //**************

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapHub<ChatHub>("OnlineChat"));
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

        }
    }
}