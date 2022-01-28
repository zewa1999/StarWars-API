using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : GenericController<People, PeopleController>
{
    public PeopleController(ILogger<PeopleController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }
}