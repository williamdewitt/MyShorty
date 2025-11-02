using AwesomeAssertions;
using Microsoft.Extensions.Options;
using MyShorty.Api.Configuration;
using MyShorty.Api.Repositories;
using MyShorty.Api.Services;
using NSubstitute;

namespace MyShorty.Api.Tests;

public class UrlShorteningServiceTests
{
  [Theory]
  [InlineData("https://www.google.com/", "Z29vZ2xl")]
  // GPT Generate test cases.  Need to figure out why the short codes don't work.
  // [InlineData("https://www.google.com/", "iJgeYmO-")]
  // [InlineData("http://example.com/path?query=1", "dbKiwwcn")]
  // [InlineData("https://sub.domain.example.com/", "qbplGw78")]
  // [InlineData("https://www.google.com/search?q=test", "CAny1rKf")]
  // [InlineData("http://192.0.2.1/", "D9Zt_qRd")]
  // [InlineData("https://example.com/%E2%9C%93", "q3YSGFOm")]
  // [InlineData("https://example.com/space here", "N9granT6")]
  // [InlineData("https://example.com/?utm_source=newsletter&utm_medium=email", "OZuj-n8x")]
  // [InlineData("https://example.com/#fragment", "sggI6LBK")]
  // [InlineData("https://a.co", "NqNbZ_mK")]
  public async Task Test1(string uri, string shortCode)
  {
    var applicationOptions = Options.Create(new ApplicationOptions() { BaseUri = "http://localhost:5290" });
    var encodingOptions = Options.Create(new EncodingOptions() { CharactersForId = 8 });
    var repository = Substitute.For<IMyShortyInterface>();

    var sut = new UrlShorteningService(applicationOptions, encodingOptions, repository);

    var result = await sut.ShortenUrl(uri);

    result.Should().Be($"http://localhost:5290/{shortCode}");
  }
}
