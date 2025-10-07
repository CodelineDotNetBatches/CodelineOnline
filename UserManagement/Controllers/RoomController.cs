using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        // ---------------------------------------------------------
        // GET: api/room
        // ---------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms([FromQuery] bool includeBranch = false)
        {
            var rooms = await _service.GetAllAsync(includeBranch);
            return Ok(rooms);
        }

        // ---------------------------------------------------------
        // GET: api/room/{roomNumber}
        // ---------------------------------------------------------
        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<Room>> GetRoom(string roomNumber, [FromQuery] bool includeBranch = false)
        {
            var room = await _service.GetByNumberAsync(roomNumber, includeBranch);
            if (room == null)
                return NotFound(new { message = $"Room '{roomNumber}' not found." });

            return Ok(room);
        }

        // ---------------------------------------------------------
        // GET: api/room/branch/{branchId}
        // ---------------------------------------------------------
        [HttpGet("branch/{branchId:int}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByBranch(int branchId, [FromQuery] bool includeBranch = false)
        {
            var rooms = await _service.GetByBranchAsync(branchId, includeBranch);
            return Ok(rooms);
        }

        // ---------------------------------------------------------
        // POST: api/room
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(room);
            return CreatedAtAction(nameof(GetRoom), new { roomNumber = created.RoomNumber }, created);
        }

        // ---------------------------------------------------------
        // PUT: api/room/{roomNumber}
        // ---------------------------------------------------------
        [HttpPut("{roomNumber}")]
        public async Task<ActionResult> UpdateRoom(string roomNumber, [FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(roomNumber, room);
            if (!updated)
                return NotFound(new { message = $"Room '{roomNumber}' not found." });

            return NoContent();
        }

        // ---------------------------------------------------------
        // DELETE: api/room/{roomNumber}
        // ---------------------------------------------------------
        [HttpDelete("{roomNumber}")]
        public async Task<ActionResult> DeleteRoom(string roomNumber)
        {
            var deleted = await _service.DeleteAsync(roomNumber);
            if (!deleted)
                return NotFound(new { message = $"Room '{roomNumber}' not found." });

            return NoContent();
        }
    }
}
