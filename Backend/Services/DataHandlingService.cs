﻿using Backend.Model;

namespace Backend.Services;

public class DataHandlingService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DataHandlingService> _logger;

    public DataHandlingService(ApplicationDbContext context, ILogger<DataHandlingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void SaveData(CSVModel csvModel)
    {
        try
        {
            if (!csvModel.LINTestPassed.Contains("360"))
            {
                _logger.LogWarning(
                    "LINTest has not passed since it does not contain field with value 360. Data will not be saved.");
                return;
            }

            if (!int.TryParse(csvModel.WorkOrderNumber, out var workOrderNumber) ||
                !int.TryParse(csvModel.SerialNumber, out var serialNumber) ||
                !int.TryParse(csvModel.PCBAUid, out var pcbaUid))
            {
                _logger.LogWarning("Failed to parse data from CSVModel. Data will not be saved.");
                return;
            }

            var actuator = new ActuatorModel
            {
                WorkOrderNumber = workOrderNumber,
                SerialNumber = serialNumber,
                PCBAUid = pcbaUid
            };

            _context.ActuatorModel.Add(actuator);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while saving data to the database: {e.Message}");

            _logger.LogError(e, "Error occurred while saving data to the database.");
        }
    }
    
}