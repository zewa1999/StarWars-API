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

public class PeopleControllerTests
{
    private PeopleController controller;
    private IStrategyGetCaller strategyGetCaller;

    private Mock<ILogger<PeopleController>> loggerMock;
    private Mock<IDataProvider> dataProviderMock;
    private Mock<IIdentityMap> mapMock;

    [SetUp]
    public void Initialize()
    {
        loggerMock = new();
        dataProviderMock = new();
        mapMock = new();

        strategyGetCaller = new StrategyGetCaller(dataProviderMock.Object, mapMock.Object);
        controller = new PeopleController(loggerMock.Object, strategyGetCaller);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnEmptyListAsync()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PeopleEndpoint)).Returns(Task.FromResult(string.Empty));

        var expected = new List<People>();
        var actual = await controller.GetAllAsync();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnValidObjectsAsync()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PeopleEndpoint)).Returns(Task.FromResult(TestUtils.GetPeopleString()));

        var expected = TestUtils.DeserializeObjects<People>(TestUtils.GetPeopleString());
        var actual = await controller.GetAllAsync();

        Assert.AreEqual(expected.Count(), actual.Count());

        for (int i = 0; i < expected.Count(); i++)
        {
            Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
            Assert.AreEqual(expected.ElementAt(i).Height, actual.ElementAt(i).Height);
            Assert.AreEqual(expected.ElementAt(i).Mass, actual.ElementAt(i).Mass);
            Assert.AreEqual(expected.ElementAt(i).HairColor, actual.ElementAt(i).HairColor);
            Assert.AreEqual(expected.ElementAt(i).SkinColor, actual.ElementAt(i).SkinColor);
            Assert.AreEqual(expected.ElementAt(i).EyeColor, actual.ElementAt(i).EyeColor);
            Assert.AreEqual(expected.ElementAt(i).BirthYear, actual.ElementAt(i).BirthYear);
            Assert.AreEqual(expected.ElementAt(i).Gender, actual.ElementAt(i).Gender);
            Assert.AreEqual(expected.ElementAt(i).Homeworld, actual.ElementAt(i).Homeworld);
            Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
            Assert.AreEqual(expected.ElementAt(i).Species, actual.ElementAt(i).Species);
            Assert.AreEqual(expected.ElementAt(i).Vehicles, actual.ElementAt(i).Vehicles);
            Assert.AreEqual(expected.ElementAt(i).Starships, actual.ElementAt(i).Starships);
            Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
        }
    }

    [Test]
    public async Task GetByValueAsyncShouldReturnEmpty()
    {
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PeopleEndpoint)).Returns(Task.FromResult(string.Empty));

        var expected = new List<People>();
        var actual = await controller.GetByValueAsync("invalidPropertyName", "invalidValue");

        Assert.AreEqual(expected, actual);
    }

    [TestCase("name", "Luke Skywalker")]
    [TestCase("height", "172")]
    [TestCase("mass", "77")]
    [TestCase("hair_color", "blond")]
    [TestCase("skin_color", "fair")]
    [TestCase("eye_color", "blue")]
    [TestCase("birth_year", "19BBY")]
    [TestCase("gender", "male")]
    [TestCase("url", @"https://swapi.dev/api/people/1/")]
    [Test]
    public async Task GetByValueAsyncShouldReturnValidObjects(string propertyName, string value)
    {
        var objectFilter = new ObjectFilter<People>();
        dataProviderMock.Setup(x => x.GetAllStringAsync(ApiRoutes.PeopleEndpoint)).Returns(Task.FromResult(TestUtils.GetPeopleString()));

        objectFilter.Objects = TestUtils.DeserializeObjects<People>(TestUtils.GetPeopleString());

        var expected = objectFilter.GetObjectsByProperty(propertyName, value);
        var actual = await controller.GetByValueAsync(propertyName, value);

        Assert.AreEqual(expected.Count(), actual.Count());
        for (int i = 0; i < expected.Count(); i++)
        {
            Assert.AreEqual(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
            Assert.AreEqual(expected.ElementAt(i).Height, actual.ElementAt(i).Height);
            Assert.AreEqual(expected.ElementAt(i).Mass, actual.ElementAt(i).Mass);
            Assert.AreEqual(expected.ElementAt(i).HairColor, actual.ElementAt(i).HairColor);
            Assert.AreEqual(expected.ElementAt(i).SkinColor, actual.ElementAt(i).SkinColor);
            Assert.AreEqual(expected.ElementAt(i).EyeColor, actual.ElementAt(i).EyeColor);
            Assert.AreEqual(expected.ElementAt(i).BirthYear, actual.ElementAt(i).BirthYear);
            Assert.AreEqual(expected.ElementAt(i).Gender, actual.ElementAt(i).Gender);
            Assert.AreEqual(expected.ElementAt(i).Homeworld, actual.ElementAt(i).Homeworld);
            Assert.AreEqual(expected.ElementAt(i).Films, actual.ElementAt(i).Films);
            Assert.AreEqual(expected.ElementAt(i).Species, actual.ElementAt(i).Species);
            Assert.AreEqual(expected.ElementAt(i).Vehicles, actual.ElementAt(i).Vehicles);
            Assert.AreEqual(expected.ElementAt(i).Starships, actual.ElementAt(i).Starships);
            Assert.AreEqual(expected.ElementAt(i).Url, actual.ElementAt(i).Url);
        }
    }
}
