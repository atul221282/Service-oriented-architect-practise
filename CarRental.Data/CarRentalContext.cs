namespace CarRental.Data
{
    using Business.Entities;
    using Configuration;
    using Core.Common.Contracts;
    using Core.Common.Core;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Runtime.Serialization;
    public class CarRentalContext : DbContext
    {
        // Your context has been configured to use a 'CarRental' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CarRental.Data.CarRentalModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CarRentalModel' 
        // connection string in the application configuration file.
        public CarRentalContext()
            : base("name=CarRental")
        {
            //Database.SetInitializer<CarRentalContext>(null);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Account> AccountSet { get; set; }
        public virtual DbSet<Car> CarSet { get; set; }

        public virtual DbSet<Rental> RentalSet { get; set; }

        public virtual DbSet<Reservation> ReservationSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Account>().HasKey<long?>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Car>().HasKey<long?>(e => e.Id).Ignore(e => e.EntityId);
            
            modelBuilder.Entity<Reservation>().HasKey<long?>(e => e.Id).Ignore(e => e.EntityId);

            modelBuilder.Configurations.Add(new RentalConfiguration());

            modelBuilder.Configurations.Add(new CarConfiguration());


        }
    }

    
}