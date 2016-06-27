using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using Core.Common.Contracts;

namespace CarRental.Data.Contracts
{
    public interface IRentalRepository : IDataRepository<Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(long? carId);
        Rental GetCurrentRentalByCar(long? carId);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<Rental> GetRentalHistoryByAccount(long? accountId);
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
    }
}
