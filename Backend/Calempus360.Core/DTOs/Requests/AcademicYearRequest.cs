using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Calempus360.Core.DTOs.Requests;

public class AcademicYearRequest
{
    [Required]
    public string   Id        { get; set; } = string.Empty;
    [Required]
    public DateTime DateStart { get; set; }
    [Required]
    public DateTime DateEnd   { get; set; }
}