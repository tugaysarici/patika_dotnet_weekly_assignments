using Microsoft.EntityFrameworkCore;

namespace Auto_Gallery.Models;

public class AutoContext : DbContext
{
    public AutoContext(DbContextOptions<AutoContext> options) : base(options)
    {
    }

    public DbSet<Auto> Autos { get; set; } = null;
}
