using StarWarsAPI.Server.Models;
using System.Reflection;

namespace StarWarsAPI.Server;

public static class Utils
{
    #region LogMessages

    public static string GetSuccessfulCallMessage(string endpoint)
    {
        return $"|{DateTime.Now} Call to endpoint: {endpoint} -> successful!";
    }

    public static string GetUnsuccessfulCallMessage(string endpoint, Exception exception = null)
    {
        if (exception == null)
        {
            return $"|{DateTime.Now} Call to endpoint: {endpoint} -> unsuccessful!";
        }
        else if (exception.InnerException == null)
            return $"|{DateTime.Now} Call to endpoint: {endpoint} -> unsuccessful! \nException Message: {exception.Message}\n";

        return $"|{DateTime.Now} Call to endpoint: {endpoint} -> unsuccessful! \nException Message: {exception.Message}\nInner Exception: {exception.InnerException}";

    }

    #endregion LogMessages

    public static object GetPropValue<T>(this object obj, string propertyName)
    {
        if (propertyName.Length == 0)
            return "String is empty";

        if (propertyName.Contains('_'))
        {
            propertyName = propertyName.Replace('_', ' ');
            propertyName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(propertyName);
            propertyName = propertyName.Replace(" ", string.Empty);
        }

        propertyName = char.ToUpper(propertyName[0]) + propertyName[1..];

        foreach (String part in propertyName.Split('.'))
        {
            if (obj == null) { return null; }

            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(part);
            if (info == null) { return null; }

            obj = info.GetValue(obj, null);
        }
        return obj;
    }
    public static Films GetFilmById(List<Films> films, int id)
    {
        return films.Where(x => x.EpisodeId == id).FirstOrDefault();
    }
}