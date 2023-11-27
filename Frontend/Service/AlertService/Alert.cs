using Radzen;

namespace Frontend.Service.AlertService;

public class Alert
{
    public string Message { get; set; }
    public AlertStyle Style { get; set; }

    public static Alert Create(string message, AlertStyle style)
    {
        return new Alert { Message = message, Style = style};
    }
}