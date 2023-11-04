using OpenRiaServices.DomainServices.Hosting;
using OpenRiaServices.DomainServices.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenSilverBusinessApplicationDemo.Web
{
    // The MetadataTypeAttribute identifies CustomerMetadata as the class
    // that carries additional metadata for the Customer class.
    [MetadataTypeAttribute(typeof(Customer.CustomerMetadata))]
    public partial class Customer
    {

        // This class allows you to attach custom attributes to properties
        // of the Customer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CustomerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CustomerMetadata()
            {
            }

            public string CompanyName { get; set; }

            public ICollection<CustomerAddress> CustomerAddresses { get; set; }

            public int CustomerID { get; set; }

            public string EmailAddress { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string MiddleName { get; set; }

            public DateTime ModifiedDate { get; set; }

            public bool NameStyle { get; set; }

            public string PasswordHash { get; set; }

            public string PasswordSalt { get; set; }

            public string Phone { get; set; }

            public Guid rowguid { get; set; }

            public ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }

            public string SalesPerson { get; set; }

            public string Suffix { get; set; }

            public string Title { get; set; }
        }
    }
}
