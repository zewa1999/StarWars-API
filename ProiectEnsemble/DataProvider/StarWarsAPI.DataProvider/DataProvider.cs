using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace StarWarsAPI.DataProvider;

public class DataProvider : IDataProvider
{
    private readonly ILogger _logger;
    private readonly HttpClient client;

    public DataProvider(ILogger logger)
    {
        client = new ();
        client.BaseAddress = new Uri(@"https://swapi.dev/api/");
        _logger = logger;
    }

    public async Task<string> GetAsync(string endpointURL)
    {
        _logger.LogInformation("Starting the process to use the GET method with the {EndpointURL} endpoint", endpointURL);
        var responseBody = string.Empty;

        try
        {
            responseBody = await client.GetStringAsync(endpointURL);
            _logger.LogInformation("responseBody = {ResponseBody}", responseBody);
        }
        catch (Exception ex)
        {
            _logger.LogError("Method GetAsync has LogErrors: {Ex}", ex.Message);
        }

        if (!responseBody.Equals(string.Empty))
        {
            _logger.LogInformation($"Response retrieved successfully");
            return responseBody;
        }
        else
        {
            _logger.LogError("Unable to get a response. Response string is empty!");
            return string.Empty;
        }
    }

    public async Task<string> GetAllStringAsync(string endpointURL)
    {
        _logger.LogInformation("Starting the process to use the GET method with the {EndpointURL} endpoint", endpointURL);
        var responseBody = string.Empty;
        var responseObject = new JObject();

        try
        {
            responseBody = await client.GetStringAsync(endpointURL);
            responseObject = JObject.Parse(responseBody);
            JToken token = responseObject["next"];

            while (token.Type != JTokenType.Null)
            {
                responseBody = await client.GetStringAsync(responseObject.Property("next").Value.ToString());
                var dataObject2 = JObject.Parse(responseBody);

                responseObject.Merge(dataObject2, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                token = dataObject2["next"];
            }

            responseObject.Remove("count");
            responseObject.Remove("next");
            responseObject.Remove("previous");
        }
        catch (Exception ex)
        {
            _logger.LogError("Method GetAllStringAsync has LogErrors: {ex}", ex.Message);
        }

        if (!responseBody.Equals(string.Empty))
        {
            _logger.LogInformation($"Response retrieved successfully");
            return responseObject.ToString();
        }
        else
        {
            _logger.LogError("Unable to get a response. Response string is empty!");
            return string.Empty;
        }
    }
}