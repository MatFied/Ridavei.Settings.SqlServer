# Ridavei.Settings.SqlServer

### Latest release
[![NuGet Badge Ridavei.Settings.SqlServer](https://buildstats.info/nuget/Ridavei.Settings.SqlServer)](https://www.nuget.org/packages/Ridavei.Settings.SqlServer)

Builder extension to store and retrieve settings keys and values in SQL Server.

## Example of use

### Get and set value in SQL Server using ConnectionString.
```csharp
using Ridavei.Settings;
using Ridavei.Settings.SqlServer;

namespace Example
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string connectionString = "YOUR_CONNECTIONSTRING";
            var builder = SettingsBuilder
                .CreateBuilder()
                .UseSqlServerManager(connectionString);
            using (var settings = builder.GetSettings("DictionaryName"))
            {
                //You can use settings.Get("ExampleKey", "DefaultValue") if you want to retrieve the default value if the key doesn't exists.
                var value = settings.Get("ExampleKey");
                settings.Set("AnotherKey", "NewValue");
            }
        }
    }
}

```

### Get and set value in SQL Server using ConnectionString.
```csharp
using System.Data.SqlClient;

using Ridavei.Settings;
using Ridavei.Settings.SqlServer;

namespace Example
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string connectionString = "YOUR_CONNECTIONSTRING";
            var builder = SettingsBuilder.CreateBuilder();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var settings = builder
                    .UseSqlServerManager(connection)
                    .GetSettings("DictionaryName"))
                {
                    //You can use settings.Get("ExampleKey", "DefaultValue") if you want to retrieve the default value if the key doesn't exists.
                    var value = settings.Get("ExampleKey");
                    settings.Set("AnotherKey", "NewValue");
                }
            }
        }
    }
}

```
