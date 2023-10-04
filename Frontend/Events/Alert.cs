namespace Frontend.Events;

public class Alert
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public AlertType Type { get; set; }
}