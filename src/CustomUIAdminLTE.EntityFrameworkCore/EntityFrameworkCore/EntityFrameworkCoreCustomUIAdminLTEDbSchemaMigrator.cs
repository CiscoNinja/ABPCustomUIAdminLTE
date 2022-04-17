using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CustomUIAdminLTE.Data;
using Volo.Abp.DependencyInjection;

namespace CustomUIAdminLTE.EntityFrameworkCore;

public class EntityFrameworkCoreCustomUIAdminLTEDbSchemaMigrator
    : ICustomUIAdminLTEDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCustomUIAdminLTEDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the CustomUIAdminLTEDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CustomUIAdminLTEDbContext>()
            .Database
            .MigrateAsync();
    }
}
