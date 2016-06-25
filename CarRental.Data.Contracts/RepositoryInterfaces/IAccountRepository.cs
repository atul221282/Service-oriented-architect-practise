using CarRental.Business.Entities;
using Core.Common.Contract;

namespace CarRental.Data.Contracts
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}
