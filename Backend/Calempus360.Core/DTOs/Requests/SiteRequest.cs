using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Requests;

public class SiteRequest
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