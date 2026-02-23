using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace INFRAESTRUCTURE.Context
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            // Detecta el directorio base desde donde se ejecuta el comando
            var currentDirectory = Directory.GetCurrentDirectory();

            // Busca el archivo appsettings.json en el proyecto de inicio
            var possiblePaths = new[]
            {
                Path.Combine(currentDirectory, "appsettings.json"),
                Path.Combine(currentDirectory, "..", "PLECSYS_STUDIO_DEMO", "appsettings.json"),
                Path.Combine(currentDirectory, "..", "JWT_PLECSYS_DEMO", "appsettings.json")
            };

            string configPath = null;

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    configPath = path;
                    break;
                }
            }

            if (configPath == null)
                throw new FileNotFoundException("No se encontró appsettings.json en ninguna ruta esperada.");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(configPath)!)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
