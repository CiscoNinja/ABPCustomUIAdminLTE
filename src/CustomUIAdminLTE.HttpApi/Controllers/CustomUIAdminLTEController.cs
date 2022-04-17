using CustomUIAdminLTE.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CustomUIAdminLTE.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CustomUIAdminLTEController : AbpControllerBase
{
    protected CustomUIAdminLTEController()
    {
        LocalizationResource = typeof(CustomUIAdminLTEResource);
    }
}
