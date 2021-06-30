//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP_2184587.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.producto_compra = new HashSet<producto_compra>();
        }
    
        public int id { get; set; }

        [Required(ErrorMessage = "No puede ir Vacio")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Debe ser maximo 20 y minimo 2 caracteres")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "No puede ir Vacio")]
        public Nullable<int> percio_unitario { get; set; }

        [Required(ErrorMessage = "No puede ir Vacio")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "No puede ir Vacio")]
        public Nullable<int> cantidad { get; set; }

        [Required(ErrorMessage = "No puede ir Vacio")]
        public Nullable<int> id_proveedor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<producto_compra> producto_compra { get; set; }
        public virtual proveedor proveedor { get; set; }
    }
}
