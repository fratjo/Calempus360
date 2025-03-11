using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/university/{universityId:guid}/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #region POST
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseRequestDto courseRequest, Guid academicYear, Guid universityId)
        {
            var course = await _courseService.AddCourseAsync(new Core.Models.Course
                (
                    name: courseRequest.Name,
                    code: courseRequest.Code,
                    description: courseRequest.Description,
                    totalHours: courseRequest.TotalHours,
                    weeklyHours: courseRequest.WeeklyHour,
                    semester: courseRequest.Semester,
                    credits: courseRequest.Credits
                ), academicYear, universityId, courseRequest.EquipmentType);

                return Ok(course.MapToDto());
        }
        #endregion

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses.Select(c => c.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            return Ok(course.MapToDto());
        }

        #endregion

        #region PUT
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCourse(CourseRequestDto courseRequest,Guid academicYear, Guid universityId)
        {
            var course = await _courseService.UpdateCourseAsync(new Core.Models.Course
                (
                    name: courseRequest.Name,
                    code: courseRequest.Code,
                    description: courseRequest.Description,
                    totalHours: courseRequest.TotalHours,
                    weeklyHours: courseRequest.WeeklyHour,
                    semester: courseRequest.Semester,
                    credits: courseRequest.Credits
                ),academicYear,courseRequest.EquipmentType,universityId);
            return Ok(course.MapToDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var response = await _courseService.DeleteCourseAsync(id);
            if(response) return Ok(new { message = $"Course with id : {id} deleted" });
            return NotFound(new { message = $"Course with id: {id} not found or could not be deleted" });
        }
        #endregion
    }
}
