using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Option
{
    public interface IOptionService
    {
        Task<IEnumerable<Models.Option>> GetAllOptionAsync();
        Task<Models.Option?> GetOptionByIdAsync(Guid id);
        Task<Models.Option> AddOptionAsync(Models.Option option, Guid academicYear);
        Task<Models.Option> UpdateOptionAsync(Models.Option option, Guid academicYear);
        Task<bool> DeleteOptionAsync(Guid id);
    }
}
