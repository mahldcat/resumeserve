using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class BulletPoints
{
    [JsonPropertyName("technology")]
    public string[] Technology { get; set; }
    
    [JsonPropertyName("languages")]
    public string[] Languages { get; set; }
    
    [JsonPropertyName("devPractices")]
    public string[] DevPractices { get; set; }

    [JsonPropertyName("hobbies")]
    public Hobbies[] Hobbies { get; set; }

    public static BulletPoints? FromDataFile
    {
        get
        {
            string breakdownPath = ".\\data\\bulletbreakdowns.json";
            if (!File.Exists(breakdownPath))
            {
                throw new FileNotFoundException($"bullet breakdown file not found: {breakdownPath}");
            }
            
            string rawData = File.ReadAllText(breakdownPath);

            return JsonSerializer.Deserialize<BulletPoints>(rawData);
        }
    }

}

public class Hobbies
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("properties")]
    public string[] Properties { get; set; }

}
