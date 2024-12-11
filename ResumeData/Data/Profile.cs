using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Profile : DataFile<Profile>
{
    protected override string FileName => "resumeprofile.json";

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("address")]
    public string Address { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")] 
    public string Phone { get; set; }

    [JsonPropertyName("introParagraph")]
    public string IntroParagraph { get; set; }

}