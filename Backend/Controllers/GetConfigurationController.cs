using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetConfigurationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public GetConfigurationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet()]
    [Route("api/configuration")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfigurationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public IActionResult GetAsync(CancellationToken cancellationToken)
    {
        var configValidationSettings = _configuration.GetSection("ValidationSettings");
        var woNoLength = configValidationSettings.GetValue<int>("WorkOrderNumberLength");
        var serialNoMinLength = configValidationSettings.GetValue<int>("SerialNumberMinLength");
        var serialNoMaxLenght = configValidationSettings.GetValue<int>("SerialNumberMaxLength");
        var validationSettings = ValidationSettings.From(woNoLength, serialNoMinLength, serialNoMaxLenght);
        return Ok(ConfigurationResponse.From(validationSettings));
    }
}

public class ConfigurationResponse
{
    public ValidationSettings ValidationSettings { get; }
    
    private ConfigurationResponse(){}

    private ConfigurationResponse(ValidationSettings validationSettings)
    {
        ValidationSettings = validationSettings;
    }

    internal static ConfigurationResponse From(ValidationSettings validationSettings)
    {
        return new ConfigurationResponse(validationSettings);
    }
}

public class ValidationSettings
{
    public int WorkOrderNumberLength { get; }
    public int SerialNumberMinLength { get; }
    public int SerialNumberMaxLength { get; }
    
    private ValidationSettings(){}
    private ValidationSettings(int workOrderNumberLength, int serialNumberMinLength, int serialNumberMaxLength)
    {
        WorkOrderNumberLength = workOrderNumberLength;
        SerialNumberMinLength = serialNumberMinLength;
        SerialNumberMaxLength = serialNumberMaxLength;
    }

    internal static ValidationSettings From(int woNoLength, int serialNoMinLength, int serialNoMaxLength )
    {
        return new ValidationSettings(woNoLength, serialNoMinLength, serialNoMaxLength);
    }
}