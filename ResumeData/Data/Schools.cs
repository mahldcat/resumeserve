using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Schools
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("degree")]
    public string Degree { get; set; }

    [JsonPropertyName("field")]
    public string Field { get; set; }
}