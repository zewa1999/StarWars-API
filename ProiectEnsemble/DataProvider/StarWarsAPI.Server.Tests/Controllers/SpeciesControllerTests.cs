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
    public class SpeciesControllerTests
    {
        private SpeciesController controller;
        private IStrategyGetCaller strategyGetCaller;

        private Mock<ILogger<SpeciesController>> loggerMock;
        private Mock<IDataProvider> dataProviderMock;
        private Mock<IIdentityMap> mapMock;

        [SetUp]
        public void Initialize()
        {
            loggerMock = new();
            dataProviderMock = new();
            mapMock = new();

            strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
            controller = new SpeciesController(loggerMock.Object, strategyGetCaller);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyListAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.SpeciesEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Species>();
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnValidObjectsAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.SpeciesEndpoint)).Returns(Task.FromResult(TestUtils.GetSpeciesString()));

            var expected = TestUtils.DeserializeObjects<Species>(TestUtils.GetSpeciesString());
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected.Count(), actual.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected[i].Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected[i].Classification, actual.ElementAt(i).Classification);
                Assert.AreEqual(expected[i].Designation, actual.ElementAt(i).Designation);
                Assert.AreEqual(expected[i].AverageHeight, actual.ElementAt(i).AverageHeight);
                Assert.AreEqual(expected[i].SkinColors, actual.ElementAt(i).SkinColors);
                Assert.AreEqual(expected[i].HairColors, actual.ElementAt(i).HairColors);
                Assert.AreEqual(expected[i].EyeColors, actual.ElementAt(i).EyeColors);
                Assert.AreEqual(expected[i].AverageLifespan, actual.ElementAt(i).AverageLifespan);
                Assert.AreEqual(expected[i].Homeworld, actual.ElementAt(i).Homeworld);
                Assert.AreEqual(expected[i].Language, actual.ElementAt(i).Language);
                Assert.AreEqual(expected[i].People, actual.ElementAt(i).People);
                Assert.AreEqual(expected[i].Films, actual.ElementAt(i).Films);
                Assert.AreEqual(expected[i].Url, actual.ElementAt(i).Url);
            }
        }

        [Test]
        public async Task GetByValueAsyncShouldReturnEmpty()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.SpeciesEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Species>();
            var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("name", "Human")]
        [TestCase("classification", "mammal")]
        [TestCase("designation", "sentient")]
        [TestCase("average_height", "180")]
        [TestCase("skin_colors", "caucasian, black, asian, hispanic")]
        [TestCase("hair_colors", "blonde, brown, black, red")]
        [TestCase("eye_colors", "brown, blue, green, hazel, grey, amber")]
        [TestCase("average_lifespan", "120")]
        [TestCase("language", "Galactic Basic")]
        [Test]
        public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
        {
            var objectFilter = new ObjectFilter<Species>();
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.SpeciesEndpoint)).Returns(Task.FromResult(TestUtils.GetSpeciesString()));

            objectFilter.Objects = TestUtils.DeserializeObjects<Species>(TestUtils.GetSpeciesString());

            var expected = objectFilter.GetObjectsByProperty(propertyName, value);
            var actual = await controller.GetByValueAsync(propertyName, value);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.AreEqual(expected.ElementAt(i).Classification, actual.ElementAt(i).Classification);
                Assert.AreEqual(expected.ElementAt(i).Designation, actual.ElementAt(i).Designation);
                Assert.AreEqual(expected.ElementAt(i).AverageHeight, actual.ElementAt(i).AverageHeight);
                Assert.AreEqual(expected.ElementAt(i).SkinColors, actual.ElementAt(i).SkinColors);
                Assert.AreEqual(expected.ElementAt(i).HairColors, actual.ElementAt(i).HairColors);
                Assert.AreEqual(expected.ElementAt(i).EyeColors, actual.ElementAt(i).EyeColors);
                Assert.AreEqual(expected.ElementAt(i).AverageLifespan, actual.ElementAt(i).AverageLifespan);
                Assert.AreEqual(expected.ElementAt(i).Homeworld, actual.ElementAt(i).Homeworld);
                Assert.AreEqual(expected.ElementAt(i).Language, actual.ElementAt(i).Language);
                Assert.AreEqual(expected.ElementAt(i).People, actual.ElementAt(i).People);
                Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
                Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
            }
        }
    }
}
