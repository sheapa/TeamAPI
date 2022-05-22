# ********** TeamAPI **********

## About
An API that serves data about teams and thier players. 


## Tools + Languages
* C# 10
*.Net 6
* Visual Studio
* Entity Framework

## Assignment

API Coding Challenge

For the most part, this process will follow the pattern described here: 
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio
We will use .NET 6 along with either Visual Studio 2022 or Visual Studio Code to create a small web app.

Assessment:
Set up a web API project to interact with a database containing Teams and Players.
Ignore the parts about calling the API with jQuery – we will be testing your application just by hitting certain endpoints with PostMan. This is not a UI test.

Guidelines:
Create an entity framework DbContext that models Team and Player objects.
A Team should contain an ID, a Name, a Location, and a List of Players on the team.
-	No two Teams should exist with the same Name and Location
-	A Team should have a maximum of 8 Players
-	Name and Location are required fields
A Player should contain an ID, a FirstName, and a LastName
-	A Player can only be on one Team
-	First Name, and Last Name are required fields

(The tutorial shows you how to set it up as an in-memory database so you don’t have to worry about using a real database.)

Create endpoints that allow us to:
-	Create a Team
-	Create a Player
-	Add or Remove a Player from a Team
-	Query for Players
    o	Query by ID
    o	Query All Players
    o	Query All Players matching a given Last Name
    o	Query for all Players on a Team
-	Query for Teams
    o	Query by ID
    o	Query All Teams
    o	Query All Teams ordered by Name or Location

For example, we should be able to:
-	Send a POST request containing a Player object and create a new Player
-	Send a GET request and see all teams ordered by their name (sorted alphabetically)


## Method
1. Create ASP.Net Core Web API project in Visual Studio.
2. Design Player & Team models.
3. Add Nuget Package "Microsoft.EntityFrameworkCore.InMemory", Create Database Context & Register Database Context.
4. Scaffold a controller using Entity Framework.
5. Create custom endpoints.
6. Test with Postman & Refactor.


## Functionality & Instructions For Testing End Points
1.) (Landing Page) Query All Teams : https://localhost:[Port]/api/Teams
    * Displays comprehensive list of teams.
    
2.) Query Teams Alphabetically : https://localhost:[Port]/api/Teams/sort
    * Displays comprehensive list of teams ordered alphabetically.
    
3.) Query Teams by Location : https://localhost:[Port]/api/Teams/location
    * Displays comprehensive list of teams ordred by location.

4.) Query Team by TeamID : https://localhost:[Port]/api/Teams/{TeamID}
    * Displays team selected by team ID.
    
5.) Query All Players : https://localhost:[Port]/api/players
    * Displays comprehensive list of players.
    
6.) Query Player by ID : https://localhost:[Port]/api/players/{PlayerID}
    * Displays player selected by player ID.
    
7.) Query Player by LastName : https://localhost:[Port]/api/players/?lastname={lastName}
    * Displays player selected by player last name.
    
8.) Create a team :  https://localhost:[Port]/api/teams
    * Add a team to in memory database. Send JSON team object to above address through postman post.
    
9.) Create a player :  https://localhost:[Port]/api/players
    * Add a player to in memory database. Send JSON player object to above address through postman post

10.) Add a player to a team : https://localhost:7284/api/teams/{teamId)
    * Add a player to a team. Send existing player ID throught postman patch to above address.
    
11.)Remove a player :  https://localhost:[Port]/api/players{playerId}
    * Deletes player from team. Send existing player ID through postman delete to above address.
    
## Seed Data
Build an entire team with one post by posting below JSON through postman using above #8 "Create a Team"

### Team 1
{
    "id": 1,
    "name": "Panthers",
    "location": "Charlotte",
    "players" : [
        {
            "id": 1,
            "firstName": "Philip",
            "lastName": "Walker"
        },
        {
            "id": 2,
            "firstName": "Dennis",
            "lastName": "Daley"
        },
        {
            "id": 3,
            "firstName": "Darius",
            "lastName": "Bradwell"
        },
        {
            "id": 4,
            "firstName": "Aaron",
            "lastName": "Parker"
        },
        {
            "id": 5,
            "firstName": "Spencer",
            "lastName": "Brown"
        },
        {
            "id": 6,
            "firstName": "Sam",
            "lastName": "Darnold"
        },
        {
            "id": 7,
            "firstName": "John",
            "lastName": "Miller"
        },
        {
            "id": 8,
            "firstName": "Colin",
            "lastName": "Thompson"
        }
    ]
}

### Team 2
{
    "id": 2,
    "name": "Browns",
    "location": "Cleveland",
    "players" : [
        {
            "id": 9,
            "firstName": "Amari",
            "lastName": "Cooper"
        },
        {
            "id": 10,
            "firstName": "Stephen",
            "lastName": "Carlson"
        },
        {
            "id": 11,
            "firstName": "Joel",
            "lastName": "Bitonio"
        },
        {
            "id": 12,
            "firstName": "Hijalte",
            "lastName": "Froholdt"
        },
        {
            "id": 13,
            "firstName": "Demetric",
            "lastName": "Felton"
        },
        {
            "id": 14,
            "firstName": "Jack",
            "lastName": "Conklin"
        },
        {
            "id": 15,
            "firstName": "Kareem",
            "lastName": "Hunt"
        },
        {
            "id": 16,
            "firstName": "Joshua",
            "lastName": "Dobbs"
        }
    ]
}

### Team 3

{
    "id": 3,
    "name": "Angles",
    "location": "Anahiem",
    "players" : [
        {
            "id": 17,
            "firstName": "Austin",
            "lastName": "Romine"
        },
        {
            "id": 18,
            "firstName": "Max",
            "lastName": "Stassi"
        },
        {
            "id": 19,
            "firstName": "Kurt",
            "lastName": "Suzuki"
        },
        {
            "id": 20,
            "firstName": "Matt",
            "lastName": "Thaiss"
        },
        {
            "id": 21,
            "firstName": "Chad",
            "lastName": "Wallach"
        },
        {
            "id": 22,
            "firstName": "David",
            "lastName": "Fletcher"
        },
        {
            "id": 23,
            "firstName": "Jack",
            "lastName": "Mayfield"
        },
        {
            "id": 24,
            "firstName": "Anthony",
            "lastName": "Rendon"
        }
    ]
}


## Resources
* <a href="https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio" target="_blank">Tutorial: Create a web API with ASP.NET Core</a>
* <a href="https://www.youtube.com/watch?v=Fbf_ua2t6v4&t=1810s" target="_blank">CRUD with a .NET 6 Web API & Entity Framework Core 🚀 Full Coursee</a>
* And StackOverflow of course!



