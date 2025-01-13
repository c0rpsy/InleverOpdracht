using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


public class DatabaseContext : DbContext
{
    public System.Data.Entity.DbSet<UserProfile> UserProfiles { get; set; }

    private readonly string _dbPath;

    public DatabaseContext(string dbPath)
    {
        _dbPath = dbPath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}

