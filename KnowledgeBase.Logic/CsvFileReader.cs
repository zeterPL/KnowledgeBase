using CsvHelper;
using KnowledgeBase.Logic.Dto.Project;
using System.Globalization;

namespace KnowledgeBase.Logic;

public class CsvFileReader : IFileReader
{
    public IEnumerable<FileProjectDto> ReadProjects(Stream stream)
    {
        var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var projects = csvReader.GetRecords<FileProjectDto>();
        return projects.ToList();
    }
}