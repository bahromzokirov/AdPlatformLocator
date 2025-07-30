using AdPlatformLocator.Services;
using Microsoft.AspNetCore.Http;

namespace AdPlatformLocator.Tests.Services;

public class AdPlatformServiceLoadFromFileTests
{
    [Fact]
    public void LoadFromFile_ParsesValidData()
    {
        // Arrange
        var content = "Яндекс.Директ:/ru\nКрутая реклама:/ru/svrd";
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        IFormFile file = new FormFile(stream, 0, bytes.Length, "Data", "test.txt");

        var service = new AdPlatformService();

        // Act
        service.LoadFromFile(file);
        var result = service.FindPlatformsForLocation("/ru/svrd");

        // Assert
        Assert.Contains("Яндекс.Директ", result);
        Assert.Contains("Крутая реклама", result);
    }

    [Fact]
    public void LoadFromFile_IgnoresInvalidLines()
    {
        var content = "Неверная строка без двоеточия\nПравильная:/ru";
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        IFormFile file = new FormFile(stream, 0, bytes.Length, "Data", "bad.txt");

        var service = new AdPlatformService();
        service.LoadFromFile(file);
        var result = service.FindPlatformsForLocation("/ru");

        Assert.Single(result);
        Assert.Contains("Правильная", result);
    }
}