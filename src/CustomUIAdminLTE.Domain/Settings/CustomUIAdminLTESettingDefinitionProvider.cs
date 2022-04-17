using Volo.Abp.Settings;

namespace CustomUIAdminLTE.Settings;

public class CustomUIAdminLTESettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CustomUIAdminLTESettings.MySetting1));
    }
}
