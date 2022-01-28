using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SpeciesController : GenericController<Species, SpeciesController>
{
    public SpeciesController(ILogger<SpeciesController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }
}