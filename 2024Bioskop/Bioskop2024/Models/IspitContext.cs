namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!

    public DbSet<Projekcija> Projekcije { get; set; }
    public DbSet<Karta> Karte { get; set; }
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
