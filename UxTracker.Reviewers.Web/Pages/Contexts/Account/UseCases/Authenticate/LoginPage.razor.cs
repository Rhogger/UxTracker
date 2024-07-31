using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UxTracker.Reviewers.Web.Pages.Contexts.Account.UseCases.Authenticate
{
  public partial class Login : ComponentBase
  {
    [Inject]
    protected ISnackbar Snackbar { get; set; }

    protected string Email { get; set; }
    protected string ReviewCode { get; set; }

    protected async Task HandleValidSubmit()
    {
      if (ValidateEmail(Email) && ValidateReviewCode(ReviewCode))
      {
        Snackbar.Add("Login bem-sucedido.", Severity.Success);
      }
      else
      {
        Snackbar.Add("E-mail ou código da avaliação inválido.", Severity.Error);
      }
    }

    protected bool ValidateEmail(string email)
    {
      return !string.IsNullOrEmpty(email) && email.Contains("@");
    }

    protected bool ValidateReviewCode(string reviewCode)
    {
      return reviewCode == "codigo123";
    }
  }
}
