using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core.Contexts.Account.UseCases.Create;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.Create;

public partial class Register : ComponentBase
{
    [Inject] protected ISnackbar Snackbar { get; set; }

    [Inject] protected IRestClient RestClient { get; set; }

    protected Request Req = new();
    
    protected MudForm Form;
    protected string[] Errors = Array.Empty<string>();
    protected bool IsValid;

    protected InputType PasswordInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    public bool IsPasswordShow;
    protected InputType ConfirmPasswordInput = InputType.Password;
    protected string ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    public bool IsConfirmPasswordShow;

    protected async Task CreateAsync()
    {
        var request = new RestRequest("/api/v1/users", Method.Post)
            .AddJsonBody(Req);

        try
        {
            var response = await RestClient.ExecuteAsync<Response>(request);

            if (response.Data != null)
            {
                if (response.Data.StatusCode == 201)
                    Snackbar.Add("Conta criada com sucesso!", Severity.Success);
                else
                {
                    if (response.Data.Notifications != null)
                        foreach (var notification in response.Data.Notifications)
                        {
                            Snackbar.Add($"Obrigatório: {notification.Message}", Severity.Error);
                        }
                    else
                        Snackbar.Add($"Erro: {response.StatusCode} - {response.Data.Message}", Severity.Error);
                }
            }
            else
            {
                Snackbar.Add($"Erro: {response.StatusCode} - {response.Content}", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            Snackbar.Add($"Exception: {ex.Message}", Severity.Error);
        }
    }

    public void ShowPassword()
    {
        if (IsPasswordShow)
        {
            IsPasswordShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            IsPasswordShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    public void ShowConfirmPassword()
    {
        if (IsConfirmPasswordShow)
        {
            IsConfirmPasswordShow = false;
            ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmPasswordInput = InputType.Password;
        }
        else
        {
            IsConfirmPasswordShow = true;
            ConfirmPasswordInputIcon = Icons.Material.Filled.Visibility;
            ConfirmPasswordInput = InputType.Text;
        }
    }

    protected static string? ValidateEmail(string email)
    {
        var contract = Specification.EnsureEmail(email);

        if (contract.IsValid)
        {
            Console.WriteLine("Email is valid");
            return null;
        }

        var error = contract.Notifications.FirstOrDefault()?.Message;
        Console.WriteLine($"Email error: {error}");
        return error;
    }

    protected static string? ValidateName(string name)
    {
        var contract = Specification.EnsureName(name);

        if (contract.IsValid)
        {
            Console.WriteLine("Name is valid");
            return null;
        }

        var error = contract.Notifications.FirstOrDefault()?.Message;
        Console.WriteLine($"Name error: {error}");
        return error;
    }

    protected static string? ValidatePassword(string password)
    {
        var contract = Specification.EnsurePassword(password);

        if (contract.IsValid)
        {
            Console.WriteLine("Password is valid");
            return null;
        }

        var error = contract.Notifications.FirstOrDefault()?.Message;
        Console.WriteLine($"Password error: {error}");
        return error;
    }

    protected static string? ComparePasswords(string password, string confirmPassword)
    {
        var contract = Specification.EnsureComparePasswords(password, confirmPassword);

        if (contract.IsValid)
        {
            Console.WriteLine("Passwords match");
            return null;
        }

        var error = contract.Notifications.FirstOrDefault()?.Message;
        Console.WriteLine($"Confirm password error: {error}");
        return error;
    }
}