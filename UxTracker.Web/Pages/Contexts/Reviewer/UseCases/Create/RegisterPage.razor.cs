using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UxTracker.Web.Pages.Contexts.Reviewer.UseCases.Create
{
  public partial class Register : ComponentBase
  {
    [Inject]
    protected ISnackbar Snackbar { get; set; }

    protected string Email { get; set; }
    protected string Gender { get; set; }
    protected DateTime? BirthDate { get; set; }
    protected string Country { get; set; }
    protected string State { get; set; }
    protected string City { get; set; }

    protected async Task HandleValidSubmit()
    {
      if (ValidateEmail(Email) && ValidateGender(Gender) && ValidateDate(BirthDate) && ValidateLocation(Country, State, City))
      {
        Snackbar.Add("Registro bem-sucedido.", Severity.Success);
      }
      else
      {
        Snackbar.Add("Verifique os dados do formulário e tente novamente.", Severity.Error);
      }
    }

    protected bool ValidateEmail(string email)
    {
      return !string.IsNullOrEmpty(email) && email.Contains("@");
    }

    protected bool ValidateGender(string gender)
    {
      return !string.IsNullOrEmpty(gender);
    }

    protected bool ValidateDate(DateTime? date)
    {
      return date.HasValue && date.Value <= DateTime.Now;
    }

    protected bool ValidateLocation(string country, string state, string city)
    {
      return !string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(city);
    }
  }
}
