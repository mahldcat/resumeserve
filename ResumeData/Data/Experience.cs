using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Experience : DataFile<Experience>
{
    protected override string FileName => "experience.json";
    
    [JsonPropertyName("jobExperience")] 
    public IList<Company> JobExperience { get; set; }

}