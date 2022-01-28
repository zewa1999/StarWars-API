using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StarWarsAPI.DataProvider;
using StarWarsAPI.Server.Controllers;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Server.Tests.Controllers
{
    public class StarshipsControllerTests
    {
        private StarshipsController controller;
        private IStrategyGetCaller strategyGetCaller;

        private Mock<ILogger<StarshipsController>> loggerMock;
        private Mock<IDataProvider> dataProviderMock;
        private Mock<IIdentityMap> mapMock;

        [SetUp]
        public void Initialize()
        {
            loggerMock = new();
            dataProviderMock = new();
            mapMock = new();

            strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
            controller = new StarshipsController(loggerMock.Object, strategyGetCaller);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyListAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.StarshipsEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Starships>();
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnValidObjectsAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.StarshipsEndpoint)).Returns(Task.FromResult(TestUtils.GetStarshipsString()));

            var expected = TestUtils.DeserializeObjects<Starships>(TestUtils.GetStarshipsString());
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected.Count(), actual.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).Model, actual.ElementAt(i).Model);
                Assert.AreEqual(expected.ElementAt(i).Manufacturer, actual.ElementAt(i).Manufacturer);
                Assert.AreEqual(expected.ElementAt(i).CostInCredits, actual.ElementAt(i).CostInCredits);
                Assert.AreEqual(expected.ElementAt(i).Length, actual.ElementAt(i).Length);
                Assert.AreEqual(expected.ElementAt(i).MaxAtmospheringSpeed, actual.ElementAt(i).MaxAtmospheringSpeed);
                Assert.AreEqual(expected.ElementAt(i).Crew, actual.ElementAt(i).Crew);
                Assert.AreEqual(expected.ElementAt(i).Passengers, actual.ElementAt(i).Passengers);
                Assert.AreEqual(expected.ElementAt(i).CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected.ElementAt(i).Consumables, actual.ElementAt(i).Consumables);
                Assert.AreEqual(expected.ElementAt(i).HyperdriveRating, actual.ElementAt(i).HyperdriveRating);
                Assert.AreEqual(expected.ElementAt(i).MGLT, actual.ElementAt(i).MGLT);
                Assert.AreEqual(expected.ElementAt(i).StarshipClass, actual.ElementAt(i).StarshipClass);
                Assert.AreEqual(expected.ElementAt(i).Pilots, actual.ElementAt(i).Pilots);
                Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
                Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
            }
        }

        [Test]
        public async Task GetByValueAsyncShouldReturnEmpty()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.StarshipsEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Starships>();
            var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("name", "CR90 corvette")]
        [TestCase("model", "CR90 corvette")]
        [TestCase("manufacturer", "Corellian Engineering Corporation")]
        [TestCase("cost_in_credits", "3500000")]
        [TestCase("length", "150")]
        [TestCase("max_atmosphering_speed", "950")]
        [TestCase("crew", "30-165")]
        [TestCase("passengers", "600")]
        [TestCase("cargo_capacity", "3000000")]
        [TestCase("consumables", "1 year")]
        [TestCase("MGLT", "60")]
        [TestCase("hyperdrive_rating", "2.0")]
        [TestCase("starship_class", "corvette")]
        [Test]
        public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
        {
            var objectFilter = new ObjectFilter<Starships>();
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.StarshipsEndpoint)).Returns(Task.FromResult(TestUtils.GetStarshipsString()));

            objectFilter.Objects = TestUtils.DeserializeObjects<Starships>(TestUtils.GetStarshipsString());

            var expected = objectFilter.GetObjectsByProperty(propertyName, value);
            var actual = await controller.GetByValueAsync(propertyName, value);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).Model, actual.ElementAt(i).Model);
                Assert.AreEqual(expected.ElementAt(i).Manufacturer, actual.ElementAt(i).Manufacturer);
                Assert.AreEqual(expected.ElementAt(i).CostInCredits, actual.ElementAt(i).CostInCredits);
                Assert.AreEqual(expected.ElementAt(i).Length, actual.ElementAt(i).Length);
                Assert.AreEqual(expected.ElementAt(i).MaxAtmospheringSpeed, actual.ElementAt(i).MaxAtmospheringSpeed);
                Assert.AreEqual(expected.ElementAt(i).Crew, actual.ElementAt(i).Crew);
                Assert.AreEqual(expected.ElementAt(i).Passengers, actual.ElementAt(i).Passengers);
                Assert.AreEqual(expected.ElementAt(i).CargoCapacity, actual.ElementAt(i).CargoCapacity);
                Assert.AreEqual(expected.ElementAt(i).Consumables, actual.ElementAt(i).Consumables);
                Assert.AreEqual(expected.ElementAt(i).HyperdriveRating, actual.ElementAt(i).HyperdriveRating);
                Assert.AreEqual(expected.ElementAt(i).MGLT, actual.ElementAt(i).MGLT);
                Assert.AreEqual(expected.ElementAt(i).StarshipClass, actual.ElementAt(i).StarshipClass);
                Assert.AreEqual(expected.ElementAt(i).Pilots, actual.ElementAt(i).Pilots);
                Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
                Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
            }
        }
    }
}
