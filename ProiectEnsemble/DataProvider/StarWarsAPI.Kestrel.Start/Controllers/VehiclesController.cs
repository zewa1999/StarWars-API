using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class VehiclesController : GenericController<Vehicles, VehiclesController>
{
    public VehiclesController(ILogger<VehiclesController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }
}