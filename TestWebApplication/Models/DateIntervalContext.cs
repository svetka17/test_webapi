namespace TestWebApplication.Models
{
    using System.Data.Entity;

    public class DateIntervalContext : DbContext
    {
        public DateIntervalContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<DateInterval> DateIntervals { get; set; }

        public DbSet<AppLog> AppLogs { get; set; }

    }

}