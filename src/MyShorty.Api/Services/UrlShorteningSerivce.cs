using Microsoft.Extensions.Options;
using MyShorty.Api.Configuration;
using MyShorty.Api.Engines;
using MyShorty.Api.Providers;
using MyShorty.Api.Repositories;

namespace MyShorty.Api.Services;

public class UrlShorteningService
{
  private readonly ApplicationOptions _applicationOptions;
  private readonly EncodingOptions _encodingOptions;
  private readonly IMyShortyInterface _myShortyRepository;

  public UrlShorteningService(
    IOptions<ApplicationOptions> applicationOptions,
    IOptions<EncodingOptions> encodingOptions,
    IMyShortyInterface myShortyRepository)
  {
    _applicationOptions = applicationOptions.Value;
    _encodingOptions = encodingOptions.Value;
    _myShortyRepository = myShortyRepository;
  }

  public async Task<string> ShortenUrl(string url)
  {
    var cleanUrl = UrlProvider.CleanUri(url);

    var encodedUrl = UrlShorteningEngine.Encode(cleanUrl);
    var urlId = encodedUrl.Substring(0, _encodingOptions.CharactersForId);

    var existingRecord = await GetOriginalUrl(urlId);

    var urlBuilder = new UriBuilder(_applicationOptions.BaseUri);
    urlBuilder.Path = urlId;

    if(existingRecord != default)
    {
      return urlBuilder.Uri.ToString();
    }

    await _myShortyRepository.SaveUrlRecord(urlId, url);

    return urlBuilder.Uri.ToString();
  }

  public async Task<string> GetOriginalUrl(string shortUrl)
  {
    var originalUrl = await _myShortyRepository.GetOriginalUrl(shortUrl);
    return originalUrl;
  }
}