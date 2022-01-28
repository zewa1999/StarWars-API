using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

public abstract class GenericController<Model, AppController> : Controller
    where Model : BaseModel
    where AppController : Controller
{
    protected readonly ObjectFilter<Model> objectFilter;
    protected readonly IStrategyGetCaller strategyGetCaller;
    protected readonly ILogger<AppController> logger;

    public GenericController(ILogger<AppController> logger, IStrategyGetCaller strategy)
    {
        this.logger = logger;
        strategyGetCaller = strategy;
        objectFilter = new ObjectFilter<Model>();
    }

    [HttpGet]
    public async Task<IEnumerable<Model>> GetAllAsync()
    {
        var entities = new List<Model>();

        try
        {
            var entitiesBeforeCast = await strategyGetCaller.GetEntities<Model>();

            entities = entitiesBeforeCast.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return entities;
    }

    [HttpGet(ApiRoutes.GetByValueEndpoint)]
    public async Task<IEnumerable<Model>> GetByValueAsync(string propertyName, string value)
    {

        if(propertyName == "id")
        {
            var requestPath = Request.Path.Value;
            var reqPathTokens = requestPath.Split('/');
            value = ApiRoutes.StarWarsApiBase + reqPathTokens[1] + "/" + value + "/".Trim();
            propertyName = "url";
        }
        var entities = new List<Model>();
        try
        {
            var uncastedEntities = await strategyGetCaller.GetEntities<Model>();
            entities = uncastedEntities.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        var searchedItems = new List<Model>();
        objectFilter.Objects = entities;

        try
        {
            searchedItems = objectFilter.GetObjectsByProperty(propertyName, value).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to filter Objects", ex));
        }

        return searchedItems;
    }

}