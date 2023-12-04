using System.Globalization;
using Backend.Model;

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