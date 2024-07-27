using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.UpdatePasswordRecovery;

public partial class UpdatePasswordRecoveryPage : ComponentBase
{
    public string Password { get; set;} = string.Empty;

    public bool isShow;
    
    public InputType PasswordInput = InputType.Password;
    public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    public void ShowPassword()
    {
        if(isShow)
        {
            isShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
}