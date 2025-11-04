using MyShorty.Web.Clients;
using MyShorty.Web.Models;

namespace MyShorty.Web.Services;

public class MyShortyService
{
  private readonly IMyShortyClient _myShortyClient;

  public MyShortyService(IMyShortyClient myShortyClient)
  {
    _myShortyClient = myShortyClient;
  }

  public async Task<string> GenerateShortUrl(string originalUrl)
  {
    return await _myShortyClient.GenerateShortUrl(new ShortUrlRequest() { Url = originalUrl });
  }
}