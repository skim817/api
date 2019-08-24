using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using placeAPI.Model;

namespace placeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceConvertersController : ControllerBase
    {
        private readonly placeConverterContext _context;

        public PlaceConvertersController(placeConverterContext context)
        {
            _context = context;
        }

        // GET: api/PlaceConverters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceConverter>>> GetPlaceConverter()
        {
            return await _context.PlaceConverter.ToListAsync();
        }

        // GET: api/PlaceConverters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceConverter>> GetPlaceConverter(int id)
        {
            var placeConverter = await _context.PlaceConverter.FindAsync(id);

            if (placeConverter == null)
            {
                return NotFound();
            }

            return placeConverter;
        }

        // PUT: api/PlaceConverters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceConverter(int id, PlaceConverter placeConverter)
        {
            if (id != placeConverter.PlaceConverterId)
            {
                return BadRequest();
            }

            _context.Entry(placeConverter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceConverterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlaceConverters
        [HttpPost]
        public async Task<ActionResult<PlaceConverter>> PostPlaceConverter(PlaceConverter placeConverter)
        {
            _context.PlaceConverter.Add(placeConverter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceConverter", new { id = placeConverter.PlaceConverterId }, placeConverter);
        }

        // DELETE: api/PlaceConverters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceConverter>> DeletePlaceConverter(int id)
        {
            var placeConverter = await _context.PlaceConverter.FindAsync(id);
            if (placeConverter == null)
            {
                return NotFound();
            }

            _context.PlaceConverter.Remove(placeConverter);
            await _context.SaveChangesAsync();

            return placeConverter;
        }

        private bool PlaceConverterExists(int id)
        {
            return _context.PlaceConverter.Any(e => e.PlaceConverterId == id);
        }
    }
}
