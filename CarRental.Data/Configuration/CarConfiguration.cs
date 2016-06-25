using CarRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Configuration
{
    public class CarConfiguration : EntityTypeConfiguration<Car>
    {
        public CarConfiguration()
        {
            Ignore(x => x.CurrentlyRented);
        }
    }
}
