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
    [Tags("Configuration")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfigurationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public IActionResult GetAsync(CancellationToken cancellationToken)
    {
        var configValidationSettings = _configuration.GetSection("ValidationSettings");
        var woNoLength = configValidationSettings.GetValue<int>("WorkOrderNumberLength");
        var serialNoMinLength = configValidationSettings.GetValue<int>("SerialNumberMinLength");
        var serialNoMaxLenght = configValidationSettings.GetValue<int>("SerialNumberMaxLength");
        var productionDateCodeMinLength = configValidationSettings.GetValue<int>("ProductionDateCodeMinLength");
        var productionDateCodeMaxLength = configValidationSettings.GetValue<int>("ProductionDateCodeMaxLength");


        var validationSettings = ValidationSettings.From(woNoLength, serialNoMinLength,
            serialNoMaxLenght, productionDateCodeMinLength,
            productionDateCodeMaxLength);
        return Ok(ConfigurationResponse.From(validationSettings));
    }
}

public class ConfigurationResponse
{
    public ValidationSettings ValidationSettings { get; }

    private ConfigurationResponse()
    {
    }

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
    public int ProductionDateCodeMinLength { get; }
    public int ProductionDateCodeMaxLength { get; }


    private ValidationSettings()
    {
    }

    private ValidationSettings(int woNoLength, int serialNoMinLength,
        int serialNoMaxLength, int productionDateCodeMinLength, int productionDateCodeMaxLength)
    {
        WorkOrderNumberLength = woNoLength;
        SerialNumberMinLength = serialNoMinLength;
        SerialNumberMaxLength = serialNoMaxLength;
        ProductionDateCodeMinLength = productionDateCodeMinLength;
        ProductionDateCodeMaxLength = productionDateCodeMaxLength;
    }

    internal static ValidationSettings From(int woNoLength, int serialNoMinLength,
        int serialNoMaxLength, int productionDateCodeMinLength, int productionDateCodeMaxLength)
    {
        return new ValidationSettings(woNoLength, serialNoMinLength, serialNoMaxLength, productionDateCodeMinLength,
            productionDateCodeMaxLength);
    }
}