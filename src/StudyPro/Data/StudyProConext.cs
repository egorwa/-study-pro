using Microsoft.EntityFrameworkCore;
using StudyPro.Models;

namespace StudyPro.Data
{
    public class StudyProContext : DbContext
    {
        public StudyProContext(DbContextOptions<StudyProContext> options) : base(options)
        {
        }

        public DbSet<StudyTask> StudyTasks { get; set; }
    }
}