using System.Text.Json;
using Flunt.Notifications;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core.Contexts.Account.UseCases.Create;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.Create;



public partial class Register : ComponentBase
{
    [Inject]
    protected ISnackbar Snackbar { get; set; }
    
    [Inject]
    protected IRestClient RestClient { get; set; }
    
    protected Request Req = new();

    protected MudForm Form;
    protected List<Notification> Notifications = new();
    protected bool IsValid;

    protected async Task CreateAsync()
    {
        //TODO: Refatorar aqui
        //TODO: Criar um Handler
        //TODO: Trabalhar na validação 
        var request = new RestRequest("/api/v1/users", Method.Post)
            .AddJsonBody(Req);

        try
        {
            var response = await RestClient.ExecuteAsync<Response>(request);

                
            if (response.Data != null)
            {
                if (response.Data.StatusCode == 201)
                {
                    Snackbar.Add("Conta criada com sucesso!", Severity.Success);
                }
                else
                {
                    // if (response.Data.Notifications != null)
                    // foreach (var notification in response.Data.Notifications)
                    // Snackbar.Add($"Erro: {notification.Message}", Severity.Error);
                    // else
                    Snackbar.Add($"Erro: {response.Data.Message}", Severity.Error);
                            
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
}