using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.IOption
{
    public interface IOptionService
    {
        Task<IEnumerable<OptionResponseDto>> GetAllOptionAsync();
        Task<OptionResponseDto?> GetOptionByIdAsync(Guid id);
        Task AddOptionAsync(OptionRequestDto option);
        Task<bool> UpdateOptionAsync(UpdateOptionRequest option);
        Task<bool> DeleteOptionAsync(Guid id);
    }
}
