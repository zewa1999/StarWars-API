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

namespace StarWarsAPI.Server.Tests.Controllers;

public class PlanetsControllerTests
{
    private PlanetsController controller;
    private IStrategyGetCaller strategyGetCaller;

    private Mock<ILogger<PlanetsController>> loggerMock;
    private Mock<IDataProvider> dataProviderMock;
    private Mock<IIdentityMap> mapMock;

    [SetUp]
    public void Initialize()
    {
        loggerMock = new();
        dataProviderMock = new();
        mapMock = new();

        strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
        controller = new PlanetsController(loggerMock.Object, strategyGetCaller);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnEmptyListAsync()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PlanetsEndpoint)).Returns(Task.FromResult(string.Empty));

        var expected = new List<Planets>();
        var actual = await controller.GetAllAsync();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnValidObjectsAsync()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PlanetsEndpoint)).Returns(Task.FromResult(TestUtils.GetPlanetsString()));

        var expected = TestUtils.DeserializeObjects<Planets>(TestUtils.GetPlanetsString());
        var actual = await controller.GetAllAsync();

        Assert.AreEqual(expected.Count(), actual.Count());

        for (int i = 0; i < expected.Count(); i++)
        {
            Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
            Assert.AreEqual(expected.ElementAt(i).RotationPeriod, actual.ElementAt(i).RotationPeriod);
            Assert.AreEqual(expected.ElementAt(i).OrbitalPeriod, actual.ElementAt(i).OrbitalPeriod);
            Assert.AreEqual(expected.ElementAt(i).Diameter, actual.ElementAt(i).Diameter);
            Assert.AreEqual(expected.ElementAt(i).Climate, actual.ElementAt(i).Climate);
            Assert.AreEqual(expected.ElementAt(i).Gravity, actual.ElementAt(i).Gravity);
            Assert.AreEqual(expected.ElementAt(i).Terrain, actual.ElementAt(i).Terrain);
            Assert.AreEqual(expected.ElementAt(i).SurfaceWater, actual.ElementAt(i).SurfaceWater);
            Assert.AreEqual(expected.ElementAt(i).Population, actual.ElementAt(i).Population);
            Assert.AreEqual(expected.ElementAt(i).Residents, actual.ElementAt(i).Residents);
            Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
            Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
        }
    }

    [Test]
    public async Task GetByValueAsyncShouldReturnEmpty()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PlanetsEndpoint)).Returns(Task.FromResult(string.Empty));

        var expected = new List<Planets>();
        var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

        Assert.AreEqual(expected, actual);
    }

    [TestCase("name", "Tatooine")]
    [TestCase("rotation_period", "23")]
    [TestCase("orbital_period", "304")]
    [TestCase("diameter", "10465")]
    [TestCase("climate", "arid")]
    [TestCase("gravity", "1 standard")]
    [TestCase("terrain", "desert")]
    [TestCase("surface_water", "1")]
    [TestCase("population", @"200000")]
    [Test]
    public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
    {
        var objectFilter = new ObjectFilter<Planets>();
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PlanetsEndpoint)).Returns(Task.FromResult(TestUtils.GetPlanetsString()));

        objectFilter.Objects = TestUtils.DeserializeObjects<Planets>(TestUtils.GetPlanetsString());

        var expected = objectFilter.GetObjectsByProperty(propertyName, value);
        var actual = await controller.GetByValueAsync(propertyName, value);

        Assert.AreEqual(expected.Count(), actual.Count());
        for (int i = 0; i < expected.Count(); i++)
        {
            Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
            Assert.AreEqual(expected.ElementAt(i).RotationPeriod, actual.ElementAt(i).RotationPeriod);
            Assert.AreEqual(expected.ElementAt(i).OrbitalPeriod, actual.ElementAt(i).OrbitalPeriod);
            Assert.AreEqual(expected.ElementAt(i).Diameter, actual.ElementAt(i).Diameter);
            Assert.AreEqual(expected.ElementAt(i).Climate, actual.ElementAt(i).Climate);
            Assert.AreEqual(expected.ElementAt(i).Gravity, actual.ElementAt(i).Gravity);
            Assert.AreEqual(expected.ElementAt(i).Terrain, actual.ElementAt(i).Terrain);
            Assert.AreEqual(expected.ElementAt(i).SurfaceWater, actual.ElementAt(i).SurfaceWater);
            Assert.AreEqual(expected.ElementAt(i).Population, actual.ElementAt(i).Population);
            Assert.AreEqual(expected.ElementAt(i).Residents, actual.ElementAt(i).Residents);
            Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
            Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
        }
    }
}
