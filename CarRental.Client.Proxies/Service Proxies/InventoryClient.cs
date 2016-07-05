using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using Core.Common.ServiceModel;
using System.ServiceModel;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        public void DeleteCar(int carId)
        {
            Channel.DeleteCar(carId);
        }

        public List<Car> GetAllCars()
        {
            return Channel.GetAllCars();
        }

        public List<Car> GetAvailableCars(DateTimeOffset pickUpDate, DateTimeOffset returnDate)
        {
           return Channel.GetAvailableCars(pickUpDate, returnDate);
        }

        public Car GetCar(long? Id)
        {
            return Channel.GetCar(Id);
        }

        public Car UpdateCar(Car car)
        {
            return Channel.UpdateCar(car);
        }
    }
}
