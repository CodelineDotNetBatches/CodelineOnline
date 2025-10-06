using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;
using UserManagement.DTOs;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsSkillController : ControllerBase
    {
        private readonly IInstructorSkillService _instructorSkillService;
        // Mapping injection of the service layer

        public InsSkillController(IInstructorSkillService skillService)
        {
            _instructorSkillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _instructorSkillService.GetSkillByIdAsync(id);
            return skill == null ? NotFound() : Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsSkillDto dto)
        {
            await _instructorSkillService.AddSkillAsync(dto);
            return Ok("Skill added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(InsSkillDto dto)
        {
            await _instructorSkillService.UpdateSkillAsync(dto);
            return Ok("Skill updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorSkillService.DeleteSkillAsync(id);
            return Ok("Skill deleted successfully.");
        }

        //[HttpPost("assign")]
        //public async Task<IActionResult> Assign(Guid InsID, int skillId)
        //{
        //    await _instructorSkillService.AddSkillAsync(Ma)
        //        AssignSkillToTraineeAsync(traineeId, skillId);
        //    return Ok("Skill assigned to trainee.");
        //}
    }
}
