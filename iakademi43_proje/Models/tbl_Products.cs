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
    
    public partial class tbl_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Products()
        {
            this.tbl_Orders = new HashSet<tbl_Orders>();
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<long> Stok { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> statusID { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public string Keywords { get; set; }
        public Nullable<int> Kdv { get; set; }
        public Nullable<int> OneCikanlar { get; set; }
        public Nullable<int> CokSatanlar { get; set; }
        public Nullable<int> BunaBakanlar { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual tbl_Categories tbl_Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Orders> tbl_Orders { get; set; }
        public virtual tbl_Status tbl_Status { get; set; }
        public virtual tbl_Suppliers tbl_Suppliers { get; set; }
    }
}