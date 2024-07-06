using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraHourController : ControllerBase
    {
        private readonly IExtraHourService _extraHourService;

        public ExtraHourController(IExtraHourService extraHourService)
        {
            _extraHourService = extraHourService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraHour>>> GetExtraHours()
        {
            var employees = await _extraHourService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraHour>> GetExtraHour(int id)
        {
            var employee = await _extraHourService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route(nameof(ExtraHourController.Create))]
        public async Task<ActionResult> Create(ExtraHour extraHour)
        {
            await _extraHourService.Add(extraHour);
            return CreatedAtAction(nameof(GetExtraHour), new { id = extraHour.Id }, extraHour);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExtraHour(int id, ExtraHour employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            await _extraHourService.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExtraHour(int id)
        {
            var employee = await _extraHourService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _extraHourService.Delete(id);
            return NoContent();
        }
    }
}
