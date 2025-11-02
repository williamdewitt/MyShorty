using System.ComponentModel.DataAnnotations;

namespace MyShorty.Api.Configuration;

public class ApplicationOptions
{
  public const string SectionKey = "Application";

  [Required]
  [Url]
  public string BaseUri { get; set; }
}