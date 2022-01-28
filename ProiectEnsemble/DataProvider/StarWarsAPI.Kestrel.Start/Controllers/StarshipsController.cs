using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StarshipsController : GenericController<Starships, StarshipsController>
{
    public StarshipsController(ILogger<StarshipsController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }
}