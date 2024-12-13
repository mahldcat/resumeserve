using System.Text.Json.Serialization;

namespace ResumeData.Data;

public class ProfessionalReferences:DataFile<ProfessionalReferences>
{
    protected override string FileName => "references.json";
    
    [JsonPropertyName("references")]
    public IList<ProRefer> References { get; set; }

    
}