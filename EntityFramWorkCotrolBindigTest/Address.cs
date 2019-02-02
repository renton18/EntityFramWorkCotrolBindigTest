namespace EntityFramWorkCotrolBindigTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.Address")]
    public partial class Address
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            //BusinessEntityAddress = new HashSet<BusinessEntityAddress>();
        }

        public int AddressID { get; set; }

        [Required]
        [StringLength(60)]
        public string AddressLine1 { get; set; }

        [StringLength(60)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        public int StateProvinceID { get; set; }

        [Required]
        [StringLength(15)]
        public string PostalCode { get; set; }


        public DateTime ModifiedDate { get; set; }
    }
}
