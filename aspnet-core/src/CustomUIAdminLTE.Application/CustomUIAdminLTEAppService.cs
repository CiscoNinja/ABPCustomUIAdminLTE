using System;
using System.Collections.Generic;
using System.Text;
using CustomUIAdminLTE.Localization;
using Volo.Abp.Application.Services;

namespace CustomUIAdminLTE;

/* Inherit your application services from this class.
 */
public abstract class CustomUIAdminLTEAppService : ApplicationService
{
    protected CustomUIAdminLTEAppService()
    {
        LocalizationResource = typeof(CustomUIAdminLTEResource);
    }
}
