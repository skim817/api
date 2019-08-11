using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourDate.Model;
using PlanYourDate.Transfer;
using PlanYourDate.DAL;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace PlanYourDate.Controllers
{

    public class IDGEN
    {
        public string PlaceID { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]

    public class PlacesController : ControllerBase
    {

        private IPlaceRepository PlaceRepository;
        private readonly IMapper _mapper;


        public PlacesController(PlacesForDateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.PlaceRepository = new PlaceRepository(new PlacesForDateContext());
        }
        private readonly PlacesForDateContext _context;

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Places>>> GetPlaces()
        {
            return await _context.Places.ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Places>> GetPlaces(int id)
        {
            var places = await _context.Places.FindAsync(id);

            if (places == null)
            {
                return NotFound();
            }

            return places;
        }

        // PUT: api/Places/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaces(int id, Places places)
        {
            if (id != places.PlaceId)
            {
                return BadRequest();
            }

            _context.Entry(places).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacesExists(id))
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

        // POST: api/Places
        [HttpPost]
        public async Task<ActionResult<Places>> PostPlaces([FromBody]IDGEN id)
        {
            Places place;
            String PlaceId;
         
            try
            {
                // Constructing the video object from our helper function
                PlaceId = id.PlaceID;
                place = Map.GetPlaceFromId(PlaceId);
            }
            catch
            {
                return BadRequest("Invalid PlaceID");
            }
            _context.Places.Add(place);

            await _context.SaveChangesAsync();

            int ide = place.PlaceId;

            PlacesForDateContext pfc = new PlacesForDateContext();
            ReviewsController ReviewCon = new ReviewsController(pfc);

            Task addReview = Task.Run(async () =>
            {
                List<Reviews> reviewss = new List<Reviews>();
                reviewss = Map.GetReviews(PlaceId);

                for (int i = 0; i < reviewss.Count; i++)
                {
                     Reviews review = reviewss.ElementAt(i);
                     review.PlaceId = ide;

                await ReviewCon.PostReviews(review);
            }
            });

            return CreatedAtAction("GetPlaces", new { id = place.PlaceId }, place);
        }


        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Places>> DeletePlaces(int id)
        {
            var places = await _context.Places.FindAsync(id);
            if (places == null)
            {
                return NotFound();
            }

            _context.Places.Remove(places);
            await _context.SaveChangesAsync();

            return places;
        }

        private bool PlacesExists(int id)
        {
            return _context.Places.Any(e => e.PlaceId == id);
        }

        [HttpGet("SearchInReview/{searchString}")]
        public async Task<ActionResult<IEnumerable<Places>>> Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return BadRequest("Search string cannot be null or empty.");
            }

            var place = await _context.Places.Include(places => places.Reviews).Select(a => new Places
            {
                PlaceId = a.PlaceId,
                PlaceName = a.PlaceName,
                PlaceAddress = a.PlaceAddress,
                PhoneNumber = a.PhoneNumber,
                PhotoRef = a.PhotoRef,
                IsFavourited = a.IsFavourited,
                Reviews = a.Reviews.Where(tran => tran.Comment.Contains(searchString)).ToList()
            }).ToListAsync();

            place.RemoveAll(video => video.Reviews.Count == 0);
            return Ok(place);
        }

        [HttpPatch("update/{id}")]
        public PlaceDTO Patch(int id, [FromBody]JsonPatchDocument<PlaceDTO> PlacePatch)
        {
            
            Places originplace = PlaceRepository.GetPlaceByID(id);
          
            PlaceDTO placeDTO = _mapper.Map<PlaceDTO>(originplace);
           
            PlacePatch.ApplyTo(placeDTO);
          
            _mapper.Map(placeDTO, originplace);
          
            _context.Update(originplace);
            _context.SaveChanges();
            return placeDTO;
        }


    }
}
