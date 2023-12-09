using Microsoft.Extensions.Logging;

namespace LINTest.Services;

public class FileProcessor
{
    private readonly string _csvFolderPath;
    private readonly ILogger<FileProcessor> _logger;

    public FileProcessor(FileProcessorOptions options, ILogger<FileProcessor> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (options == null)
        {
            _logger.LogError("FileProcessorOptions provided is null.");
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.CsvFolderPath))
        {
            _logger.LogError("CSV folder path is null or whitespace.");
            throw new ArgumentException("CSV folder path must not be null or with whitespace.",
                nameof(options.CsvFolderPath));
        }

        if (!Directory.Exists(options.CsvFolderPath))
        {
            _logger.LogError($"The directory '{options.CsvFolderPath}' does not exist.");
            throw new DirectoryNotFoundException($"The directory '{options.CsvFolderPath}' does not exist.");
        }

        _csvFolderPath = options.CsvFolderPath;
        _logger.LogInformation($"CSV folder path is set to {_csvFolderPath}.");
    }

    public string[] GetCsvFileNamesSince(DateTime lastProcessed)
    {
        try
        {
            var files = Directory.GetFiles(_csvFolderPath, "*.csv")
                .Where(file => File.GetLastWriteTime(file) > lastProcessed)
                .ToArray();
            foreach (var file in files)
            {
                _logger.LogInformation($"Found CSV file: {file}");
            }

            return files;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while attempting to get CSV files.");
            throw;
        }
    }

    public DateTime GetFileCreationTime(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found: {filePath}");
                return DateTime.MinValue;
            }

            var lastWriteTime = File.GetLastWriteTime(filePath);
            _logger.LogInformation($"Last Updated time for file '{filePath}' is {lastWriteTime}.");
            return lastWriteTime;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while attempting to get the last write time for file '{filePath}'.");
            throw;
        }
    }
}