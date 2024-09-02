using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YT_VillaApi.Data;
using YT_VillaApi.Logging;
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
        private readonly ILogging _logger;

        public VillaApiController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas","");
            return Ok(VillaStore.villalist);
        }
        [HttpGet("{id:int}",Name ="GetVilla")]
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
                _logger.Log("get villa  Error with Id" + id,"error");
                return BadRequest();
            }
            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
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
            if(VillaStore.villalist.FirstOrDefault(u=>u.Name.ToLower()==VillaDTO.Name.ToLower()
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
            VillaDTO.Id = VillaStore.villalist.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villalist.Add(VillaDTO);
            return CreatedAtRoute("GetVilla",new { id = VillaDTO.Id }, VillaDTO);
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
          var villa=VillaStore.villalist.FirstOrDefault(u=>u.Id==id);   
          if(villa==null)
            {
                return NotFound();
            }
          VillaStore.villalist.Remove(villa);
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
            var villa=VillaStore.villalist.FirstOrDefault(u=>u.Id==id);
            villa.Name = villaDTO.Name;
            villa.sqft = villaDTO.sqft;
            villa.Occupancy = villaDTO.Occupancy;
            return NoContent();
        }
    }
    
}
