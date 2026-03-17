using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BTGK_NHOM7.Web.Views.Shared.Components.AccountLanguages;

public class AccountLanguagesViewComponent : BTGK_NHOM7ViewComponent
{
    private readonly ILanguageManager _languageManager;

    public AccountLanguagesViewComponent(ILanguageManager languageManager)
    {
        _languageManager = languageManager;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        var model = new LanguageSelectionViewModel
        {
            CurrentLanguage = _languageManager.CurrentLanguage,
            Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
            .Where(l => !l.IsDisabled)
            .ToList(),
            CurrentUrl = Request.Path
        };

        return Task.FromResult(View(model) as IViewComponentResult);
    }
}
