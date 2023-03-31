using crudone.Data;
using crudone.Dto;
using crudone.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace crudone.Controllers
{
    [Route("/api/ApiController")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetDetails()
        {
            return Ok(DataStore.userList);
        }
        [HttpGet("id:int", Name = "GetData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<UserDto>> GetUsers(int id)
        {
            var DataVal = DataStore.userList.FirstOrDefault(u => u.Id == id);
            if (id == 0)
            {
                return BadRequest();
            }
            if (DataVal == null)
            {
                return NotFound();
            }


            return Ok(DataVal);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<UserDto> CreateDto([FromBody] UserDto userDto)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            if (DataStore.userList.FirstOrDefault(u => u.Name.ToLower() == userDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError(" ", "User Already Exist!");
                return BadRequest(ModelState);
            }
            if (userDto == null)
            {
                return BadRequest(userDto);
            }
            if (userDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            userDto.Id = DataStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            DataStore.userList.Add(userDto);
            return CreatedAtRoute("GetData", new { id = userDto.Id }, userDto);
        }
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var User = DataStore.userList.FirstOrDefault(u => u.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            DataStore.userList.Remove(User);
            return NoContent();
        }
        [HttpPut("id:int",Name ="UpdateData")]
        public IActionResult updateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null || id!=userDto.Id)
            {
                return BadRequest();
                

            }
            var user = DataStore.userList.FirstOrDefault(u => u.Id == id);
            user.Name = userDto.Name;
            return NoContent();


        }

    }
}
