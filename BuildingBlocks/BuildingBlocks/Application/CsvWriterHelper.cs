using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BuildingBlocks.Application;

public static class CsvWriterHelper
{
    public static byte[] WriteToCsv<T>(List<T> records, List<string> includedProperties)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
        var csv = new CsvWriter(writer, config);

        var classMap = new DefaultClassMap<T>();
        foreach (var propertyName in includedProperties)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo != null)
            {
                classMap.Map(typeof(T), propertyInfo);
            }
        }
        
        csv.Context.RegisterClassMap(classMap);

        csv.WriteRecords(records);
        writer.Flush();
        return memoryStream.ToArray();
    }
}