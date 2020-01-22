using Microsoft.AspNetCore.Mvc.Razor;
using Web.Infrastructures.Authorizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views
{
    public abstract class BaseRazorPage<TModel> : RazorPage<TModel>
    {
        protected virtual bool IsGranted(string permissionName)
        {
            return User.HasPermission(permissionName);
        }
    }
}
