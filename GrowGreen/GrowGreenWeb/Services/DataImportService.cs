using GrowGreenWeb.Models;
using Microsoft.VisualBasic.FileIO;

namespace GrowGreenWeb.Services;

public class DataImportService
{
    private readonly GrowGreenContext _context;
    private readonly ILogger<DataImportService> _logger;

    public DataImportService(GrowGreenContext context,
                             ILogger<DataImportService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Import AWS Rekognition labels into the ItemType table
    /// </summary>
    /// <param name="csvPath"></param>
    /// <returns>Whether the file is valid</returns>
    public async Task<int> ImportRekognitionLabels(string csvPath)
    {
        using TextFieldParser parser = new TextFieldParser(csvPath);
        parser.SetDelimiters(new string[] { "," });
        parser.HasFieldsEnclosedInQuotes = true;

        await ClearAllLabels();
        int importedRows = 0;

        string[] firstRow = parser.ReadFields()!;
        if (firstRow[0] != "Label")
        {
            return 0;
        }

        while (!parser.EndOfData)
        {
            try
            {
                string[] values = parser.ReadFields()!;

                ItemType itemType = new ItemType()
                {
                    Name = values[0]
                };

                _context.Add(itemType);
                ++importedRows;
            }
            catch (MalformedLineException ex)
            {
                _logger.LogWarning("Line {LineNo} is invalid, skipping...", 
                                   ex.Message);
            }
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error importing file");
            return 0;
        }
        return importedRows;
    }

    private async Task ClearAllLabels()
    {
        _context.RemoveRange(_context.ItemTypes);
        await _context.SaveChangesAsync();
    }
}