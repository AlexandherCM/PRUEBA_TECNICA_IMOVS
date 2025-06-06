using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description; 
using PRUEBA_TECNICA_IMOVS.DTOs;
using PRUEBA_TECNICA_IMOVS.Interfaces.Services; 


namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class EntidadesEjemploController : ApiController
    {
        private readonly IServicesEntidadEjem _entidadService; 

        public EntidadesEjemploController(IServicesEntidadEjem entidadService)
        {
            _entidadService = entidadService;
        }


        public async Task<IHttpActionResult> GetEntidadesEjemplo()
        {
            var entidades = await _entidadService.GetAllEntidadesAsync();
            return Ok(entidades);
        }

        [ResponseType(typeof(EntidadEjemploReadDto))]
        public async Task<IHttpActionResult> GetEntidadEjemplo(int id)
        {
            var entidadDto = await _entidadService.GetEntidadByIdAsync(id);
            if (entidadDto == null)
            {
                return NotFound();
            }
            return Ok(entidadDto);
        }


        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntidadEjemplo(int id, EntidadEjemDTO entidadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _entidadService.UpdateEntidadAsync(id, entidadDto);
            if (!updated)
            {
                return NotFound(); 
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

   
        [ResponseType(typeof(EntidadEjemploReadDto))]
        public async Task<IHttpActionResult> PostEntidadEjemplo(EntidadEjemDTO entidadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntidadDto = await _entidadService.CreateEntidadAsync(entidadDto);

            if (createdEntidadDto == null)
            {
                return InternalServerError(); 
            }

            return CreatedAtRoute("DefaultApi", new { id = createdEntidadDto.ID }, createdEntidadDto);
        }

 
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteEntidadEjemplo(int id)
        {
            var deleted = await _entidadService.DeleteEntidadAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}