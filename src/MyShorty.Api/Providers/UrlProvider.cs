namespace MyShorty.Api.Providers;

public static class UrlProvider
{
  public static string CleanUri(string uri)
  {
    var uriBuilder = new UriBuilder(uri);
    var scheme = uriBuilder.Scheme;

    var url = uriBuilder.Uri.AbsoluteUri
      .Replace(scheme, string.Empty)
      .Replace("://", string.Empty)
      .Replace("www.", string.Empty);

    return url;
  }
}