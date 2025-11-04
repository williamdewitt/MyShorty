using MyShorty.Web.Models;
using Refit;

namespace MyShorty.Web.Clients;

public interface IMyShortyClient
{
  [Post("/generateShortUrl")]
  public Task<string> GenerateShortUrl(ShortUrlRequest request);
}