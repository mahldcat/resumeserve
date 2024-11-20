using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class Company
{
    [JsonPropertyName("startDate")] 
    public string StartDate { get; set; }

    [JsonPropertyName("endDate")] 
    public string EndDate { get; set; }
    
    [JsonPropertyName("companyName")] 
    public string CompanyName { get; set; }

    [JsonPropertyName("jobTitle")] 
    public string JobTitle { get; set; }

    [JsonPropertyName("teamName")] 
    public string TeamName { get; set; }

    [JsonPropertyName("jobFunctions")] 
    public IDictionary<string, IList<string>> JobFunctions { get; set; }

}