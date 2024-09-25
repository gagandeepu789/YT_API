using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YT_VillaApi.Data;

using YT_VillaApi.Models;
using YT_VillaApi.Models.Dto;

namespace YT_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    ////////// Get all
    public class VillaApiController : ControllerBase
    {
        //private readonly ILogger<VillaApiController> _logger;
        private readonly ApplicationDbContext _db;
        public VillaApiController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //_logger.Log("Getting all villas","");
            return Ok(_db.Villas.ToList());
        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]


        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                // _logger.Log("get villa  Error with Id" + id,"error");
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        //
        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="VillaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO VillaDTO)
        {
            // if we comment ApiController then we can use this Modelstate where it checks  for condtions if they are valid or not
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == VillaDTO.Name.ToLower()
            ) != null)
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);
            }

            if (VillaDTO == null)
            {
                return BadRequest(VillaDTO);
            }
            if (VillaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // VillaDTO.Id = VillaStore.villalist.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            Villa model = new()
            {
                Id = VillaDTO.Id,
                Name = VillaDTO.Name,
                Details = VillaDTO.Details,
                ImageUrl = VillaDTO.ImageUrl,
                Occupancy = VillaDTO.Occupancy,
                Rate = VillaDTO.Rate,
                sqft = VillaDTO.sqft,
                Amenity = VillaDTO.Amenity
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new {id=VillaDTO.Id},VillaDTO);
        }
    

        /// <summary>
        /// Delete operation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id)
        {
          if(id==0)
            {
                return BadRequest(ModelState);

            }
          var villa=_db.Villas.FirstOrDefault(u=>u.Id==id);   
          if(villa==null)
            {
                return NotFound();
            }
          _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if(villaDTO==null || id!=villaDTO.Id)
            {
                return BadRequest();
            }
            //var villa=VillaStore.villalist.FirstOrDefault(u=>u.Id==id);
            //villa.Name = villaDTO.Name;
            //villa.sqft = villaDTO.sqft;
            //villa.Occupancy = villaDTO.Occupancy;
            Villa model = new()
            {
                Id=villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                sqft = villaDTO.sqft,
                Amenity = villaDTO.Amenity
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
    
}
