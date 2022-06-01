using EmovisionBlazor.Domain.MediaStreamsApi;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmovisionBlazor.Domain
{
    public class MediaStreamsApiService
        : IMediaStreamsApi
    {
        public MediaStreamsApiService(IJSRuntime JSRuntime)
        {
            MediaDevices = new(JSRuntime);
        }

        public MediaDevices MediaDevices { get; }
    }
}
