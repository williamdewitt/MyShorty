using System.ComponentModel.DataAnnotations.Schema;

namespace MyShorty.Api.Repositories.Dtos;

public class UrlDto
{
  [Column("id")]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Column("short_url")]
  public string ShortUrl { get; set; }

  [Column("origional_url")]
  public string OriginalUrl { get; set; }
}