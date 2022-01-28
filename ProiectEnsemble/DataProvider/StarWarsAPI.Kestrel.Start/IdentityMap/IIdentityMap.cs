namespace StarWarsAPI.Server;

public interface IIdentityMap
{
    public bool HasFilms { get; set; }
    public bool HasPeople { get; set; }
    public bool HasPlanets { get; set; }
    public bool HasSpecies { get; set; }
    public bool HasStarships { get; set; }
    public bool HasVehicles { get; set; }

    public bool AddItem(object value);

    public bool ContainsKey(string key);

    public object? GetItem(string key);

    public IEnumerable<T> GetItems<T>();
}
