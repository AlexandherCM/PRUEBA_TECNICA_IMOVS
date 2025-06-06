using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description; // Para ResponseType
using PRUEBA_TECNICA_IMOVS.DTOs;
using PRUEBA_TECNICA_IMOVS.Interfaces.Services; // Para tu interfaz de servicio


namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class EntidadesEjemploController : ApiController
    {
        private readonly IServicesEntidadEjem _entidadService; // Inyección del servicio

        public EntidadesEjemploController(IServicesEntidadEjem entidadService)
        {
            _entidadService = entidadService;
        }

        // GET: api/EntidadesEjemplo
        public async Task<IHttpActionResult> GetEntidadesEjemplo()
        {
            var entidades = await _entidadService.GetAllEntidadesAsync();
            return Ok(entidades);
        }

        // GET: api/EntidadesEjemplo/5
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

        // PUT: api/EntidadesEjemplo/5
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
                return NotFound(); // O un HttpStatusCode.Conflict si es por concurrencia
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EntidadesEjemplo
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
                return InternalServerError(); // El servicio no pudo crear/guardar
            }

            return CreatedAtRoute("DefaultApi", new { id = createdEntidadDto.ID }, createdEntidadDto);
        }

        // DELETE: api/EntidadesEjemplo/5
        [ResponseType(typeof(void))] // La respuesta es un 204 No Content si es exitoso
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