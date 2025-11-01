using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using MyShorty.Api.Repositories.Dtos;

namespace MyShorty.Api.Repositories.Contexts;

public class MyShortyContext : DbContext
{
  public MyShortyContext(DbContextOptions<MyShortyContext> options) : base(options) { }
  
  public DbSet<UrlDto> Urls { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.Entity<UrlDto>()
        .ToTable("urls");
}