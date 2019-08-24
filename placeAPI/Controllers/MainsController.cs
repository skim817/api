using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using placeAPI.Model;
using placeAPI.Transfer;

namespace placeAPI.Controllers
{
    public class INPUT
    {
        public string GeoCode { get; set; }
        public string TypeOfPlace { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class MainsController : ControllerBase
    {
        private readonly placeConverterContext _context;

        public MainsController(placeConverterContext context)
        {
            _context = context;
        }

        // GET: api/Mains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Main>>> GetMain()
        {
            return await _context.Main.ToListAsync();
        }

        // GET: api/Mains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Main>> GetMain(int id)
        {
            var main = await _context.Main.FindAsync(id);

            if (main == null)
            {
                return NotFound();
            }

            return main;
        }

        // PUT: api/Mains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMain(int id, Main main)
        {
            if (id != main.MainId)
            {
                return BadRequest();
            }

            _context.Entry(main).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainExists(id))
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

        // POST: api/Mains
        [HttpPost]
        public async Task<ActionResult<Main>> PostMain([FromBody]INPUT data)
        {
            Main main;
            String geolcation;
            String type;
            try
            {
                // Constructing the video object from our helper function
                geolcation = data.GeoCode;
                type = data.TypeOfPlace;
                main = Map.GetPlaces(geolcation, type);
            }
            catch
            {
                return BadRequest("Invalid");
            }

            _context.Main.Add(main);
            await _context.SaveChangesAsync();

            int ide = main.MainId;

            placeConverterContext a = new placeConverterContext();
            PlaceConvertersController pcc = new PlaceConvertersController(a);

            Task addplace = Task.Run(async () =>
            {
                List<PlaceConverter> placearray = new List<PlaceConverter>();
                placearray = Map.GetPlacesFromGeoCodenType(geolcation, type);

                for (int i = 0; i < placearray.Count; i++)
                {
                    PlaceConverter pl = placearray.ElementAt(i);
                    pl.MainId = ide;

                    await pcc.PostPlaceConverter(pl);
                }
            });





            return CreatedAtAction("GetMain", new { id = main.MainId }, main);
        }

        // DELETE: api/Mains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Main>> DeleteMain(int id)
        {
            var main = await _context.Main.FindAsync(id);
            if (main == null)
            {
                return NotFound();
            }

            _context.Main.Remove(main);
            await _context.SaveChangesAsync();

            return main;
        }

        private bool MainExists(int id)
        {
            return _context.Main.Any(e => e.MainId == id);
        }
    }
}
