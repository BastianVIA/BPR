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
        var pcbaUidLenght = configValidationSettings.GetValue<int>("PCBAUidLength");
        var woNoLength = configValidationSettings.GetValue<int>("WorkOrderNumberLength");
        var serialNoMinLength = configValidationSettings.GetValue<int>("SerialNumberMinLength");
        var serialNoMaxLenght = configValidationSettings.GetValue<int>("SerialNumberMaxLength");
        var itemNoLength = configValidationSettings.GetValue<int>("ItemNumberLength");
        var manufacturerNoLength = configValidationSettings.GetValue<int>("ManufacturerNumberLength");
        var productionDateCodeLength = configValidationSettings.GetValue<int>("ProductionDateCodeLength");

        var validationSettings = ValidationSettings.From(pcbaUidLenght, woNoLength, serialNoMinLength,
            serialNoMaxLenght, itemNoLength, manufacturerNoLength, productionDateCodeLength);
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
    public int ItemNumberLength { get; }
    public int ManufacturerNumberLength { get; }
    public int ProductionDateCodeLenght { get; }
    public int PCBAUidLength { get; }
    
    private ValidationSettings()
    {
    }

    private ValidationSettings(int pcbaUidLenght, int woNoLength, int serialNoMinLength,
        int serialNoMaxLength, int itemNoLength, int manufacturerNoLength, int productionDateCodeLenght)
    {
        PCBAUidLength = pcbaUidLenght;
        WorkOrderNumberLength = woNoLength;
        SerialNumberMinLength = serialNoMinLength;
        SerialNumberMaxLength = serialNoMaxLength;
        ItemNumberLength = itemNoLength;
        ManufacturerNumberLength = manufacturerNoLength;
        ProductionDateCodeLenght = productionDateCodeLenght;
    }

    internal static ValidationSettings From(int pcbaUidLenght, int woNoLength, int serialNoMinLength,
        int serialNoMaxLength, int itemNoLength, int manufacturerNoLength, int productionDateCodeLenght)
    {
        return new ValidationSettings(pcbaUidLenght, woNoLength, serialNoMinLength, serialNoMaxLength, itemNoLength,
            manufacturerNoLength, productionDateCodeLenght);
    }
}