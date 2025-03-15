using Calempus360.Core.Interfaces.Option;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Services.Services
{
    public class OptionService : IOptionService
    {

        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository) 
        {
            _optionRepository = optionRepository;
        }
        public async Task<Option> AddOptionAsync(Option option, List<Guid> courses, Guid academicYear)
        {
            return await _optionRepository.AddOptionAsync(option, courses, academicYear);
        }

        public async Task<bool> DeleteOptionAsync(Guid id)
        {
            return await _optionRepository.DeleteOptionAsync(id);
        }

        public async Task<IEnumerable<Option>> GetAllOptionAsync()
        {
            return await _optionRepository.GetAllOptionAsync();
        }

        public async Task<Option?> GetOptionByIdAsync(Guid id)
        {
            return await _optionRepository.GetOptionByIdAsync(id);
        }

        public async Task<Option> UpdateOptionAsync(Option option, List<Guid> courses, Guid academicYear)
        {
            return await _optionRepository.UpdateOptionAsync(option,courses, academicYear);
        }
    }
}
