using Calempus360.Core.DTOs.Requests.Option;
using Calempus360.Core.DTOs.Responses.Option;
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
        Task<IEnumerable<GetOptionResponse>> GetAllOptionAsync();
        Task<GetOptionResponse?> GetOptionByIdAsync(Guid id);
        Task AddOptionAsync(AddOptionRequest option);
        Task<bool> UpdateOptionAsync(UpdateOptionRequest option);
        Task<bool> DeleteOptionAsync(Guid id);
    }
}
