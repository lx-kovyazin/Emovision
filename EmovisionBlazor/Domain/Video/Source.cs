using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace EmovisionBlazor.Domain.Video;

public abstract class Source
{
    protected readonly string _setupMethodName;

    protected IJSRuntime _js;
    protected Source(string setupMethodName)
    {
        _setupMethodName = setupMethodName;
    }

    public string Id { get => GetType().Name; }
    public SourceState State { get; set; } = SourceState.Idle;

    public SourceSettings? Settings = null;

    protected abstract object[] Args { get; }

    public async Task ConfigureAsync(IJSRuntime js)
    {
        _js = js;
        await _js.InvokeVoidAsync(_setupMethodName, Args.Prepend(Id).ToArray());
        //object retval = await _js.InvokeAsync<object>("getSettings");
        //Settings = JsonConvert.DeserializeObject<SourceSettings>(retval?.ToString()!);
        State = SourceState.Configured;
    }

    public async Task ConfigureSettings()
	{

	}

    public async Task StartAsync()
    {
        while (State is not SourceState.Configured)
            await Task.Delay(100);

        await _js.InvokeVoidAsync("start");
        State = SourceState.Started;
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1.0/* / settings.FrameRate*/));
        while (await timer.WaitForNextTickAsync())
        {
            await _js.InvokeVoidAsync("captureFrame", Id);
        }
    }

    public async Task StopAsync()
    {
        if (State is SourceState.Started)
        {
            await _js.InvokeVoidAsync("stop");
            State = SourceState.Stopped;
        }
    }

    public void Reset() => State = SourceState.Idle;

    [JSInvokable]
    public static async Task CaptureImageAsync(string rawFrame)
    {
        //;
        var frame = RawFrame.Create(rawFrame);

        using var image = Image.FromStream(new MemoryStream(frame.Data));
        image.Save("output.png", ImageFormat.Png);  // Or Png
    }
}
