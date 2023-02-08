using FlowCSFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowCSFinal.Data;

public class FlowCSDbContext : IdentityDbContext<AppUser>
{
    public FlowCSDbContext(DbContextOptions<FlowCSDbContext> options)
        : base(options) {}

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<FlowCSFinal.Models.Category> Category { get; set; }

    public DbSet<FlowCSFinal.Models.Templates> Templates { get; set; }

    public DbSet<FlowCSFinal.Models.Collections> Collections { get; set; }

    public DbSet<FlowCSFinal.Models.About> About { get; set; }

    public DbSet<FlowCSFinal.Models.Contacts> Contacts { get; set; }

    public DbSet<FlowCSFinal.Models.Redirect> Redirect { get; set; }
    public DbSet<FlowCSFinal.Models.Workspace> Workspace { get; set; }
    public DbSet<FlowCSFinal.Models.Board> Board { get; set; }
    public DbSet<FlowCSFinal.Models.List> List { get; set; }
}
