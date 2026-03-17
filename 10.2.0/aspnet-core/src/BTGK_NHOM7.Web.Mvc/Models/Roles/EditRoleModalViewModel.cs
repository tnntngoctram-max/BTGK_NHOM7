using Abp.AutoMapper;
using BTGK_NHOM7.Roles.Dto;
using BTGK_NHOM7.Web.Models.Common;

namespace BTGK_NHOM7.Web.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}
