using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace EmovisionBlazor.Components.Video;

public partial class FileUploader
{
    [Inject]
    public ILogger<FileUploader> Logger { get; set; } = null!;

    [Parameter]
    public string Dir { get; set; }

    [Parameter]
    public EventCallback OnUploaded { get; set; }

    private const int _maxFileSize = 1 * 1024 * 1024 * 1024;
    private bool _isUploading = false;

    private async Task UploadFile(InputFileChangeEventArgs e)
    {
        _isUploading = true;
        try
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
            await using FileStream fs = new(Path.Combine(Dir, e.File.Name), FileMode.Create);
            await e.File.OpenReadStream(_maxFileSize).CopyToAsync(fs);
            await using StreamWriter sw = new(Path.Combine(Dir, "meta"));
            await sw.WriteAsync(e.File.Name);
        }
        catch (Exception ex)
        {
            Logger.LogError($"File: {e.File} Error: {ex}");
        }
        _isUploading = false;
        await OnUploaded.InvokeAsync();
    }
}
