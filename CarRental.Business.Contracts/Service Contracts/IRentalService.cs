using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using CarRental.Business.Entities;

namespace CarRental.Business.Contracts
{
    [ServiceContract]
    public interface IRentalService
    {
        [OperationContract(Name = "RentCarToCustomerImmediately")]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(FaultException))]
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(FaultException))]
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AcceptCarReturn(int carId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        IEnumerable<Rental> GetRentalHistory(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Reservation GetReservation(int reservationId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ExecuteRentalFromReservation(int reservationId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CancelReservation(int reservationId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        CustomerReservationData[] GetCurrentReservations();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        CustomerReservationData[] GetCustomerReservations(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Rental GetRental(int rentalId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        CustomerRentalData[] GetCurrentRentals();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Reservation[] GetDeadReservations();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        bool IsCarCurrentlyRented(int carId);
    }
}
