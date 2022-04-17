using CustomUIAdminLTE.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CustomUIAdminLTE.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CustomUIAdminLTEEntityFrameworkCoreModule),
    typeof(CustomUIAdminLTEApplicationContractsModule)
    )]
public class CustomUIAdminLTEDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
