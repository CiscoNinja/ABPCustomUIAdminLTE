using System.Threading.Tasks;

namespace CustomUIAdminLTE.Data;

public interface ICustomUIAdminLTEDbSchemaMigrator
{
    Task MigrateAsync();
}
