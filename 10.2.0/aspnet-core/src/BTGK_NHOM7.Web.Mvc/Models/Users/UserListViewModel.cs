using BTGK_NHOM7.Roles.Dto;
using System.Collections.Generic;

namespace BTGK_NHOM7.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
