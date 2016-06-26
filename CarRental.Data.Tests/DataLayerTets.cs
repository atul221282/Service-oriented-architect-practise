using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CarRental.Data.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using CarRental.Business.Bootstrapper;
using Moq;

namespace CarRental.Data.Tests
{
    [TestClass]
    public class DataLayerTets
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();

            IEnumerable<Car> cars = repositoryTest.GetCars();

            Assert.IsTrue(cars != null);
        }

        [TestMethod]
        public void test_repository_mocking()
        {
            List<Car> cars = new List<Car>()
            {
                new Car() { Id = 1, Description = "Mustang" },
                new Car() { Id = 2, Description = "Corvette" }
            };

            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            RepositoryTestClass repositoryTest = new RepositoryTestClass(mockCarRepository.Object);

            IEnumerable<Car> ret = repositoryTest.GetCars();

            Assert.IsTrue(ret == cars);
        }

    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(ICarRepository carRepository)
        {
            _CarRepository = carRepository;
        }

        [Import]
        ICarRepository _CarRepository;

        public IEnumerable<Car> GetCars()
        {
            IEnumerable<Car> cars = _CarRepository.Get();

            return cars;

        }
    }
}
