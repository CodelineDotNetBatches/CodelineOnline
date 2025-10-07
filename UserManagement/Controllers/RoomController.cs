using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.DTOs;
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

        [HttpGet("All_Rooms")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms([FromQuery] bool includeBranch = false)
        {
            try
            {
                var rooms = await _service.GetAllAsync(includeBranch);
                return Ok(rooms);
            }
            catch (System.Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // ---------------------------------------------------------
        // GET: api/room/{roomNumber}
        // ---------------------------------------------------------
        [HttpGet("GetRoomByRoomNumber")]
        public async Task<ActionResult<Room>> GetRoomByRoomNumber(string roomNumber, [FromQuery] bool includeBranch = false)
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
        [HttpPost("AddRoom")]
        public async Task<ActionResult> AddRoom([FromBody] RoomDTO roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Create the room via service (service now expects RoomDTO)
            var createdRoom = await _service.CreateAsync(roomDto);

            // Return the created room with its route
            return CreatedAtAction(nameof(GetRoomByRoomNumber),
                new { roomNumber = createdRoom.RoomNumber },
                createdRoom);
        }

        // ---------------------------------------------------------
        // PUT: api/room/{roomNumber}
        // ---------------------------------------------------------
        [HttpPut("update code")]
        public async Task<ActionResult> UpdateRoom(string roomNumber, [FromBody] RoomDTO roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(roomNumber, roomDto);
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
