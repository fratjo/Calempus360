using System.Text.Json.Serialization;

namespace Calempus360.Core.DTOs.Requests;

public class PostPutAcademicYearRequest
{
    public string   Id        { get; set; }
    [JsonPropertyName("dateStart")]
    public DateTime DateStart { get; set; }
    [JsonPropertyName("dateEnd")]
    public DateTime DateEnd   { get; set; }
}