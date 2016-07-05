using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Data.Contracts;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using CarRental.Business.Business_Engines;
using Moq;

namespace CarRental.Business.Tests
{
    [TestClass]
    public class CarRentalEngineTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rental rental = new Rental()
            {
                CarId = 1
            };

            Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
            mockRentalRepository.Setup(obj => obj.GetCurrentRentalByCar(1)).Returns(rental);

            Mock<IDataRepositoryFactory> mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

            CarRentalEngine engine = new CarRentalEngine(mockRepositoryFactory.Object);

            bool try1 = engine.IsCarCurrentlyRented(2);

            Assert.IsFalse(try1);

            bool try2 = engine.IsCarCurrentlyRented(1);

            Assert.IsTrue(try2);
        }
    }
}
