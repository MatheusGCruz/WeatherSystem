# WeatherSystem
Weather System - .Net - Vue 

The system has 3 parts:

HangFire Worker:
	Designed in C# with Hangfire
	Used for getting data from the public API https://wttr.in/
	Scheduled for running hourly
	
WebServer
	Designed in C#
	Used for exposing the API via Cloudflare tunnel
	API links:
		https://api.antares.ninja/weather/(city)
		https://api.antares.ninja/forecast/(city)
		https://api.antares.ninja/analytics
	This API is exposed via CloudFlare, which can be accessed
	The (city) can be changed for different cities
	
Website
	Designed in VUE
	Website used to show data
	Hosted on CloudFlare Pages
	Website link:
		https://weather.antares.ninja/(city)
	The (city) is optional and can be changed for different cities
	

Design Proccess

	This project was designed with a Weather App in mind
	The public API chosen was not available, but used, on the code, the https://wttr.in/ API.
	The project was designed in three parts, where 2 are public acessible, and 1 running on premises (Hangfire).
	
How the system works
	As soon as a new city is queried in the API endpoint (weather), a is executed on the database. If the register exists, the returns with the data.
	If a query not find any register, an API call to wttr endpoint is executed, and the register is saved on the database.
	The Hangfire worker runs every hour, and looks for the cities saved on the system. This implies that, if a city was previous queried (both from the worker and the API calls), this city is queried.
	If the query return a new register (different observation hours), the resgister is saved.
	If the query return a register that its the same as a previous one, only the lastUpdatedDate is changed, to reflect this new update date.
	