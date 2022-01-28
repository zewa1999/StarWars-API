using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Server.Models;
using StarWarsAPI.Server.Strategy;

namespace StarWarsAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmsController : GenericController<Films, FilmsController>
{
    public FilmsController(ILogger<FilmsController> logger, IStrategyGetCaller strategy) : base(logger, strategy)
    {
    }

    [HttpGet("episode_id={id}/characters/")]
    public async Task<List<People>> GetEpisodeCharactersAsync(int id)
    {
        var listOfPeople = new List<People>();
        try
        {
            var films = await strategyGetCaller.GetEntities<Films>();
            var people = await strategyGetCaller.GetEntities<People>();

            var film = Utils.GetFilmById(films.ToList(), id);

            foreach (var characterUrl in film.Characters)
            {
                listOfPeople.Add(people.Where(x=> x.Url == characterUrl).FirstOrDefault());
            }

            logger.LogInformation(Utils.GetSuccessfulCallMessage("Succesfully GET the characters!"));

        }
        catch (Exception ex)
        {
            logger.LogError(Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return listOfPeople;
    }

    [HttpGet("episode_id={id}/planets/")]
    public async Task<List<Planets>> GetEpisodePlanetsAsync(int id)
    {
        var listOfPlanets = new List<Planets>();
        try
        {
            var films = await strategyGetCaller.GetEntities<Films>();
            var planets = await strategyGetCaller.GetEntities<Planets>();

            var film = Utils.GetFilmById(films.ToList(), id);

            foreach (var planetsUrl in film.Planets)
            {
                listOfPlanets.Add(planets.Where(x => x.Url == planetsUrl).FirstOrDefault());
            }

            logger.LogInformation("{Message}", Utils.GetSuccessfulCallMessage("Succesfully GET the planets!"));
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return listOfPlanets;
    }

    [HttpGet("episode_id={id}/starships/")]
    public async Task<List<Starships>> GetEpisodeStarshipsAsync(int id)
    {
        var listOfStarships = new List<Starships>();
        try
        {
            var films = await strategyGetCaller.GetEntities<Films>();
            var starships = await strategyGetCaller.GetEntities<Starships>();

            var film = Utils.GetFilmById(films.ToList(), id);

            foreach (var starshipsUrl in film.Starships)
{
                listOfStarships.Add(starships.Where(x => x.Url == starshipsUrl).FirstOrDefault());
            }
            logger.LogInformation("{Message}", Utils.GetSuccessfulCallMessage("Succesfully GET the starships!"));
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return listOfStarships;
    }

    [HttpGet("episode_id={id}/vehicles/")]
    public async Task<List<Vehicles>> GetEpisodeVehiclesAsync(int id)
    {
        var listOfVehicles = new List<Vehicles>();
        try
        {
            var films = await strategyGetCaller.GetEntities<Films>();
            var vehicles = await strategyGetCaller.GetEntities<Vehicles>();

            var film = Utils.GetFilmById(films.ToList(), id);

            foreach (var vehiclesUrl in film.Vehicles)
            {
                listOfVehicles.Add(vehicles.Where(x => x.Url == vehiclesUrl).FirstOrDefault());
            }
            logger.LogInformation("{Message}", Utils.GetSuccessfulCallMessage("Succesfully GET the vehicles!"));
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return listOfVehicles;
    }

    [HttpGet("episode_id={id}/species/")]
    public async Task<List<Species>> GetEpisodeSpeciesAsync(int id)
    {
        var listOfSpecies = new List<Species>();
        try
        {
            var films = await strategyGetCaller.GetEntities<Films>();
            var species = await strategyGetCaller.GetEntities<Species>();

            var film = Utils.GetFilmById(films.ToList(), id);

            foreach (var speciesUrl in film.Species)
            {
                listOfSpecies.Add(species.Where(x => x.Url == speciesUrl).FirstOrDefault());
            }
            logger.LogInformation("{Message}", Utils.GetSuccessfulCallMessage("Succesfully GET the species!"));
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", Utils.GetUnsuccessfulCallMessage("Error when trying to get the Objects", ex));
        }

        return listOfSpecies;
    }
}