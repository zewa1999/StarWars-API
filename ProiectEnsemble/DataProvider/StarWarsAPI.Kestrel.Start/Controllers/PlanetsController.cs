using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetsController : GenericController<Planets, PlanetsController>
{
    public PlanetsController(ILogger<PlanetsController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }
}