

using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinalPaper.Migration; 

public class MigrationDbContextBuilder<T> : IDesignTimeDbContextFactory<T>
    where T : DbContext
    {
        private const string DbServerParam = "--dbServer";
        private const string DbNameParam = "--dbName";
        
        public T CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var appSettings = GetAppSettingsFileName(environmentName);

            Console.WriteLine("Environment: {0}", environmentName);

            var builder = new ConfigurationBuilder();

            IConfigurationRoot configuration = builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile(appSettings)
                .AddEnvironmentVariables()
                .Build();
            
            configuration = builder.Build();

            var contextBuilder = new DbContextOptionsBuilder<T>();

            contextBuilder.LogTo(Console.WriteLine);

            var dbNameParamIndex = Array.IndexOf(args, DbNameParam);

            var dbName = string.Empty;
            if (dbNameParamIndex > -1)
                dbName = args[dbNameParamIndex + 1];
            
            var dbServerParamIndex = Array.IndexOf(args, DbServerParam);

            var dbServer = string.Empty;
            if (dbServerParamIndex > -1)
                dbServer = args[dbServerParamIndex + 1];

            //TODO: we need to fix this
            var connectionString = "Server=localhost;Initial Catalog=FinalPaper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            /*
            if (args.Length > 1 && args[1] == "UseNetTopologySuite")
            {
                contextBuilder.UseSqlServer(connectionString, o => o.UseNetTopologySuite());
            }
            else
            {
                contextBuilder.UseSqlServer(connectionString);
            }*/
            contextBuilder.UseSqlServer(connectionString);
            
            return BuildDbContextInstance(contextBuilder.Options);
        }

        private static T BuildDbContextInstance(DbContextOptions<T> contextBuilderOptions)
        {
            return Activator.CreateInstance(typeof(T), contextBuilderOptions) as T;
        }

        public T CreateDbContextFromConnection(string[] args, DbConnection connection)
        {
            var contextBuilder = new DbContextOptionsBuilder<T>();

            contextBuilder.LogTo(Console.WriteLine);

            /*
            if (args.Length > 1 && args[1] == "UseNetTopologySuite")
            {
                contextBuilder.UseSqlServer(connection, o => o.UseNetTopologySuite());
            }
            else
            {
                contextBuilder.UseSqlServer(connection);
            }
            */
            contextBuilder.UseSqlServer(connection);

            return BuildDbContextInstance(contextBuilder.Options);
        }

        public SqlConnection BuildSqlConnection(string[] args)
        {
            var environmentName = BuildConfiguration(out var configuration);

            var contextBuilder = new DbContextOptionsBuilder<T>();

            contextBuilder.LogTo(Console.WriteLine);

            var connectionString = "PrepareConnectionString(configuration, environmentName, args[0])";

            return new SqlConnection(connectionString);
        }

        private static string BuildConfiguration(out IConfigurationRoot configuration)
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var appSettings = GetAppSettingsFileName(environmentName);

            Console.WriteLine("Environment: {0}", environmentName);

            var builder = new ConfigurationBuilder();

            configuration = builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile(appSettings)
                .AddEnvironmentVariables()
                .Build();

                configuration = builder.Build();

            return environmentName;
        }
         private static string GetAppSettingsFileName(string environmentName) {
             var isUnix = true;

            var configurationFileExists =
                isUnix ? File.Exists($"appsettings.Unix.json") : File.Exists($"appsettings.{environmentName}.json");

            if (!configurationFileExists) return "appsettings.json";

            return isUnix ? "appsettings.Unix.json" : $"appsettings.{environmentName}.json";
        }
        
        
       
    }