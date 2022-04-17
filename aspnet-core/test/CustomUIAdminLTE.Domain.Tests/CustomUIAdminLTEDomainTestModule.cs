using CustomUIAdminLTE.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CustomUIAdminLTE;

[DependsOn(
    typeof(CustomUIAdminLTEEntityFrameworkCoreTestModule)
    )]
public class CustomUIAdminLTEDomainTestModule : AbpModule
{

}
