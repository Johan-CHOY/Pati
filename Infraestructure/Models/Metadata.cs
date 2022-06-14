using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class UsuarioMetadata
    {
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        //    ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        //[Display(Name = "Correo electrónico")]
        //[Required]
        
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasennia { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[A-Z a-z0-9ÑñáéíóúÁÉÍÓÚ\\-\\_]+$",
            ErrorMessage = "El Nombre debe ser alfanumérico.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Tipo de usuario")]
        public Nullable<int> IdTipoUsuario { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario1 { get; set; }
        public Nullable<int> Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
    internal partial class TipoUsuarioMetadata
    {
        [Display(Name = "Tipo de usuario")]
        public string Descripcion { get; set; }
    }
}
