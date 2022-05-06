using CardManagementAPI.Abstractions;
using CardManagementAPI.Implementations;
using CardManagementAPI.Models.EntityDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IServices _service;

        public CardController(IServices service)
        {
            _service = service;
        }
        //[HttpGet("generatepin")]
        public async Task<IActionResult> GeneratePin(string CustomerId)
        {
            if (string.IsNullOrEmpty(CustomerId))
                return BadRequest("Customer ID cannot be null");
            int? result = await _service.GeneratePinAsync(CustomerId);
            if (result == null)
                return NotFound("Customer not found!");
            return Ok(result);
        }
        //[HttpPost("activate")]
        public async Task<IActionResult> ActivateCard([FromBody]CardDTO cardDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please fill the required fields");
            string result = await _service.ActivateCardAsync(cardDTO);
            if (result == null)
                return NotFound("Customer not found!");
            return Ok(result);
        }
        //[HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateCard([FromBody] CardDTO cardDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please fill the required fields");
            string result = await _service.DeactivateCardAsync(cardDTO);
            if (result == null)
                return NotFound("Customer not found!");
            return Ok(result);
        }
        [HttpPost("add")]
        public IActionResult AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please fill the required fields");
            _service.AddCustomer(customerDTO);
            return Ok("Successfully Added");
        }

    }
}
