using CarRental.Business.Entities;
using Core.Common.Contract;
using System;
using System.Collections.Generic;

namespace CarRental.Business.Common
{
    public interface ICarRentalEngine: IBusinessEngine
    {

        bool IsCarCurrentlyRented(long? carId, int accountId);
        bool IsCarCurrentlyRented(long? carId);
        bool IsCarAvailableForRental(long? carId, DateTimeOffset pickupDate, DateTimeOffset returnDate,
                                     IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars);
        Rental RentCarToCustomer(string loginEmail, long? carId, DateTimeOffset rentalDate, DateTimeOffset dateDueBack);
    }
}
