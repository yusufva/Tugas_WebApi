﻿using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        //Logic untuk Get Room
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll(); //mengambil semua data Room
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get Room/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid); //mengambil data Room By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post Room/
        [HttpPost]
        public IActionResult Insert(Room room)
        {
            var result = _roomRepository.Create(room); //melakukan Create Room
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT Room
        [HttpPut]
        public IActionResult Update(Room room)
        {
            var result = _roomRepository.Update(room); //melakukan update Room
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid); //mengambil room by GUID
            var result = _roomRepository.Delete(room); //melakukan Delete Room
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Room has been deleted");
        }
    }
}