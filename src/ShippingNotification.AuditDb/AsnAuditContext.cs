using Microsoft.EntityFrameworkCore;
using ShippingNotification.AuditDb.Models;

namespace ShippingNotification.AuditDb;

public class AsnAuditContext : DbContext
{
    public DbSet<Box> Boxes { get; set; }
    private string DbPath { get; }
    
    public AsnAuditContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        // Should read DbPath from config.
        DbPath = Path.Join(path, "asn_audit.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}