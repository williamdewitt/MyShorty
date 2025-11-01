using System.ComponentModel.DataAnnotations;

namespace MyShorty.Api.Configuration;

public class EncodingOptions
{
  public const string SectionKey = "Encoding";

  [Range(6, 8)]
  public int CharactersForId { get; set; }
}