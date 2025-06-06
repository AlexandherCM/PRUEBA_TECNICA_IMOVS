using System.ComponentModel.DataAnnotations;

namespace PRUEBA_TECNICA_IMOVS.DTOs 
{
    public class EntidadEjemDTO
    {
        
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(250, ErrorMessage = "El nombre no puede exceder los 250 caracteres.")]
        public string Nombre { get; set; }

        [Range(0, 150, ErrorMessage = "Los años deben estar entre 0 y 150.")]
        public int Años { get; set; }
    }
}