using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using Core.Common.Extensions;

namespace CarRental.Data
{
    [Export(typeof(IReservationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReservationRepository : DataRepositoryBase<Reservation>, IReservationRepository
    {
        protected override Reservation AddEntity(CarRentalContext entityContext, Reservation entity)
        {
            return entityContext.ReservationSet.Add(entity);
        }

        protected override Reservation UpdateEntity(CarRentalContext entityContext, Reservation entity)
        {
            return (from e in entityContext.ReservationSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Reservation> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.ReservationSet
                   select e;
        }

        protected override Reservation GetEntity(CarRentalContext entityContext, long? id)
        {
            var query = (from e in entityContext.ReservationSet
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo()
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from r in entityContext.ReservationSet
                            join a in entityContext.AccountSet on r.AccountId equals a.Id
                            join c in entityContext.CarSet on r.CarId equals c.Id
                            select new CustomerReservationInfo()
                            {
                                Customer = a,
                                Car = c,
                                Reservation = r
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from r in entityContext.ReservationSet
                            where r.RentalDate < pickupDate
                            select r;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(long? accountId)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                var query = from r in entityContext.ReservationSet
                            join a in entityContext.AccountSet on r.AccountId equals a.Id
                            join c in entityContext.CarSet on r.CarId equals c.Id
                            where r.AccountId == accountId
                            select new CustomerReservationInfo()
                            {
                                Customer = a,
                                Car = c,
                                Reservation = r
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
