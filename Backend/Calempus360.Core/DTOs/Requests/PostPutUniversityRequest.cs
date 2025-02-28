using System.ComponentModel.DataAnnotations;

namespace Calempus360.Core.DTOs.Requests;

public class PostPutUniversityRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Code { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
}