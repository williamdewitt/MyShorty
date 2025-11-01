using Microsoft.Extensions.Options;
using MyShorty.Api.Configuration;
using MyShorty.Api.Engines;
using MyShorty.Api.Providers;
using MyShorty.Api.Repositories;

namespace MyShorty.Api.Services;

public class UrlShorteningService
{
  private readonly EncodingOptions _options;
  private readonly MyShortyRepository _myShortyRepository;

  public UrlShorteningService(
    IOptions<EncodingOptions> options,
    MyShortyRepository myShortyRepository)
  {
    _options = options.Value;
    _myShortyRepository = myShortyRepository;
  }

  public async Task<string> ShortenUrl(string url)
  {
    var cleanUrl = UrlProvider.CleanUri(url);

    var encodedUrl = UrlShorteningEngine.Encode(cleanUrl);
    var urlId = encodedUrl.Substring(0, _options.CharactersForId);

    var shortUrl = string.Concat("http://localhost:5290", "/", urlId);

    var existingRecord = await GetOriginalUrl(urlId);

    if(existingRecord != default)
    {
      return shortUrl;
    }

    await _myShortyRepository.SaveUrlRecord(urlId, url);

    return shortUrl;
  }

  public async Task<string> GetOriginalUrl(string shortUrl)
  {
    var originalUrl = await _myShortyRepository.GetOriginalUrl(shortUrl);
    return originalUrl;
  }
}