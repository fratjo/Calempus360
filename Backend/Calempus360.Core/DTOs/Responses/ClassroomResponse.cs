using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ClassroomResponse
{
    
}

public static partial class DtoMapper
{
    public static ClassroomResponse MapToDTO(this Classroom classroom)
    {
        return new ClassroomResponse
        {
            
        };
    }
}