using Microsoft.EntityFrameworkCore;
using SchoolsApp.Models;

namespace SchoolsApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        DbSet<Gender> Genders { get; set; }

        DbSet<Student> Students { get; set; }

        DbSet<School> Schools { get; set; }
    }
}
