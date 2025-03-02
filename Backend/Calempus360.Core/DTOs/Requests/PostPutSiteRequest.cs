using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Requests;

public class PostPutSiteRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Phone { get; set; }
}