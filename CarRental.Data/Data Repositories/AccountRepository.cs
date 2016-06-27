using CarRental.Business.Entities;
using CarRental.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace CarRental.Data
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        protected override Account AddEntity(CarRentalContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(CarRentalContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.AccountSet
                   select e;
        }

        protected override Account GetEntity(CarRentalContext entityContext, long? id)
        {
            var query = (from e in entityContext.AccountSet
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public Account GetByLogin(string login)
        {
            using (CarRentalContext entityContext = new CarRentalContext())
            {
                return (from a in entityContext.AccountSet
                        where a.LoginEmail == login
                        select a).FirstOrDefault();
            }
        }
    }
}
