using CarRental.Client.Entities;
using Core.Common.Contract;
using System.ServiceModel;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService:IServiceContract
    {
        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //[FaultContract(typeof(AuthorizationValidationException))]
        Account GetCustomerAccountInfo(string loginEmail);

        [OperationContract]
        //[FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCustomerAccountInfo(Account account);
    }
}
