using System.Globalization;
using Backend.Model;
using LINTest.Models;

namespace LINTest.Handlers;

public class CSVHandler
{
    public static CSVModel ReadCSV(string filePath)
    {
        var record = new CSVModel();
        var keyActions = GetKeyActions();

        try
        {
            using var reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine()?.Split(';') ?? Array.Empty<string>();

                if (values.Length < 6) continue;

                var key = values[4].Trim();
                var value = values[5].Trim();
                var stepType = values[2].Trim();
                var stepNo = values[3].Trim();
                var timestamp = values[0].Trim();

                ProcessKey(record, keyActions, key, value, timestamp);

                if (stepType == "ERROR")
                {
                    AddError(record, stepNo, ParseDateTime(timestamp));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while reading CSV file: {e.Message}");
            throw;
        }

        return record;
    }

    private static void ProcessKey(CSVModel record, Dictionary<string, Action<CSVModel, string>> keyActions, string key,
        string value, string timestamp)
    {
        if (key == "LINTest has finished. EndOfTest")
        {
            value = timestamp;
        }
        if (keyActions.TryGetValue(key, out var action))
        {
            action(record, value);
        }
    }

    private static void AddError(CSVModel record, string stepNo, DateTime dateTime)
    {
        var error = new TestErrorModel
        {
            WorkOrderNumber = Int32.TryParse(record.WorkOrderNumber, out var woNumber) ? woNumber : 0,
            SerialNumber = Int32.TryParse(record.SerialNumber, out var serialNumber) ? serialNumber : 0,
            Tester = record.Tester,
            Bay = record.Bay,
            ErrorCode = Int32.TryParse(stepNo, out var errorCode) ? errorCode : 0,
            ErrorMessage = stepNo,
            TimeOccured = dateTime
        };
        record.TestErrors.Add(error);
    }

    private static Dictionary<string, Action<CSVModel, string>> GetKeyActions()
    {
        return new Dictionary<string, Action<CSVModel, string>>
        {
            { "Communication Protocol", (model, value) => model.CommunicationProtocol = value },
            { "WO Number", (model, value) => model.WorkOrderNumber = value },
            { "Serial Number", (model, value) => model.SerialNumber = value },
            { "Serial from PLC", (model, value) => model.SerialNumber = value },
            { "Actuator ID", (model, value) => model.ActuatorId = value },
            { "UniqueID from Actuator", (model, value) => model.PCBAUid = value },
            { "From AxArtNo", (model, value) => model.ArticleNumber = value },
            { "From AxArtName", (model, value) => model.ArticleName = value },
            {
                "LINTest has finished. EndOfTest", (model, value) =>
                {
                    model.LINTestPassed = true;
                    model.CreatedTime = ParseDateTime(value);
                }
            },
            { "Tester", (model, value) => model.Tester = value },
            { "Bay", (model, value) => model.Bay = Int32.Parse(value) },
            { "Min Servo Position", (model, value) => model.MinServoPosition = value },
            { "Min.Servo.Position", (model, value) => model.MinServoPosition = value },
            { "Max Servo Position", (model, value) => model.MaxServoPosition = value },
            { "Max.Servo.Position", (model, value) => model.MaxServoPosition = value },
            { "Min Buslink Position", (model, value) => model.MinBuslinkPosition = value },
            { "Min. BusLink Position", (model, value) => model.MinBuslinkPosition = value },
            { "Max Buslink Position", (model, value) => model.MaxBuslinkPosition = value },
            { "Max. BusLink Position", (model, value) => model.MaxBuslinkPosition = value },
            { "Servo Stroke", (model, value) => model.ServoStroke = value },
        };
    }

    private static DateTime ParseDateTime(string dateTimeString)
    {
        try
        {
            return DateTime.ParseExact(dateTimeString, "dd.MM.y / HH:mm:ss", null, DateTimeStyles.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException($"Could not parse datetime trying to read CSV file with date: {dateTimeString}");
        }
    }
}