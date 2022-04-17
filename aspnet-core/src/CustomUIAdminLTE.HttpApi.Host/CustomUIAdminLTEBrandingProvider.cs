using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CustomUIAdminLTE;

[Dependency(ReplaceServices = true)]
public class CustomUIAdminLTEBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CustomUIAdminLTE";
}
