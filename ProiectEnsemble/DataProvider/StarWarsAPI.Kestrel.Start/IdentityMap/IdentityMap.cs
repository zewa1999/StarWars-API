using StarWarsAPI.Server.Models;
using System.Collections.Concurrent;

namespace StarWarsAPI.Server;

public class IdentityMap : IIdentityMap
{
    protected ConcurrentDictionary<string, object> ObjectPool { get; set; } = new();
    public bool HasFilms { get; set; }
    public bool HasPeople { get; set; }
    public bool HasPlanets { get; set; }
    public bool HasSpecies { get; set; }
    public bool HasStarships { get; set; }
    public bool HasVehicles { get; set; }

    public bool AddItem(object value)
    {
        var classObject = (BaseModel)value;

        if (!ObjectPool.ContainsKey(classObject.Url) &&
            (classObject.Url != null || classObject.Url != string.Empty))
        {
            ObjectPool.TryAdd(classObject.Url, value);
            return true;
        }

        return false;
    }

    public bool ContainsKey(string key)
    {
        if (ObjectPool.ContainsKey(key))
        {
            return true;
        }

        return false;
    }

    public object? GetItem(string key)
    {
        if (ObjectPool.ContainsKey(key))
        {
            return ObjectPool[key];
        }

        return null;
    }

    public IEnumerable<T> GetItems<T>()
    {
        var listOfObjects = new List<T>();

        foreach (var item in ObjectPool.Values)
        {
            if (item is T t)
            {
                listOfObjects.Add(t);
            }
        }

        return listOfObjects;
    }
}
