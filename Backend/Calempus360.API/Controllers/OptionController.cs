using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Option;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/options")]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        #region POST
        [HttpPost]
        public async Task<IActionResult> AddOption([FromBody] OptionRequestDto optionRequest,Guid academicYear)
        {
            var option = await _optionService.AddOptionAsync(new Core.Models.Option
                (
                    name: optionRequest.Name,
                    code: optionRequest.Code,
                    description: optionRequest.Description
                ), academicYear);

            return Ok(option.MapToDto());
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GettAllOption()
        {
            var options = await _optionService.GetAllOptionAsync();
            return Ok(options.Select(option => option.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOptionById(Guid id)
        {
            var option = await _optionService.GetOptionByIdAsync(id);
            return Ok(option.MapToDto());
        }

        #endregion

        #region PUT

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOption(OptionRequestDto optionRequest, Guid id, Guid academicYear)
        {
            var option = await _optionService.UpdateOptionAsync(new Core.Models.Option
                (
                    id: id,
                    name: optionRequest.Name,
                    code: optionRequest.Code,
                    description: optionRequest.Description
                ),academicYear);
            return Ok(option.MapToDto());
        }

        #endregion

        #region DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOption(Guid id)
        {
            var response = await _optionService.DeleteOptionAsync(id);
            return Ok(new {message = $"Option with id : {id} deleted" });
        } 
        #endregion
    }
}
