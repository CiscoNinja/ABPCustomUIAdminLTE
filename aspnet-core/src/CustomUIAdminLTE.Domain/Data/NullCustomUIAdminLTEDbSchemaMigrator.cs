using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CustomUIAdminLTE.Data;

/* This is used if database provider does't define
 * ICustomUIAdminLTEDbSchemaMigrator implementation.
 */
public class NullCustomUIAdminLTEDbSchemaMigrator : ICustomUIAdminLTEDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
