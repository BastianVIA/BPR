using System.Runtime.InteropServices.JavaScript;
using LINTest.Models;
using Newtonsoft.Json;

namespace LINTest.Services;

public class FileProcessingStateManager
{
    private readonly string _lastProcessedDateTimePath;
    
    public FileProcessingStateManager(StateManagerOptions options)
    {
        _lastProcessedDateTimePath = options.LastProcessedDateTimePath ?? throw new ArgumentNullException(nameof(options.LastProcessedDateTimePath));
    }

    public DateTime? LoadLastProcessedDateTime()
    {
        if (File.Exists(_lastProcessedDateTimePath))
        {
            var jsonData = File.ReadAllText(_lastProcessedDateTimePath);
            var lastProcessedData = JsonConvert.DeserializeObject<LastProcessedData>(jsonData);
            if (lastProcessedData != null)
            {
                return lastProcessedData.LastProcessedTime;
            }
        }
        return null;
    }

    public void SaveLastProcessedDateTime(DateTime datetime)
    {
        var lastProcessed = new LastProcessedData(datetime);
        var jsonData = JsonConvert.SerializeObject(lastProcessed, Formatting.Indented);
        File.WriteAllText(_lastProcessedDateTimePath, jsonData);
    }

    public void ProcessingFailed()
    {
        var previousData = File.ReadAllText(_lastProcessedDateTimePath);
        var lastProcessedData = JsonConvert.DeserializeObject<LastProcessedData>(previousData);
        
        if (lastProcessedData != null)
        {
            lastProcessedData.ConsecutiveFails++;
            var jsonData = JsonConvert.SerializeObject(lastProcessedData, Formatting.Indented);
            File.WriteAllText(_lastProcessedDateTimePath, jsonData);
            return;
        }

        var newData = new LastProcessedData(DateTime.MinValue, 1);
        var newAsJsonData = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(_lastProcessedDateTimePath, newAsJsonData);
    }

    public int GetNumberOfConsecutiveFails()
    {
        if (File.Exists(_lastProcessedDateTimePath))
        {
            var jsonData = File.ReadAllText(_lastProcessedDateTimePath);
            var lastProcessedData = JsonConvert.DeserializeObject<LastProcessedData>(jsonData);
            if (lastProcessedData != null)
            {
                return lastProcessedData.ConsecutiveFails;
            }
        }
        return 0;
    }
}