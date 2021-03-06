﻿using CarRental.Business.Entities;
using Core.Common.Exceptiona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Contracts
{
    [ServiceContract]
    public interface IInventoryService
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
