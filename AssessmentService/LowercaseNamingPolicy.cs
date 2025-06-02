using System.Text.Json;
namespace AssessmentService;

public class LowercaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToLowerInvariant();
    }
}
