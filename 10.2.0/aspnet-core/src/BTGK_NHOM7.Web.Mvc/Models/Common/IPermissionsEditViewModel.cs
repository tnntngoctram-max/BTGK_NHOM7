using BTGK_NHOM7.Roles.Dto;
using System.Collections.Generic;

namespace BTGK_NHOM7.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}