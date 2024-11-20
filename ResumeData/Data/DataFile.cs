using System.Text.Json;

namespace ResumeData.Data;

public abstract class DataFile<T> where T:DataFile<T>,new()
{
    protected abstract string FileName { get; }

    public static T? FromFile() 
    {  
        string filePath = $".\\data\\{new T().FileName}.json";

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
            
        string rawData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(rawData);
    }
    
}