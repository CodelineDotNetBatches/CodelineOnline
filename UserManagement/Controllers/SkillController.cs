using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;
using UserManagement.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsSkillController : ControllerBase
    {
        private readonly IInstructorSkillService _instructorSkillService;

        // Constructor Injection
        public InsSkillController(IInstructorSkillService skillService)
        {
            _instructorSkillService = skillService ?? throw new ArgumentNullException(nameof(skillService));
        }

        // ============================================================
        // BASIC CRUD
        // ============================================================

        [HttpGet("GetAllSkills")]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("GetSkillByID")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _instructorSkillService.GetSkillByIdAsync(id);
            return skill == null ? NotFound($"Skill with ID {id} not found.") : Ok(skill);
        }

        [HttpPost("CreateNewSkill")]
        public async Task<IActionResult> Add([FromBody] InsSkillDto dto)
        {
            await _instructorSkillService.AddSkillAsync(dto);
            return Ok("Skill added successfully.");
        }

        [HttpPut("UpdateSkill")]
        public async Task<IActionResult> Update([FromBody] InsSkillDto dto)
        {
            await _instructorSkillService.UpdateSkillAsync(dto);
            return Ok("Skill updated successfully.");
        }

        [HttpDelete("DeleteSkillById")]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorSkillService.DeleteSkillAsync(id);
            return Ok("Skill deleted successfully.");
        }

        // ============================================================
        // FILTERING ENDPOINTS
        // ============================================================

        /// <summary>
        /// Filter skills by name (case-insensitive contains).
        /// Example: /InsSkill/FilterBySkillName?name=python
        /// </summary>
        [HttpGet("FilterBySkillName")]
        public async Task<IActionResult> FilterBySkillName([FromQuery] string name)
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();
            var filtered = skills
                .Where(s => s.SkillName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter skills by level (e.g. Beginner, Intermediate, Advanced).
        /// Example: /InsSkill/FilterBySkillLevel?level=Advanced
        /// </summary>
        [HttpGet("FilterBySkillLevel")]
        public async Task<IActionResult> FilterBySkillLevel([FromQuery] string level)
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();
            var filtered = skills
                .Where(s => s.SkillLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter skills by months of experience range.
        /// Example: /InsSkill/FilterByMonthsOfExperience?min=6&max=24
        /// </summary>
        [HttpGet("FilterByMonthsOfExperience")]
        public async Task<IActionResult> FilterByMonthsOfExperience([FromQuery] int? min, [FromQuery] int? max)
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();
            var filtered = skills
                .Where(s =>
                    (!min.HasValue || s.MonthsOfExperience >= min) &&
                    (!max.HasValue || s.MonthsOfExperience <= max))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Combined filter by name, level, and experience range.
        /// Example: /InsSkill/FilterCombined?name=python&level=Advanced&min=12
        /// </summary>
        [HttpGet("FilterCombined")]
        public async Task<IActionResult> FilterCombined(
            [FromQuery] string? name,
            [FromQuery] string? level,
            [FromQuery] int? min,
            [FromQuery] int? max)
        {
            var skills = await _instructorSkillService.GetAllSkillsAsync();

            var filtered = skills.Where(s =>
                (string.IsNullOrEmpty(name) || s.SkillName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(level) || s.SkillLevel.Equals(level, StringComparison.OrdinalIgnoreCase)) &&
                (!min.HasValue || s.MonthsOfExperience >= min) &&
                (!max.HasValue || s.MonthsOfExperience <= max))
                .ToList();

            return Ok(filtered);
        }
    }
}
