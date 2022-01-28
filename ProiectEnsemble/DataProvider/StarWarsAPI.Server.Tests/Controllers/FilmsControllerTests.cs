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
    public class FilmsControllerTests
    {
        private FilmsController controller;
        private IStrategyGetCaller strategyGetCaller;

        private Mock<ILogger<FilmsController>> loggerMock;
        private Mock<IDataProvider> dataProviderMock;
        private Mock<IIdentityMap> mapMock;

        [SetUp]
        public void Initialize()
        {
            loggerMock = new();
            dataProviderMock = new();
            mapMock = new();

            strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
            controller = new FilmsController(loggerMock.Object, strategyGetCaller);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyListAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.FilmsEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Films>();
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnValidObjectsAsync()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.FilmsEndpoint)).Returns(Task.FromResult(TestUtils.GetFilmsString()));

            var expected = TestUtils.DeserializeObjects<Films>(TestUtils.GetFilmsString());
            var actual = await controller.GetAllAsync();

            Assert.AreEqual(expected.Count(), actual.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Title, actual.ElementAt(i).Title);
                Assert.AreEqual(expected.ElementAt(i).EpisodeId, actual.ElementAt(i).EpisodeId);
                Assert.AreEqual(expected.ElementAt(i).OpeningCrawl, actual.ElementAt(i).OpeningCrawl);
                Assert.AreEqual(expected.ElementAt(i).Director, actual.ElementAt(i).Director);
                Assert.AreEqual(expected.ElementAt(i).Producer, actual.ElementAt(i).Producer);
                Assert.AreEqual(expected.ElementAt(i).ReleaseDate, actual.ElementAt(i).ReleaseDate);
            }
        }

        [Test]
        public async Task GetByValueAsyncShouldReturnEmpty()
        {
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.FilmsEndpoint)).Returns(Task.FromResult(string.Empty));

            var expected = new List<Films>();
            var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("title", "A New Hope")]
        [TestCase("director", "George Lucas")]
        [TestCase("producer", "Gary Kurtz, Rick McCallum")]
        [TestCase("release_date", "1977-05-25")]
        [Test]
        public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
        {
            var objectFilter = new ObjectFilter<Films>();
            dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.FilmsEndpoint)).Returns(Task.FromResult(TestUtils.GetFilmsString()));

            objectFilter.Objects = TestUtils.DeserializeObjects<Films>(TestUtils.GetFilmsString());

            var expected = objectFilter.GetObjectsByProperty(propertyName, value);
            var actual = await controller.GetByValueAsync(propertyName, value);

            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i).Title, actual.ElementAt(i).Title);
                Assert.AreEqual(expected.ElementAt(i).EpisodeId, actual.ElementAt(i).EpisodeId);
                Assert.AreEqual(expected.ElementAt(i).OpeningCrawl, actual.ElementAt(i).OpeningCrawl);
                Assert.AreEqual(expected.ElementAt(i).Director, actual.ElementAt(i).Director);
                Assert.AreEqual(expected.ElementAt(i).Producer, actual.ElementAt(i).Producer);
                Assert.AreEqual(expected.ElementAt(i).ReleaseDate, actual.ElementAt(i).ReleaseDate);
            }
        }
    }
}
