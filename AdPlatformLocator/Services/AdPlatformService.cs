using AdPlatformLocator.Models;

namespace AdPlatformLocator.Services;

public class AdPlatformService : IAdPlatformService
{
    private readonly List<AdPlatform> _platforms = new();

    public void LoadFromFile(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        _platforms.Clear();
        
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if(string.IsNullOrWhiteSpace(line) || !line.Contains(":")) continue;
            
            var parts = line.Split(':', 2);
            var name = parts[0].Trim();
            var location = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .ToList();
            
            if(!string.IsNullOrWhiteSpace(name) && location.Count > 0) 
                _platforms.Add(new AdPlatform { Name = name, Locations = location });
        }
    }

    public List<string> FindPlatformsForLocation(string? location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return new List<string>();
        
        return _platforms
            .Where(p => p.Locations != null && p.Locations.Any(loc => location.StartsWith(loc)))
            .Select(p => p.Name ?? string.Empty) 
            .Distinct()
            .ToList();
    }
}