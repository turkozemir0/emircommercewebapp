//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iakademi43_proje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Suppliers()
        {
            this.tbl_Products = new HashSet<tbl_Products>();
        }
    
        public int SupplierID { get; set; }
        public string BrandName { get; set; }
        public string PhotoPath { get; set; }
        public Nullable<bool> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Products> tbl_Products { get; set; }
    }
}