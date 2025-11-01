namespace MyShorty.Api.Providers;

public static class UrlProvider
{
  public static string CleanUri(string uri)
  {
    var uriBuilder = new UriBuilder(uri);
    var scheme = uriBuilder.Scheme;

    var url = uriBuilder.Uri.OriginalString
      .Replace(scheme, string.Empty)
      .Replace("www.", string.Empty);

    return url;
  }
}