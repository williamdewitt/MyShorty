using Microsoft.EntityFrameworkCore;
using MyShorty.Api.Repositories.Contexts;
using MyShorty.Api.Repositories.Dtos;

namespace MyShorty.Api.Repositories;

public class MyShortyRepository : IMyShortyInterface
{
  private readonly MyShortyContext _myShortyContext;

  public MyShortyRepository(MyShortyContext myShortyContext)
  {
    _myShortyContext = myShortyContext;
  }

  public async Task<UrlDto> SaveUrlRecord(string shortUrl, string originalUrl)
  {
    var record = new UrlDto
    {
      ShortUrl = shortUrl,
      OriginalUrl = originalUrl
    };

    await _myShortyContext.AddAsync(record);
    await _myShortyContext.SaveChangesAsync();

    return record;
  }

  public async Task<string> GetOriginalUrl(string shortUrl)
  {
    var record = await _myShortyContext.Urls.Where(u => u.ShortUrl.Equals(shortUrl)).FirstOrDefaultAsync();
    return record?.OriginalUrl;
  }
}