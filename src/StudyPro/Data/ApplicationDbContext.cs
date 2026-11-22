using Microsoft.EntityFrameworkCore;
using StudyPro.Models;

namespace StudyPro.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();
}
