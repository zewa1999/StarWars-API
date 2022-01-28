using NUnit.Framework;
using StarWarsAPI.Server.Models;
using System.Linq;

namespace StarWarsAPI.Server.Tests;

public class IdentityMapTests
{
    private IIdentityMap identityMap;

    [SetUp]
    public void Setup()
    {
        identityMap = new IdentityMap();
    }

    public void AddItemShouldReturnFalse()
    {
        var entity = TestUtils.GetObject();
        Assert.IsTrue(identityMap.AddItem(entity));
        Assert.IsFalse(identityMap.AddItem(entity));
    }

    [Test]
    public void AddItemShouldReturnTrue()
    {
        var entity = TestUtils.GetObject();
        Assert.IsTrue(identityMap.AddItem(entity));
    }

    [Test]
    public void ContainsKeyShouldReturnFalse()
    {
        Assert.IsFalse(identityMap.ContainsKey("somethingThatDoesntExist"));
    }

    [Test]
    public void ContainsKeyShouldReturnTrue()
    {
        var entity = TestUtils.GetObject();
        Assert.IsTrue(identityMap.AddItem(entity));

        Assert.IsTrue(identityMap.ContainsKey("Url1"));
    }

    [Test]
    public void GetItemShouldReturnNull()
    {
        Assert.IsNull(identityMap.GetItem("somethingThatDoesntExist"));
    }

    [Test]
    public void GetItemShouldReturnValidObject()
    {
        var entity = TestUtils.GetObject();
        Assert.IsTrue(identityMap.AddItem(entity));

        var item = identityMap.GetItem("Url1") as BaseModel;
        Assert.IsNotNull(item);
        Assert.AreEqual(item.Url, "Url1");
    }

    [Test]
    public void GetItemsShouldReturnFalse()
    {

        Assert.AreEqual(identityMap.GetItems<Films>().Count(), 0);
    }

    [Test]
    public void GetItemsShouldReturnTrue()
    {
        var entity = TestUtils.GetObject();

        Assert.IsTrue(identityMap.AddItem(TestUtils.GetObject()));

        var item = identityMap.GetItem("Url1") as BaseModel;

        Assert.IsNotNull(identityMap.GetItem("Url1"));

        Assert.AreEqual(item.Url, "Url1");

        Assert.AreEqual(identityMap.GetItems<Films>().Count(), 1);

    }
}
