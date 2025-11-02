using MyShorty.Api.Repositories.Dtos;

namespace MyShorty.Api.Repositories;

public interface IMyShortyInterface
{
  public Task<UrlDto> SaveUrlRecord(string shortUrl, string originalUrl);
  
  public Task<string> GetOriginalUrl(string shortUrl);
}