using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.IOption
{
    public interface IOptionRepository
    {
        Task<IEnumerable<Option>> GetAllOptionAsync();
        Task<Option?> GetOptionByIdAsync(Guid id);
        Task AddOptionAsync(Option option);
        Task<bool> UpdateOptionAsync(Option option);
        Task<bool> DeleteOptionAsync(Guid id);
    }
}
