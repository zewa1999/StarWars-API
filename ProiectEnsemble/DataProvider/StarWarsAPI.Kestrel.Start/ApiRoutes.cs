namespace StarWarsAPI.Server
{
    public static class ApiRoutes
    {
        public const string PeopleEndpoint = @"people/";
        public const string FilmsEndpoint = @"films/";
        public const string PlanetsEndpoint = @"planets/";
        public const string SpeciesEndpoint = @"species/";
        public const string StarshipsEndpoint = @"starships/";
        public const string VehiclesEndpoint = @"vehicles/";

        // Generic Controller
        public const string GetByValueEndpoint = "propertyName={propertyName}&value={value}";

        public const string StarWarsApiBase = @"https://swapi.dev/api/";
    }
}