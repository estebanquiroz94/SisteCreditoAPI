using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Domain.Models;
using System.Collections.Generic;

namespace SisteCredito.ManagementAPI.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            var areas = await _areaService.GetAll();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            var area = await _areaService.GetById(id);

            return Ok(area);

        }
        [HttpPost]
        public async Task<ActionResult> PostArea(Area area)
        {
            await _areaService.Add(area);
            return CreatedAtAction(nameof(GetArea), new { id = area.Id }, area);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutArea(int id, Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            await _areaService.Update(area);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArea(int id)
        {
            var area = await _areaService.GetById(id);
            if (area == null)
            {
                return NotFound();
            }

            await _areaService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route(nameof(AreaController.Homologate))]
        public async Task<ActionResult> Homologate(IFormFile file)
        {
            await _areaService.Homologate(file);
            return Ok();
        }
    }
}
