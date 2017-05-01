# LK2 URL Shortner Service

This repository serves as a simple, working example for hosting ASP.NET Core applications on Ubuntu Linux.

This code is recommended for demonstration purposes only.

You can see and follow the blog post here: http://blog.bobbyallen.me/2017/05/01/deploying-and-hosting-asp-net-core-applications-on-ubuntu-linux/

## Supported Database Engines

This demo app supports both __SQLite__ and __MSSQL__. These can be set and configured in the ``appsettings.json`` file. See the below examples of connection strings for both platforms.

SQLite is enabled by default and the Startup.cs file (for simplicity) will automatically run your migrations at start-up:-

__SQLite__

```json
{
  "DbDriver":  "sqlite",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=database.sqlite;"
  }
}
```

Users can also switch to use MSSQL if they wish...

__MSSQL__
```json
{
  "DbDriver":  "mssql",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=vsvr-sql027;Initial Catalog=lk2;Integrated Security=False;User Id=sa;Password=<YourSaPassword>;"
  }
}
```
