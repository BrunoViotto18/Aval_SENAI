using Controller.Exceptions;
using System.IO;

namespace Controller;


public static class Csv
{
    public static async Task<Dictionary<string, string[]>> ToDataFrameAsync(string csvPath, char charSeparator=';')
    {
        if (!File.Exists(csvPath))
            throw new PathException(csvPath);

        csvPath = csvPath.Replace("/", "\\");

        using var reader = new StreamReader(csvPath);
        var header = await reader.ReadLineAsync();

        if (header is null)
             throw new EmptyFileException(csvPath.Split('\\')[^1]);

        var (dataFrame, indexes) = GetDataFrameSkeleton(header, charSeparator);

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            var items = line!.Split(charSeparator);

            for (int i = 0; i < items.Length; i++)
            {
                var column = indexes[i];
                dataFrame[column].Add(items[i]);
            }
        }

        var normalized = NormalizeDataFrame(dataFrame);

        return normalized;
    }

    private static (Dictionary<string, List<string>> Dataframe, string[] KeyIndexes) GetDataFrameSkeleton(string headerLine, char charSep)
    {
        var headerColumns = headerLine.Split(charSep);
        var dict = new Dictionary<string, List<string>>();

        foreach (var col in headerColumns)
            dict.Add(col, new List<string>());

        return (dict, headerColumns);
    }

    private static Dictionary<string, string[]> NormalizeDataFrame(Dictionary<string, List<string>> dataFrame)
    {
        var normalized = new Dictionary<string, string[]>();

        foreach (var key in dataFrame.Keys)
            normalized.Add(key, dataFrame[key].ToArray());

        return normalized;
    }
}
