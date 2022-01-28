using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StarWarsAPI.DataProvider;
using StarWarsAPI.Server.Controllers;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsAPI.Server.Tests.Controllers
{
    public class VehicleControllerTests
    {
        private VehiclesController controller;
        private IStrategyGetCaller strategyGetCaller;

        private Mock<ILogger<VehiclesController>> loggerMock;
        private Mock<IDataProvider> dataProviderMock;
        private Mock<IIdentityMap> mapMock;

        [SetUp]
        public void Initialize()
        {
            loggerMock = new();
            dataProviderMock = new();
            mapMock = new();

            strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
            controller = new VehiclesController(loggerMock.Object, strategyGetCaller);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyListAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.VehiclesEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Vehicles>();
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnValidObjectsAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.VehiclesEndpoint)).Returns(Task.FromResult(TestUtils.GetVehiclesString()));

            var expected = TestUtils.DeserializeObjects<Vehicles>(TestUtils.GetVehiclesString());
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected.Count(), actual.Count());

            for(int i=0;i<expected.Count();i++)
            {
                Assert.AreEqual(expected[i].Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected[i].CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected[i].Model, actual.ElementAt(i).Model);
                Assert.AreEqual(expected[i].Manufacturer, actual.ElementAt(i).Manufacturer);
                Assert.AreEqual(expected[i].CostInCredits, actual.ElementAt(i).CostInCredits);
                Assert.AreEqual(expected[i].Length, actual.ElementAt(i).Length);
                Assert.AreEqual(expected[i].MaxAtmospheringSpeed, actual.ElementAt(i).MaxAtmospheringSpeed);
                Assert.AreEqual(expected[i].Crew, actual.ElementAt(i).Crew);
                Assert.AreEqual(expected[i].Passengers, actual.ElementAt(i).Passengers);
                Assert.AreEqual(expected[i].CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected[i].Consumables, actual.ElementAt(i).Consumables);
                Assert.AreEqual(expected[i].VehicleClass, actual.ElementAt(i).VehicleClass);
                Assert.AreEqual(expected[i].Pilots, actual.ElementAt(i).Pilots);
                Assert.AreEqual(expected[i].Films, actual.ElementAt(i).Films);
            }
        }

        [Test]
        public async Task GetByValueAsyncShouldReturnEmpty()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.VehiclesEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Vehicles>();
            var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("name", "Sand Crawler")]
        [TestCase("model", "Digger Crawler")]
        [TestCase("manufacturer", "Corellia Mining Corporation")]
        [TestCase("cost_in_credits", "150000")]
        [TestCase("length", "36.8")]
        [TestCase("max_atmosphering_speed", "30")]
        [TestCase("crew", "46")]
        [TestCase("passengers", "30")]
        [TestCase("cargo_capacity", "50000")]
        [TestCase("consumables", "2 months")]
        [TestCase("vehicle_class", "wheeled")]
        [Test]
        public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
        {
            var objectFilter = new ObjectFilter<Vehicles>();
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.VehiclesEndpoint)).Returns(Task.FromResult(TestUtils.GetVehiclesString()));

            objectFilter.Objects = TestUtils.DeserializeObjects<Vehicles>(TestUtils.GetVehiclesString());

            var expected = objectFilter.GetObjectsByProperty(propertyName, value);
            var actual = await controller.GetByValueAsync(propertyName, value);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected.ElementAt(i).Model, actual.ElementAt(i).Model);
                Assert.AreEqual(expected.ElementAt(i).Manufacturer, actual.ElementAt(i).Manufacturer);
                Assert.AreEqual(expected.ElementAt(i).CostInCredits, actual.ElementAt(i).CostInCredits);
                Assert.AreEqual(expected.ElementAt(i).Length, actual.ElementAt(i).Length);
                Assert.AreEqual(expected.ElementAt(i).MaxAtmospheringSpeed, actual.ElementAt(i).MaxAtmospheringSpeed);
                Assert.AreEqual(expected.ElementAt(i).Crew, actual.ElementAt(i).Crew);
                Assert.AreEqual(expected.ElementAt(i).Passengers, actual.ElementAt(i).Passengers);
                Assert.AreEqual(expected.ElementAt(i).CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected.ElementAt(i).Consumables, actual.ElementAt(i).Consumables);
                Assert.AreEqual(expected.ElementAt(i).VehicleClass, actual.ElementAt(i).VehicleClass);
                Assert.AreEqual(expected.ElementAt(i).Pilots, actual.ElementAt(i).Pilots);
                Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
            }
        }
    }
}
