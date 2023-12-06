using System.Globalization;
using Backend.Model;
using LINTest.Models;

namespace LINTest.Handlers;

public class CSVHandler
{
    public static CSVModel ReadCSV(string filePath)
    {
        var record = new CSVModel();

        try
        {
            using var reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values.Length < 6) continue;

                var key = values[4].Trim();
                var value = values[5].Trim();
                var type = values[2].Trim();
                var stepNo = values[3].Trim();

                switch (key)
                {
                    case "Communication Protocol":
                        record.CommunicationProtocol = value;
                        break;
                    case "WO Number":
                        record.WorkOrderNumber = value;
                        break;
                    case "Serial Number":
                        record.SerialNumber = value;
                        break;
                    case "Serial from PLC":
                        record.SerialNumber = value;
                        break;
                    case "Actuator ID":
                        record.ActuatorId = value;
                        break;
                    case "UniqueID from Actuator":
                        record.PCBAUid = value;
                        break;
                    case "From AxArtNo":
                        record.ArticleNumber = value;
                        break;
                    case "From AxArtName":
                        record.ArticleName = value;
                        break;
                    case "LINTest has finished. EndOfTest":
                        record.LINTestPassed = true;
                        var dateTimeString = values[0].Trim();
                        DateTime dateTime = ParseDateTime(dateTimeString);
                        record.CreatedTime = dateTime;
                        break;
                    case "Tester":
                        record.Tester = value;
                        break;
                    case "Bay":
                        record.Bay = Int32.Parse(value);
                        break;
                    case "Min Servo Position":
                        record.MinServoPosition = value;
                        break;
                    case "Min.Servo.Position":
                        record.MinServoPosition = value;
                        break;
                    case "Max Servo Position":
                        record.MaxServoPosition = value;
                        break;
                    case "Max.Servo.Position":
                        record.MaxServoPosition = value;
                        break;
                    case "Min Buslink Position":
                        record.MinBuslinkPosition = value;
                        break;
                    case "Min. BusLink Position":
                        record.MinBuslinkPosition = value;
                        break;
                    case "Max Buslink Position":
                        record.MaxBuslinkPosition = value;
                        break;
                    case "Max. BusLink Position":
                        record.MaxBuslinkPosition = value;
                        break;
                    case "Servo Stroke":
                        record.ServoStroke = value;
                        break;
                }

                if (type == "ERROR")
                {
                    var dateTimeString = values[0].Trim();
                    DateTime dateTime = ParseDateTime(dateTimeString);
                    var error = new TestErrorModel
                    {
                        WorkOrderNumber = Int32.Parse(record.WorkOrderNumber),
                        SerialNumber = Int32.Parse(record.SerialNumber),
                        Tester = record.Tester,
                        Bay = record.Bay,
                        ErrorCode = Int32.Parse(stepNo),
                        ErrorMessage = key,
                        TimeOccured = dateTime
                    };
                    record.TestErrors.Add(error);
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