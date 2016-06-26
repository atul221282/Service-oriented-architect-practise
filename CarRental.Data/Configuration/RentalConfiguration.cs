using CarRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Configuration
{
    public class RentalConfiguration : EntityTypeConfiguration<Rental>
    {
        public RentalConfiguration()
        {
            HasKey<long?>(e => e.Id).Ignore(e => e.EntityId);
        }
    }
}
