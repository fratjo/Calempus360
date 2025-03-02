using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Calempus360.Core.DTOs.Requests;

public class PostPutAcademicYearRequest
{
    [Required]
    public string   Id        { get; set; }
    [Required]
    public DateTime DateStart { get; set; }
    [Required]
    public DateTime DateEnd   { get; set; }
}