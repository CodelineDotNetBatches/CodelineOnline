using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;
using UserManagement.DTOs;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            return skill == null ? NotFound() : Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SkillDto dto)
        {
            await _skillService.AddSkillAsync(dto);
            return Ok("Skill added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(SkillDto dto)
        {
            await _skillService.UpdateSkillAsync(dto);
            return Ok("Skill updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _skillService.DeleteSkillAsync(id);
            return Ok("Skill deleted successfully.");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(int traineeId, int skillId)
        {
            await _skillService.AssignSkillToTraineeAsync(traineeId, skillId);
            return Ok("Skill assigned to trainee.");
        }
    }
}
