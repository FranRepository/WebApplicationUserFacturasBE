using WebApplicationBE.Data;

namespace WebApplicationBE
{
    public static class MigrationManager
    {
        public static IHost MigrateDataBase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                { 
                 var logger = scope.ServiceProvider?.GetService<ILogger<Program>>();
                    logger.LogError(ex, "An Errror Ocurring");
                }
            }
            return host;
        }

    }
}
