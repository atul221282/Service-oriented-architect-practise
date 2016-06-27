using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using CarRental.Business.Common;
using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using CarRental.Common;
using CarRental.Data.Contracts;
using Core.Common.Contracts;

using System.Security.Permissions;
using Core.Common.Contract;

namespace CarRental.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class RentalManager : ManagerBase, IRentalService
    {
        public RentalManager()
        {
        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public RentalManager(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public RentalManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account authAcct = accountRepository.GetByLogin(loginName);
            if (authAcct == null)
            {
                throw new FaultException(string.Format("Cannot find account for login name {0} to use for security trimming.", loginName));
            }

            return authAcct;
        }

        #region IRentalService operations

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                try
                {
                    Rental rental = carRentalEngine.RentCarToCustomer(loginEmail, carId, DateTime.Now, dateDueBack);

                    return rental;
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                try
                {
                    Rental rental = carRentalEngine.RentCarToCustomer(loginEmail, carId, rentalDate, dateDueBack);

                    return rental;
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public void AcceptCarReturn(int carId)
        {
            ExecuteFaultHandleOperation(() =>
            {
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                Rental rental = rentalRepository.GetCurrentRentalByCar(carId);
                if (rental == null)
                {

                    throw new FaultException(string.Format("Car {0} is not currently rented.", carId));
                }

                rental.DateReturned = DateTime.Now;

                Rental updatedRentalEntity = rentalRepository.Update(rental);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public IEnumerable<Rental> GetRentalHistory(string loginEmail)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    throw new FaultException(string.Format("No account found for login '{0}'.", loginEmail));
                }

                ValidateAuthorization(account);

                IEnumerable<Rental> rentalHistory = rentalRepository.GetRentalHistoryByAccount(account.Id);

                return rentalHistory;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Reservation GetReservation(int reservationId)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Reservation reservation = reservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    throw new FaultException(string.Format("No reservation found for id '{0}'.", reservationId));
                }

                ValidateAuthorization(reservation);

                return reservation;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    throw new FaultException(string.Format("No account found for login '{0}'.", loginEmail));
                }

                ValidateAuthorization(account);

                Reservation reservation = new Reservation()
                {
                    AccountId = account.Id.Value,
                    CarId = carId,
                    RentalDate = rentalDate,
                    ReturnDate = returnDate
                };

                Reservation savedEntity = reservationRepository.Add(reservation);

                return savedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public void ExecuteRentalFromReservation(int reservationId)
        {
            ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                Reservation reservation = reservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    throw new FaultException(string.Format("Reservation {0} is not found.", reservationId));
                }

                Account account = accountRepository.Get(reservation.AccountId);
                if (account == null)
                {
                    throw new FaultException(string.Format("No account found for account ID '{0}'.", reservation.AccountId));
                }

                try
                {
                    Rental rental = carRentalEngine.RentCarToCustomer(account.LoginEmail, reservation.CarId, reservation.RentalDate, reservation.ReturnDate);
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }


                reservationRepository.Remove(reservation);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public void CancelReservation(int reservationId)
        {
            ExecuteFaultHandleOperation(() =>
            {
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Reservation reservation = reservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    throw new FaultException(string.Format("No reservation found found for ID '{0}'.", reservationId));
                }

                ValidateAuthorization(reservation);

                reservationRepository.Remove(reservationId);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public CustomerReservationData[] GetCurrentReservations()
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

                IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCurrentCustomerReservationInfo();
                foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
                {
                    reservationData.Add(new CustomerReservationData()
                    {
                        ReservationId = (int)reservationInfo.Reservation.Id,
                        Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                        CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                        RentalDate = reservationInfo.Reservation.RentalDate.DateTime,
                        ReturnDate = reservationInfo.Reservation.ReturnDate.DateTime
                    });
                }

                return reservationData.ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public CustomerReservationData[] GetCustomerReservations(string loginEmail)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    throw new FaultException(string.Format("No account found for login '{0}'.", loginEmail));
                }

                ValidateAuthorization(account);

                List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

                IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCustomerOpenReservationInfo(account.Id);
                foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
                {
                    reservationData.Add(new CustomerReservationData()
                    {
                        ReservationId = (int)reservationInfo.Reservation.Id.Value,
                        Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                        CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                        RentalDate = reservationInfo.Reservation.RentalDate.DateTime,
                        ReturnDate = reservationInfo.Reservation.ReturnDate.DateTime
                    });
                }

                return reservationData.ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Rental GetRental(int rentalId)
        {

            return ExecuteFaultHandleOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                Rental rental = rentalRepository.Get(rentalId);
                if (rental == null)
                {
                    throw new FaultException(string.Format("No rental record found for id '{0}'.", rentalId));
                }

                ValidateAuthorization(rental);

                return rental;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public CustomerRentalData[] GetCurrentRentals()
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

                List<CustomerRentalData> rentalData = new List<CustomerRentalData>();

                IEnumerable<CustomerRentalInfo> rentalInfoSet = rentalRepository.GetCurrentCustomerRentalInfo();
                foreach (CustomerRentalInfo rentalInfo in rentalInfoSet)
                {
                    rentalData.Add(new CustomerRentalData()
                    {
                        RentalId = rentalInfo.Rental.Id,
                        Car = rentalInfo.Car.Color + " " + rentalInfo.Car.Year + " " + rentalInfo.Car.Description,
                        CustomerName = rentalInfo.Customer.FirstName + " " + rentalInfo.Customer.LastName,
                        DateRented = rentalInfo.Rental.DateRented.DateTime,
                        ExpectedReturn = rentalInfo.Rental.DateDue.DateTime
                    });
                }

                return rentalData.ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public Reservation[] GetDeadReservations()
        {
            return ExecuteFaultHandleOperation(() =>
            {
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                IEnumerable<Reservation> reservations = reservationRepository.GetReservationsByPickupDate(DateTime.Now.AddDays(-1));

                return (reservations != null ? reservations.ToArray() : null);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        public bool IsCarCurrentlyRented(int carId)
        {
            return ExecuteFaultHandleOperation(() =>
            {
                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

                return carRentalEngine.IsCarCurrentlyRented(carId);
            });
        }

        #endregion
    }
}
