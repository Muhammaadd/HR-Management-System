namespace HRSystem.Models
{
    public class HRDbContext : IdentityDbContext<Hr>
    {
        private readonly IConfiguration configuration;

        public HRDbContext()
        {

        }
        public HRDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Database=HrSystem;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ExceptionAttendance> Exceptions { get; set; }
        public DbSet<WeeklyHoliday> WeeklyHolidays { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<UsersMessages> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeeklyHoliday>().HasKey(c => new { c.GeneralId, c.Day });
            base.OnModelCreating(modelBuilder);
        }
    }
}
