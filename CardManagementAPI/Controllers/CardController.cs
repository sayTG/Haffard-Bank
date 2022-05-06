using CardManagementAPI.Abstractions;
using CardManagementAPI.Implementations;
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
        [HttpGet("generatepin")]
        public async Task<IActionResult> GeneratePinAsync(string CustomerId)
        {
            if (string.IsNullOrEmpty(CustomerId))
                return BadRequest("Customer ID cannot be null");
            int? result = await _service.GeneratePinAsync(CustomerId);
            if (result == null)
                return NotFound("Customer not found!");
            return Ok(result);
        }
    }
}
