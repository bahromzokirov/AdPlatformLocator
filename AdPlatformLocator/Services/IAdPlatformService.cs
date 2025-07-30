namespace AdPlatformLocator.Services;

public interface IAdPlatformService
{
    void LoadFromFile(IFormFile file);
    List<string>? FindPlatformsForLocation(string location);
}