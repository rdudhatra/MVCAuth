# MVC Auth Project

This guide will help you set up and run the MVC Auth project successfully.

## Prerequisites

- SQL Server installed and running
- Visual Studio with Package Manager Console

## Steps to Run the Project

1. **Get the Data Source Server Name of SQL Server**

    a. Open SQL Server Management Studio (SSMS).

    b. Log in with SQL authentication.

    c. Open a new query window and execute the following command to get the server name:
    ```sql
    SELECT @@servername
    ```

    d. Copy the server name from the result.

2. **Configure the Project**

    a. Open the `appsettings.json` file in the MVCAuth project.

    b. Replace the `DataSource` server name with the copied server name.

3. **Update the Database**

    a. Open Visual Studio.

    b. Open the Package Manager Console from `Tools > NuGet Package Manager > Package Manager Console`.

    c. In the Package Manager Console, select `MVCAuth` as the default project.

    d. Run the following command to create the new database:
    ```powershell
    Update-Database
    ```

4. **Run the Project**

    a. After the database is created, run the project from Visual Studio.

    b. The basic setup is now complete. You should see the Blazor Auth project running.

## Note

Different social media authentications are available in this project, including Google, Facebook, Twitter, and Github. However, all client IDs and secrets have been removed from this demo.


https://github.com/rdudhatra/MVCAuth/assets/172353572/998e1ce1-837a-41ef-90a4-dd3da6e046ba


