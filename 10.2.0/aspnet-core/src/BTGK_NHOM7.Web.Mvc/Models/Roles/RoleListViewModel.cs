using BTGK_NHOM7.Roles.Dto;
using System.Collections.Generic;

namespace BTGK_NHOM7.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
