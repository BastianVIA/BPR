using Newtonsoft.Json;
using LINTest.Services;

public class StateManager
{
    private readonly string _lastProcessedDateTimePath;

    public StateManager(StateManagerOptions options)
    {
        _lastProcessedDateTimePath = options.LastProcessedDateTimePath ?? throw new ArgumentNullException(nameof(options.LastProcessedDateTimePath));
    }

    public DateTime? LoadLastProcessedDateTime()
    {
        if (File.Exists(_lastProcessedDateTimePath))
        {
            var jsonData = File.ReadAllText(_lastProcessedDateTimePath);
            return JsonConvert.DeserializeObject<DateTime?>(jsonData);
        }
        return null;
    }

    public void SaveLastProcessedDateTime(DateTime datetime)
    {
        var jsonData = JsonConvert.SerializeObject(datetime, Formatting.Indented);
        File.WriteAllText(_lastProcessedDateTimePath, jsonData);
    }
    
}