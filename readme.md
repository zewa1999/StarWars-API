The app is made using .NET 6.<br />
Please use Visual Studio 2022, or Visual Studio 2019 with preview in all the .csproj for the TargetFramework property.<br />
Steps:<br />
Enter Visual Studio<br />
Open the app<br />
Run the application<br />
Get the localhost link where the server listens<br />
![Scheme](picture.jpg)<br />
Enter postman:<br />
Use the endpoints:<br />

Note: You need to change the localhost port to the one that you have in your console<br />

Generic endpoints<br />
https://localhost:7230/films/<br />
https://localhost:7230/people/<br />
https://localhost:7230/planets/<br />
https://localhost:7230/species/<br />
https://localhost:7230/starships/<br />
https://localhost:7230/vehicles/<br />
https://localhost:7230/root/<br />

Films endpoints<br />
https://localhost:7230/films/episode_id={id}/characters/<br />
https://localhost:7230/films/episode_id={id}/planets/<br />
https://localhost:7230/films/episode_id={id}/starships/<br />
https://localhost:7230/films/episode_id={id}/vehicles/<br />
https://localhost:7230/films/episode_id={id}/species/<br />

And for searching based on a property and a value:<br />
https://localhost:7230/films/propertyName={propertyName}&value={value}<br />
https://localhost:7230/people/propertyName={propertyName}&value={value}<br />
https://localhost:7230/planets/propertyName={propertyName}&value={value}<br />
https://localhost:7230/species/propertyName={propertyName}&value={value}<br />
https://localhost:7230/starships/propertyName={propertyName}&value={value}<br />
https://localhost:7230/vehicles/propertyName={propertyName}&value={value}<br />

Where the property is the property that you look for to search, and the value is the value for what you are searching.<br />
Note: Make sure that the property given is valid and not a list.<br />
If one of the values given are not valid, the endpoint will return an empty null and you can see what was the issue <br />
in the console of the server.<br />


For example:<br />
Search by blue eyes in people:<br />
https://localhost:7230/people/propertyName=eye_color&value=blue<br />
https://localhost:7230/starships/propertyName=id&value=66<br />


NOTE: It does not support dates, I have code for dates but I didn't wanted it to add it, since the dates are all in the same day, at the same hour and minute
	  It does not support the url property, please search by id

The app could have been improved using a database, to persist the data, but I didn't want to make the app complicated for such a little database.
The main issue that I find about this is that some endpoints may take more time, but that's were the IdentityMap comes in and rescue the situation
since if the entities looked for were already retrieved, they will get returned instantly.

P.S: I've made a commit with all the changes because I was in a hurry, sorry.