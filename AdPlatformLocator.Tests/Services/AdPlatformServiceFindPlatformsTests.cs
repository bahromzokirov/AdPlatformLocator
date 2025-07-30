using AdPlatformLocator.Models;
using AdPlatformLocator.Services;

namespace AdPlatformLocator.Tests.Services;

public class AdPlatformServiceFindPlatformsTests
{
    private readonly AdPlatformService _service;

    public AdPlatformServiceFindPlatformsTests()
    {
        _service = new AdPlatformService();

        var platformsField = typeof(AdPlatformService)
            .GetField("_platforms", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        platformsField?.SetValue(_service, new List<AdPlatform>
        {
            new() { Name = "Яндекс.Директ", Locations = ["/ru"] },
            new() { Name = "Крутая реклама", Locations = ["/ru/svrd"] },
            new() { Name = "Ревдинский рабочий", Locations = ["/ru/svrd/revda", "/ru/svrd/pervik"] },
            new() { Name = "Газета уральских москвичей", Locations = ["/ru/msk"] }
        });
    }

    [Theory]
    [InlineData("/ru", new[] { "Яндекс.Директ" })]
    [InlineData("/ru/svrd", new[] { "Яндекс.Директ", "Крутая реклама" })]
    [InlineData("/ru/svrd/revda", new[] { "Яндекс.Директ", "Крутая реклама", "Ревдинский рабочий" })]
    [InlineData("/ru/msk", new[] { "Яндекс.Директ", "Газета уральских москвичей" })]
    [InlineData("/us", new string[0])]
    public void FindPlatformsForLocation_ReturnsExpected(string location, string[] expected)
    {
        var result = _service.FindPlatformsForLocation(location);
        Assert.Equal(expected.OrderBy(x => x), result.OrderBy(x => x));
    }
}