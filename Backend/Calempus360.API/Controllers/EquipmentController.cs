using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/equipments")]
    [ApiController]
    public class EquipmentController(IEquipmentService equipmentService) : ControllerBase
    {
        #region EquipmentType

        #region Get

        [HttpGet("equipment-types")]
        public async Task<IActionResult> GetEquipmentTypes()
        {
            var equipmentTypes = await equipmentService.GetEquipmentTypesAsync();
            return Ok(equipmentTypes.Select(et => et.MapToDto()));
        }
        
        [HttpGet("equipment-types/{id:guid}")]
        public async Task<IActionResult> GetEquipmentTypeById(Guid id)
        {
            var equipmentType = await equipmentService.GetEquipmentTypeByIdAsync(id);
            return Ok(equipmentType.MapToDto());
        }
        
        #endregion
        
        #region Post

        [HttpPost("equipment-types")]
        public async Task<IActionResult> CreateEquipmentType([FromBody] EquipmentTypeRequestDto equipmentTypeRequestDto)
        {
            var equipmentType = new EquipmentType(
                name : equipmentTypeRequestDto.Name,
                code : equipmentTypeRequestDto.Code,
                description : equipmentTypeRequestDto.Description
            );
            var createdEquipmentType = await equipmentService.CreateEquipmentTypeAsync(equipmentType);
            return CreatedAtAction(nameof(GetEquipmentTypeById), new { id = createdEquipmentType.Id }, createdEquipmentType.MapToDto());
        }
        
        #endregion
        
        #region Put

        [HttpPut("equipment-types/{id:guid}")]
        public async Task<IActionResult> UpdateEquipmentType(Guid id, [FromBody] EquipmentTypeRequestDto equipmentTypeRequestDto)
        {
            var equipmentType = new EquipmentType(
                id : id,
                name : equipmentTypeRequestDto.Name,
                code : equipmentTypeRequestDto.Code,
                description : equipmentTypeRequestDto.Description
            );
            var updatedEquipmentType = await equipmentService.UpdateEquipmentTypeAsync(equipmentType);
            return Ok(updatedEquipmentType.MapToDto());
        }
        
        #endregion
        
        #region Delete

        [HttpDelete("equipment-types/{id:guid}")]
        public async Task<IActionResult> DeleteEquipmentType(Guid id)
        {
            var isDeleted = await equipmentService.DeleteEquipmentTypeByIdAsync(id);
            return Ok(isDeleted);
        }
        
        #endregion

        #endregion
        
        #region Equipment
        
        #region Get

        [HttpGet("/api/universities/{universityId:guid}/equipments")]
        public async Task<IActionResult> GetEquipmentsByUniversity(Guid universityId)
        {
            var equipments = await equipmentService.GetEquipmentsByUniversityAsync(universityId);
            return Ok(equipments.Select(e => e.MapToDto()));
        }
        
        [HttpGet("/api/universities/{universityId:guid}/sites/{siteId:guid}/equipments")]
        public async Task<IActionResult> GetEquipmentsBySite(Guid siteId)
        {
            var equipments = await equipmentService.GetEquipmentsBySiteAsync(siteId);
            return Ok(equipments.Select(e => e.MapToDto()));
        }
        
        [HttpGet("/api/universities/{universityId:guid}/sites/{siteId:guid}/classrooms/{classroomId:guid}/equipments")]
        public async Task<IActionResult> GetEquipmentsByClassroomId(Guid classroomId)
        {
            var equipments = await equipmentService.GetEquipmentsByClassroomIdAsync(classroomId);
            return Ok(equipments.Select(e => e.MapToDto()));
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEquipmentById(Guid id)
        {
            var equipment = await equipmentService.GetEquipmentByIdAsync(id);
            return Ok(equipment.MapToDto());
        }
        
        #endregion
        
        #region Post

        [HttpPost("/api/universities/{universityId:guid}/sites/{siteId:guid}/equipments")]
        public async Task<IActionResult> CreateEquipment(Guid universityId, Guid siteId, [FromBody] EquipmentRequestDto equipmentRequestDto)
        {
            var equipment = new Equipment(
                name : equipmentRequestDto.Name,
                code : equipmentRequestDto.Code,
                brand : equipmentRequestDto.Brand,
                model : equipmentRequestDto.Model,
                description : equipmentRequestDto.Description
            );
            var createdEquipment = 
                await equipmentService.CreateEquipmentAsync(equipment, equipmentRequestDto.EquipmentTypeId, siteId, universityId);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment.MapToDto());
        }
        
        [HttpPost("/api/universities/{universityId:guid}/equipments")]
        public async Task<IActionResult> CreateEquipment(Guid universityId, [FromBody] EquipmentRequestDto equipmentRequestDto)
        {
            var equipment = new Equipment(
                name : equipmentRequestDto.Name,
                code : equipmentRequestDto.Code,
                brand : equipmentRequestDto.Brand,
                model : equipmentRequestDto.Model,
                description : equipmentRequestDto.Description
            );
            
            var createdEquipment = 
                await equipmentService.CreateEquipmentAsync(equipment, equipmentRequestDto.EquipmentTypeId, universityId);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment.MapToDto());
        }
        
        #endregion
        
        #region Put

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] EquipmentRequestDto equipmentRequestDto)
        {
            var equipment = new Equipment(
                id : id,
                name : equipmentRequestDto.Name,
                code : equipmentRequestDto.Code,
                brand : equipmentRequestDto.Brand,
                model : equipmentRequestDto.Model,
                description : equipmentRequestDto.Description
            );
            var updatedEquipment = await equipmentService.UpdateEquipmentAsync(equipment, equipmentRequestDto.EquipmentTypeId);
            return Ok(updatedEquipment.MapToDto());
        }
        
        [HttpPut("{equipmentId:guid}/classrooms/change/{classroomId:guid}")]
        public async Task<IActionResult> ChangeEquipmentClassroom(Guid equipmentId, Guid classroomId, [FromQuery] Guid academicYearId)
        {
            var result = await equipmentService.ChangeEquipmentClassroomAsync(equipmentId, classroomId, academicYearId);
            return Ok(result);
        }
        
        #endregion
        
        #region Delete
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEquipment(Guid id)
        {
            var isDeleted = await equipmentService.DeleteEquipmentAsync(id);
            return Ok(isDeleted);
        }
        
        [HttpDelete("/api/universities/{universityId:guid}/equipments")]
        public async Task<IActionResult> DeleteEquipmentsByUniversity(Guid universityId)
        {
            var isDeleted = await equipmentService.DeleteEquipmentsByUniversityAsync(universityId);
            return Ok(isDeleted);
        }
        
        [HttpDelete("/api/universities/{universityId:guid}/sites/{siteId:guid}/equipments")]
        public async Task<IActionResult> DeleteEquipmentsBySite(Guid siteId)
        {
            var isDeleted = await equipmentService.DeleteEquipmentsBySiteAsync(siteId);
            return Ok(isDeleted);
        }
        
        #endregion
        
        #endregion
    }
}
