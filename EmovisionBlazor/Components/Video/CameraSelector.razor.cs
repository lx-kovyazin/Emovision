//using BlazorMedia;
//using BlazorMedia.Models;
//using EmovisionBlazor.Domain.Video;
//using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;

//namespace EmovisionBlazor.Components.Video
//{
//    public class CameraSelectorViewModel: ComponentBase
//	{
//		[Inject]
//        public IJSRuntime JSRuntime { get; set; }

//		protected BlazorMediaAPI? BlazorMediaApi { get; set; }
//		protected CameraList? cameraList = null;

//		protected override async Task OnInitializedAsync()
//		{
//			await base.OnInitializedAsync();
//		}

//        protected override async Task OnAfterRenderAsync(bool firstRender)
//        {
//			if (firstRender)
//            {
//				BlazorMediaApi = new BlazorMediaAPI(JSRuntime);
//				await BlazorMediaApi.StartDeviceChangeListenerAsync();
//				var devices = await BlazorMediaApi?.EnumerateMediaDevices();
//				cameraList = devices.FindAll(md => md.Kind is MediaDeviceKind.VideoInput).ToCameraList();
//			}
//            await base.OnAfterRenderAsync(firstRender);
//        }
//    }
//}
