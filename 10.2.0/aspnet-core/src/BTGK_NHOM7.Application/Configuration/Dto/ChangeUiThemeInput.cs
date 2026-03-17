using System.ComponentModel.DataAnnotations;

namespace BTGK_NHOM7.Configuration.Dto;

public class ChangeUiThemeInput
{
    [Required]
    [StringLength(32)]
    public string Theme { get; set; }
}
