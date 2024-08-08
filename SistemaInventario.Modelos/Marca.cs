using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(60, ErrorMessage = "Nombre menos de 60")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion requerido")]
        [MaxLength(60, ErrorMessage = "Descripcion menos de 60")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Estado requerido")]
        public bool Estado { get; set; }
    }
}
