using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using Core.Common.Contract;

namespace CarRental.Data.Contracts
{
    public interface ICarRepository : IDataRepository<Car>
    {
    }
}
