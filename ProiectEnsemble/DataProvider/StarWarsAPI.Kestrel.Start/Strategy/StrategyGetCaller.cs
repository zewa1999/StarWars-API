using Newtonsoft.Json.Linq;
using StarWarsAPI.DataProvider;
using StarWarsAPI.Server.Models;
using System.Text.Json;

namespace StarWarsAPI.Server.Strategy;

// I know that this is not a proper strategy pattern,
// BUT: If I would have made a Strategy for every algorithm it will make duplicate code, and I don't want that
// since the method that I am using is generic...

public class StrategyGetCaller : IStrategyGetCaller
{
    private readonly IDataProvider _dataProvider;
    private readonly IIdentityMap _map;

    public StrategyGetCaller(IDataProvider dataProvider, IIdentityMap map)
    {
        _dataProvider = dataProvider;
        _map = map;
    }

    public async Task<IEnumerable<T>> GetEntities<T>() where T : BaseModel
    {
        if (typeof(T) == typeof(Films))
        {
            if (_map.HasFilms == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.FilmsEndpoint, _map, _dataProvider, _map.HasFilms);
                _map.HasFilms = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        if (typeof(T) == typeof(People))
        {
            if (_map.HasPeople == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.PeopleEndpoint, _map, _dataProvider, _map.HasPeople);
                _map.HasPeople = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        if (typeof(T) == typeof(Planets))
        {
            if (_map.HasPlanets == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.PlanetsEndpoint, _map, _dataProvider, _map.HasPlanets);
                _map.HasPlanets = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        if (typeof(T) == typeof(Species))
        {
            if (_map.HasSpecies == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.SpeciesEndpoint, _map, _dataProvider, _map.HasSpecies);
                _map.HasSpecies = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        if (typeof(T) == typeof(Starships))
        {
            if (_map.HasStarships == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.StarshipsEndpoint, _map, _dataProvider, _map.HasStarships);
                _map.HasStarships = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        if (typeof(T) == typeof(Vehicles))
        {
            if (_map.HasVehicles == false)
            {
                var result =  await GetEntitiesAsync<T>(ApiRoutes.VehiclesEndpoint, _map, _dataProvider, _map.HasVehicles);
                _map.HasVehicles = true;
                return result;
            }
            else
            {
                return _map.GetItems<T>();
            }
        }

        return new List<T>();
    }

    private async Task<List<T>> GetEntitiesAsync<T>(string endpoint, IIdentityMap map, IDataProvider dataProvider, bool hasElements)
    {
        var entities = new List<T>();

        if (hasElements == false)
        {
            var jsonString = await dataProvider.GetAllStringAsync(endpoint);
            entities = DeserializeObjects<T>(jsonString);

            entities.ForEach(f => map.AddItem(f));
            return entities;
        }
        else
        {
            return map.GetItems<T>().ToList().ConvertAll(x => x);
        }
    }

    private List<T> DeserializeObjects<T>(string json)
    {
        var objectsList = new List<T>();

        var myObject = JValue.Parse(json);
        foreach (var item in myObject["results"])
        {
            var itemJson = item.ToString();
            var entity = JsonSerializer.Deserialize<T>(itemJson);
            objectsList.Add(entity);
        }

        return objectsList;
    }

}