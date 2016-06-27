using Core.Common.Contracts;
using Core.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using System.ServiceModel;
using CarRental.Business.Contracts;
using Core.Common.Exceptiona;
using System;
using Core.Common.Contract;
using CarRental.Business.Common;
using System.Security.Permissions;
using CarRental.Common;

namespace CarRental.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall
       , ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase, IInventoryService
    {
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        #region Contructor

        public InventoryManager()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            this._DataRepositoryFactory = dataRepositoryFactory;
        }

        public InventoryManager(IBusinessEngineFactory BusinessEngineFactory)
        {
            this._BusinessEngineFactory = BusinessEngineFactory;
        }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory BusinessEngineFactory)
        {
            this._DataRepositoryFactory = dataRepositoryFactory;
            this._BusinessEngineFactory = BusinessEngineFactory;
        }

        #endregion

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Car UpdateCar(Car car)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRepository carRepo = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                Car updateEntity = null;
                if (!car.Id.HasValue)
                    updateEntity = carRepo.Add(car);
                else
                    updateEntity = carRepo.Update(car);

                return updateEntity;
            });
        }

      

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public void DeleteCar(int carId)
        {
            ExecuteFaultHandleOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

                carRepository.Remove(carId);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
        public List<Car> GetAllCars()
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRepository carRepo = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepo = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                var cars = carRepo.Get().ToList();
                var rentedCars = rentalRepo.GetCurrentlyRentedCars();

                cars.ForEach((x) =>
                {
                    var rentedCar = rentedCars.Where(y => y.CarId == x.Id).FirstOrDefault();
                    x.CurrentlyRented = (rentedCar != null);
                });

                return cars;
            });
        }


        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
        public Car GetCar(long? Id)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRepository carRepo = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                var car = carRepo.Get(Id);
                if (car == null)
                {
                    NotFoundException ex = new NotFoundException("Car is null");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return car;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
        public List<Car> GetAvailableCars(DateTimeOffset pickUpDate, DateTimeOffset returnDate)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRepository carRepo = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepo = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IReservationRepository reservationRepo = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                IEnumerable<Car> allCars = carRepo.Get();
                IEnumerable<Rental> rentedCars = rentalRepo.GetCurrentlyRentedCars();
                var reservedCars = reservationRepo.Get();
                List<Car> availableCars = new List<Car>();

                foreach (Car car in allCars)
                {
                    if (carRentalEngine.IsCarAvailableForRental(car.Id, pickUpDate, returnDate, rentedCars, reservedCars))
                    {
                        availableCars.Add(car);
                    }
                }
                return availableCars.ToList();
            });
        }
    }
}