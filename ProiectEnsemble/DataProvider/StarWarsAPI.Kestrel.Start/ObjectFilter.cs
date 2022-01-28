using StarWarsAPI.Server.Models;

namespace StarWarsAPI.Server;

public class ObjectFilter<T>
    where T : BaseModel
{
    public List<T> Objects { get; set; }

    public IEnumerable<T> GetObjectsByProperty(string propertyName, string value)
    {
        var searchedItems = new List<T>();

        object property = null;

        foreach (var item in Objects)
        {
            property = Utils.GetPropValue<T>(item, propertyName);
            if(property == null)
            {
                throw new Exception("Property not found");
            }

            var castedProperty = property.ToString();
            if (castedProperty != null && castedProperty.Equals(value))
            {
                searchedItems.Add(item);
            }
           
        }

        return searchedItems;
    }
}