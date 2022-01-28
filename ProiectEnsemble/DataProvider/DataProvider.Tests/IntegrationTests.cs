using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace StarWarsAPI.DataProvider.Tests;

public class IntegrationTests
{
    private IDataProvider provider;

    [SetUp]
    public void Setup()
    {
        var mock = new Mock<ILogger<DataProvider>>();
        ILogger<DataProvider> logger = mock.Object;
        provider = new DataProvider(logger);
    }

    [Test]
    [TestCase("")]
    [TestCase(@"planets/1/")]
    [TestCase(@"people/1/")]
    [TestCase(@"films/1/")]
    [TestCase(@"starships/9/")]
    [TestCase(@"vehicles/4/")]
    [TestCase(@"species/3/")]
    [TestCase(@"planets/1/")]
    public async Task TestGetAsyncForEndpoints(string endpoint)
    {
        Assert.IsNotNull(await provider.GetAsync(endpoint));
        Assert.IsNotEmpty(await provider.GetAsync(endpoint));
    }
}