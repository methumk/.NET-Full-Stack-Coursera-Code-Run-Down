# Setup
Appsettings.json contains connection string to MSSQL server with the database you want to save the data to. The server is set to the local MSSQL server.
This can be changed or left as is which is currently saved to **WorkoutTrackerDB**. Make sure to have MSSQL server setup.

# Run the application
You can run the application by starting it on VS or through a terminal with dotnet and then running **dotnet run**

# MSSQL Connection
Once the application starts you will need to register an account to save data. This will fail the first time because migrations have not been run.
Register account, then you should be prompted to run migrations. Once migrations have finished, you should now be able to save data once logged in.