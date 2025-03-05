using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ClassroomResponseDto
{
    
}

public static partial class DtoMapper
{
    public static ClassroomResponseDto MapToDTO(this Classroom classroom)
    {
        return new ClassroomResponseDto
        {
            
        };
    }
}