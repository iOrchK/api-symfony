//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nomina2018.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Tabulador
    {
        public int idTabulador { get; set; }

        [DisplayName("Empleado")]
        public int idEmpleado { get; set; }

        [DisplayName("Inicio del periodo")]
        [Required(ErrorMessage = "Por favor ingrese la fecha inicial del periodo.")]
        public Nullable<System.DateTime> inicioPeriodo { get; set; }

        [DisplayName("Fin del periodo")]
        [Required(ErrorMessage = "Por favor ingrese la fecha final del periodo.")]
        public Nullable<System.DateTime> finPeriodo { get; set; }

        [DisplayName("Sueldo base")]
        [Required(ErrorMessage = "Por favor ingrese el sueldo base.")]
        [Range(0.00, 99999.99, ErrorMessage = "El precio ser entre 0.00 y 99999.00")]
        public Nullable<decimal> sueldo { get; set; }

        [DisplayName("Compensación")]
        [Required(ErrorMessage = "Por favor ingrese la compensación.")]
        [Range(0.00, 99999.99, ErrorMessage = "El precio ser entre 0.00 y 99999.00")]
        public Nullable<decimal> compensacion { get; set; }

        [DisplayName("Descuento")]
        [Required(ErrorMessage = "Por favor ingrese el descuento.")]
        [Range(0.00, 99999.99, ErrorMessage = "El precio ser entre 0.00 y 99999.00")]
        public Nullable<decimal> descuento { get; set; }

        [DisplayName("Percepción bruta")]
        [Required(ErrorMessage = "Por favor ingrese la percepción bruta (sueldo + compensación - descuento).")]
        [Range(0.00, 99999.99, ErrorMessage = "El precio ser entre 0.00 y 99999.00")]
        public Nullable<decimal> percepcion { get; set; }


        [DisplayName("Pago realizado")]
        public Nullable<byte> pagado { get; set; }
    
        public virtual Empleado Empleado { get; set; }
    }
}
