using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using WarehousesEvidence.App.Services;

namespace WarehousesEvidence.Web.Components.Backup;

public partial class ButtonImport 
{
    [Inject]
    public ISnackbar _snackbar { get; set; }
    [Inject]
    private IDatabaseService _databaseService { get; set; }
    [Inject]
    private IJSRuntime _javaScript { get; set; }

    private async Task HandleOnImport(InputFileChangeEventArgs e)
    {
        var file = e.File;
        await using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        var data = await reader.ReadToEndAsync();

        var result = await _databaseService.ImportFromJson(data);
        if (result)
        {
            _snackbar.Add("Databáze byla uspěšně nahrazena", Severity.Success);
            return;
        }

        _snackbar.Add("Při nahrávání nastala chyba", Severity.Error);
    }

    private async Task OnImport()
    {
        await _javaScript.InvokeVoidAsync("openFilePicker", GetHashCode());
    }
}