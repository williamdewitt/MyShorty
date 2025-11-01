using System.Text;

namespace MyShorty.Api.Engines;

public static class UrlShorteningEngine
{
  public static string Encode(string input)
  {
    // Convert string to bytes
    var bytes = Encoding.UTF8.GetBytes(input);

    // Base64 encode
    var base64 = Convert.ToBase64String(bytes);

    // Make it Base62-friendly: remove non-URL-safe chars
    return base64
        .Replace('+', '-')
        .Replace('/', '_')
        .Replace("=", ""); // remove padding
  }

  public static string Decode(string base62ish)
  {
    // Restore Base64-safe version
    string base64 = base62ish
        .Replace('-', '+')
        .Replace('_', '/');

    // Pad if needed (Base64 requires multiple of 4 length)
    switch (base64.Length % 4)
    {
      case 2: base64 += "=="; break;
      case 3: base64 += "="; break;
    }

    var bytes = Convert.FromBase64String(base64);
    return Encoding.UTF8.GetString(bytes);
  }
}