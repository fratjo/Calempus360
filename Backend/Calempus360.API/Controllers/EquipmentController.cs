using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Calempus360.API.Controllers
{
    [Route("api/equipments")]
    [ApiController]
    public class EquipmentController(IEquipmentService equipmentService) : ControllerBase
    {
        #region EquipmentType

        #region Get

        [HttpGet("types")]
        public async Task<IActionResult> GetEquipmentTypes()
        {
            var equipmentTypes = await equipmentService.GetEquipmentTypesAsync();
            return Ok(equipmentTypes.Select(et => et.MapToDto()));
        }

        [HttpGet("types/{id:guid}")]
        public async Task<IActionResult> GetEquipmentTypeById(Guid id)
        {
            var equipmentType = await equipmentService.GetEquipmentTypeByIdAsync(id);
            return Ok(equipmentType.MapToDto());
        }

        #endregion

        #region Post

        [HttpPost("types")]
        public async Task<IActionResult> CreateEquipmentType([FromBody] EquipmentTypeRequestDto equipmentTypeRequestDto)
        {
            var equipmentType = new EquipmentType(
                name: equipmentTypeRequestDto.Name,
                code: equipmentTypeRequestDto.Code,
                description: equipmentTypeRequestDto.Description
            );
            var createdEquipmentType = await equipmentService.CreateEquipmentTypeAsync(equipmentType);
            return CreatedAtAction(nameof(GetEquipmentTypeById), new { id = createdEquipmentType.Id }, createdEquipmentType.MapToDto());
        }

        #endregion

        #region Put

        [HttpPut("types/{id:guid}")]
        public async Task<IActionResult> UpdateEquipmentType(Guid id, [FromBody] EquipmentTypeRequestDto equipmentTypeRequestDto)
        {
            var equipmentType = new EquipmentType(
                id: id,
                name: equipmentTypeRequestDto.Name,
                code: equipmentTypeRequestDto.Code,
                description: equipmentTypeRequestDto.Description
            );
            var updatedEquipmentType = await equipmentService.UpdateEquipmentTypeAsync(equipmentType);
            return Ok(updatedEquipmentType.MapToDto());
        }

        #endregion

        #region Delete

        [HttpDelete("types/{id:guid}")]
        public async Task<IActionResult> DeleteEquipmentType(Guid id)
        {
            var isDeleted = await equipmentService.DeleteEquipmentTypeByIdAsync(id);
            return Ok(isDeleted);
        }

        #endregion

        #endregion

        #region Equipment

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetEquipments([FromQuery] Guid? universityId, [FromQuery] Guid? academicYearId, [FromQuery] Guid? siteId, [FromQuery] Guid? classroomId, [FromQuery] Guid? equipmentTypeId, [FromQuery] bool flying = false)
        {
            var equipments = await equipmentService.GetEquipmentsAsync(universityId, academicYearId, siteId, classroomId, equipmentTypeId, flying);
            var e = equipments.Select(e => e.MapToDto()).ToList();
            return Ok(e);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEquipmentById(Guid id)
        {
            var equipment = await equipmentService.GetEquipmentByIdAsync(id);
            return Ok(equipment.MapToDto());
        }

        [HttpGet("{id:guid}/site")]
        public async Task<IActionResult> GetEquipmentSite(Guid id)
        {
            var site = await equipmentService.GetEquipmentSiteAsync(id);
            return Ok(site.MapToDto());
        }

        #endregion

        #region Post

        [HttpPost()]
        public async Task<IActionResult> CreateEquipment([FromQuery] Guid universityId, [FromBody] EquipmentRequestDto equipmentRequestDto, [FromQuery] Guid? siteId = null)
        {
            var equipment = new Equipment(
                name: equipmentRequestDto.Name,
                code: equipmentRequestDto.Code,
                brand: equipmentRequestDto.Brand,
                model: equipmentRequestDto.Model,
                description: equipmentRequestDto.Description
            );

            if (siteId.ToString().IsNullOrEmpty()) siteId = null;

            var createdEquipment =
                await equipmentService.CreateEquipmentAsync(equipment, equipmentRequestDto.EquipmentTypeId, siteId, universityId);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment.MapToDto());
        }

        #endregion

        #region Put

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] EquipmentRequestDto equipmentRequestDto, [FromQuery] Guid academicYearId)
        {
            var equipment = new Equipment(
                id: id,
                name: equipmentRequestDto.Name,
                code: equipmentRequestDto.Code,
                brand: equipmentRequestDto.Brand,
                model: equipmentRequestDto.Model,
                description: equipmentRequestDto.Description
            );

            if (equipmentRequestDto.ClassroomId.ToString().IsNullOrEmpty() || equipmentRequestDto.ClassroomId == Guid.Empty) equipmentRequestDto.ClassroomId = null;

            var updatedEquipment = await equipmentService.UpdateEquipmentAsync(equipment, equipmentRequestDto.EquipmentTypeId, equipmentRequestDto.ClassroomId, academicYearId);
            return Ok(updatedEquipment.MapToDto());
        }

        [HttpPut("{equipmentId:guid}/classrooms/{classroomId:guid}")]
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
