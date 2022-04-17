using Volo.Abp.Modularity;

namespace CustomUIAdminLTE;

[DependsOn(
    typeof(CustomUIAdminLTEApplicationModule),
    typeof(CustomUIAdminLTEDomainTestModule)
    )]
public class CustomUIAdminLTEApplicationTestModule : AbpModule
{

}
