using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class ProRefer
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("linkedIn")]
    public string LinkedIn { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}