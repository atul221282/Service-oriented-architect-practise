using CarRental.Client.Entities;
using Core.Common.Contract;
using Core.Common.Exceptiona;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService: IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Car UpdateCar(Car car);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCar(int carId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(long? Id);

        [OperationContract]
        List<Car> GetAllCars();

        [OperationContract]
        List<Car> GetAvailableCars(DateTimeOffset pickUpDate, DateTimeOffset returnDate);
    }
}
