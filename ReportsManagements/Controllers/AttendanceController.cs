using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Services;

namespace ReportsManagements.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRecordService _service;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRecordService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // POST: api/v1/attendance
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceRecordCreateDto dto)
        {
            var record = _mapper.Map<AttendanceRecord>(dto);
            var created = await _service.CreateAsync(record);
            var result = _mapper.Map<AttendanceRecordDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = result.AttId }, result);
        }

        // GET: api/v1/attendance/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _service.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(_mapper.Map<AttendanceRecordDto>(record));
        }

        // PUT: api/v1/attendance/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttendanceRecordUpdateDto dto)
        {
            var record = await _service.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            _mapper.Map(dto, record);
            var updated = await _service.UpdateAsync(id, record);

            return Ok(_mapper.Map<AttendanceRecordDto>(updated));
        }

        // DELETE: api/v1/attendance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
