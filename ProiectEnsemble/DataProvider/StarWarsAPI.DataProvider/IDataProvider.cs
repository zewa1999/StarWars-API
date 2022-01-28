namespace StarWarsAPI.DataProvider;

public interface IDataProvider
{
    /// <summary>
    /// Method to get the response based on an URL
    /// </summary>
    /// <param name="endpointURL">Endpoint url to which you want to make the call</param>
    /// <returns>string of the response</returns>
    public Task<string> GetAsync(string endpointURL);

    /// <summary>
    /// Method to get all the objects from all pages
    /// </summary>
    /// <param name="endpointURL">Endpoint url to which you want to make the call</param>
    /// <returns>string of the response</returns>
    public Task<string> GetAllStringAsync(string endpointURL);
}