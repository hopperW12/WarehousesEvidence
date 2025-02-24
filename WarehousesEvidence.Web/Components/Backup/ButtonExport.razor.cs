using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WarehousesEvidence.App.Services;

namespace WarehousesEvidence.Web.Components.Backup;

public partial class ButtonExport
{
    [Inject]
    private IDatabaseService _databaseService { get; set; }
    [Inject]
    private IJSRuntime _javaScript { get; set; }
 
    private async Task OnExport()
    {
        var fileName = $"{DateTime.Now:dd-MM-yyyy-H-mm-ss}-backup.json";
        var content = await _databaseService.ExportToJson();
        
        await _javaScript.InvokeVoidAsync("downloadFile", fileName, content);
    }

}