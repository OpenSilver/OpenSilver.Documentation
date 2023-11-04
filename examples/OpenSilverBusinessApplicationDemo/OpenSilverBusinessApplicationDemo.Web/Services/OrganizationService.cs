using OpenRiaServices.DomainServices.EntityFramework;
using OpenRiaServices.DomainServices.Hosting;
using OpenRiaServices.DomainServices.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace OpenSilverBusinessApplicationDemo.Web
{
    // Implements application logic using the AdventureWorksLT2022Entities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class OrganizationService : DbDomainService<AdventureWorksLT2022Entities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Customers' query.
        public IQueryable<Customer> GetCustomers()
        {
            return this.DbContext.Customers;
        }
    }
}


