using Core.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Data;
public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Song>? Song;
}
