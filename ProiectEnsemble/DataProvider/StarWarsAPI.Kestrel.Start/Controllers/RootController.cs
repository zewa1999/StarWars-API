using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.DataProvider;
using StarWarsAPI.Server.Models;
using System.Text.Json;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RootController : Controller
{
    private readonly ILogger<RootController> _logger;
    private readonly IDataProvider _dataProvider;

    public RootController(ILogger<RootController> logger, IDataProvider dataprovider)
    {
        _logger = logger;
        _dataProvider = dataprovider;
    }

    [HttpGet]
    public async Task<IEnumerable<Root>> GetAllAsync()
    {
        try
        {
            var json = await _dataProvider.GetAsync(@"https://swapi.dev/api/");
            _logger.LogInformation("{Message}", Utils.GetSuccessfulCallMessage("GET"));
            return new Root[] { JsonSerializer.Deserialize<Root>(json) };
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("GET", ex));
        }

        return new List<Root>();
    }
}