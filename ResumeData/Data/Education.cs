using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Education:DataFile<Education>
{
    protected override string FileName => "education.json";
    
    [JsonPropertyName("schools")]
    public IList<Schools> Schools { get; set; }
    
}