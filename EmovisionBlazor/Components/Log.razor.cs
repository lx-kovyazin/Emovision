using Microsoft.AspNetCore.Components;

namespace EmovisionBlazor.Components
{
    public partial class Log
    {
        [Inject] public ILogger<Log> Logger { get; set; }

        //protected override void OnInitialized()
        //{
        //    Logger.LogTrace(nameof(OnInitialized));
        //}

    }
}
