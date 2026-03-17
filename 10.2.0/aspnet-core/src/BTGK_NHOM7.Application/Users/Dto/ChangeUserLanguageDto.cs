using System.ComponentModel.DataAnnotations;

namespace BTGK_NHOM7.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}