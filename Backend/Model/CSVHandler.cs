using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Backend.Model;

public class CSVHandler
{
    public static CSVModel ReadCSV(string filePath)
    {
        var record = new CSVModel();

        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values.Length < 6) continue;

                var key = values[4].Trim();
                var value = values[5].Trim();
                var LINTestPassed = values[3].Trim();

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
                    case "Product":
                        record.Product = value;
                        break;
                    case "Actuator ID":
                        record.ActuatorID = value;
                        break;
                    case "UniqueID from Actuator":
                        record.UId = value;
                        break;
                    case "From AxArtNo":
                        record.ArticleNumber = value;
                        break;
                    case "From AxArtName":
                        record.ArticleName = value;
                        break;
                    case "From AxConf":
                        record.Configuration = value;
                        break;
                    case "LINTest has finished. EndOfTest":
                        record.LINTestPassed = LINTestPassed ;
                        break;
                   
                }
            }
        }

        return record;
    }
    
    public static void WriteCSV(CSVModel record, string filePath)
    {
        if (!record.LINTestPassed.Contains("360")) return;
        
        using var writer = new StreamWriter(filePath);
        writer.WriteLine("Communication Protocol;" + record.CommunicationProtocol);
        writer.WriteLine("Work Order Number;" + record.WorkOrderNumber);
        writer.WriteLine("Serial Number;" + record.SerialNumber);
        writer.WriteLine("Product;" + record.Product);
        writer.WriteLine("Actuator ID;" + record.ActuatorID);
        writer.WriteLine("UID;" + record.UId);
        writer.WriteLine("Article Number;" + record.ArticleNumber);
        writer.WriteLine("Article Name;" + record.ArticleName);
        writer.WriteLine("Configuration;" + record.Configuration);
        writer.WriteLine("LINTest Passed Successfully;" + record.LINTestPassed);

    }
    

}