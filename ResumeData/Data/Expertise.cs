using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Expertise: DataFile<Expertise>
{
    protected override string FileName => "bulletbreakdowns.json";


    [JsonPropertyName("technology")]
    public string[] Technology { get; set; }
    
    [JsonPropertyName("languages")]
    public string[] Languages { get; set; }
    
    [JsonPropertyName("devPractices")]
    public string[] DevPractices { get; set; }

    [JsonPropertyName("hobbies")]
    public Hobbies[] Hobbies { get; set; }
    
}

public class Hobbies
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("properties")]
    public string[] Properties { get; set; }

}
