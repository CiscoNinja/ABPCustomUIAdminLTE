using CustomUIAdminLTE.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CustomUIAdminLTE.Permissions;

public class CustomUIAdminLTEPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CustomUIAdminLTEPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CustomUIAdminLTEPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CustomUIAdminLTEResource>(name);
    }
}
