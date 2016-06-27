using CarRental.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using CarRental.Data.Contracts;

namespace CarRental.Business.Business_Engines
{
    [Export(typeof(ICarRentalEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRentalEngine : ICarRentalEngine
    {
        [ImportingConstructor]
        public CarRentalEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IDataRepositoryFactory _DataRepositoryFactory;

        public bool IsCarCurrentlyRented(long? carId, int accountId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null && currentRental.AccountId == accountId)
                rented = true;

            return rented;
        }

        public bool IsCarCurrentlyRented(long? carId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Rental currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null)
                rented = true;

            return rented;
        }

        public bool IsCarAvailableForRental(long? carId, DateTimeOffset pickupDate, DateTimeOffset returnDate,
                                            IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars)
        {
            bool available = true;

            Reservation reservation = reservedCars.Where(item => item.CarId == carId).FirstOrDefault();
            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
            {
                available = false;
            }

            if (available)
            {
                Rental rental = rentedCars.Where(item => item.CarId == carId).FirstOrDefault();
                if (rental != null && (pickupDate <= rental.DateDue))
                    available = false;
            }

            return available;
        }

        public Rental RentCarToCustomer(string loginEmail, long? carId, DateTimeOffset rentalDate, DateTimeOffset dateDueBack)
        {
            if (rentalDate > DateTime.Now)
                throw new Exception(string.Format("Cannot rent for date {0} yet.", rentalDate.DateTime.ToShortDateString()));

            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            bool carIsRented = IsCarCurrentlyRented((int)carId);
            if (carIsRented)
                throw new Exception(string.Format("Car {0} is already rented.", carId));

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
                throw new Exception(string.Format("No account found for login '{0}'.", loginEmail));

            Rental rental = new Rental()
            {
                AccountId = account.Id.Value,
                CarId = carId.Value,
                DateRented = rentalDate,
                DateDue = dateDueBack
            };

            Rental savedEntity = rentalRepository.Add(rental);

            return savedEntity;
        }

       
    }
}
