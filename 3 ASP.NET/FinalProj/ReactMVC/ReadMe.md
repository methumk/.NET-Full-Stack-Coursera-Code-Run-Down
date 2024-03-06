# Summary
- This is a full stack workout tracker with a React front end and ASP.NET core web api as a back end
- This uses JWT to store logged in user data info
- This uses MSSQL server to store databases to the local mSSQL server


# Running
## Configure DB
- You can specify server and database you want to connect to in `ReactMVC.server/appsettings.json`
- Make sure `mssql.sql` database to be created matches up with the one specified in appsettings.json
- Run the sql file in MSSQL to create your database and create the tables
- Once sucessfully run, the database should be ready

## Run App
- CD to ReactMVC.Server
- In your console run `dotnet run`

## Access
- Client should be on `https://localhost:5173/`
- Back-end should be on `http://localhost:5286/`
- Back-end swagger endpoint should be on `http://localhost:5286/swagger/index.html`


# Troubleshooting
If the app doesn't work it might be because the EF dependencies aren't included in the solution.
Go to VS nuget package manager and install the following:
- EntityFrameworkCore.Relational
- EntityFrameworkCore.SQLServer
- EntityFrameworkCore.Design
- EntityFrameworkCore.Tools
- EntityFrameworkCore
- ASPNetCore.Authentication.JWTBearer
- ASPNetCore.SpaProxy
- IdentityModel.Tokens.Jwt
- IdentityModel.Tokens
