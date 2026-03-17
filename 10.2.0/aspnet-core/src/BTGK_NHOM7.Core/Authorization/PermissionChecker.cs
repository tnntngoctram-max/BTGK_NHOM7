using Abp.Authorization;
using BTGK_NHOM7.Authorization.Roles;
using BTGK_NHOM7.Authorization.Users;

namespace BTGK_NHOM7.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
