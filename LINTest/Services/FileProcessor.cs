
using LINTest.Services;

public class FileProcessor
{
    private readonly string _csvFolderPath;

    public FileProcessor(FileProcessorOptions options)
    {
        if (options == null || string.IsNullOrWhiteSpace(options.CsvFolderPath))
            throw new ArgumentNullException(nameof(options.CsvFolderPath));

        _csvFolderPath = options.CsvFolderPath; 
    }

    public string[] GetCsvFiles()
    {
        var files = Directory.GetFiles(_csvFolderPath, "*.csv");
        foreach (var file in files)
        {
            Console.WriteLine($"Found CSV file: {file}");
        }
        return files;
    }


    public DateTime GetFileCreationTime(string filePath)
    {
        return File.GetCreationTime(filePath);
    }
}