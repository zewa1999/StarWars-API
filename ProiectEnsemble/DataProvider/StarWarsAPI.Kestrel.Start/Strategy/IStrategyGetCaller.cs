using StarWarsAPI.Server.Models;

namespace StarWarsAPI.Server.Strategy;

public interface IStrategyGetCaller
{
    Task<IEnumerable<T>> GetEntities<T>() where T : BaseModel;
}
