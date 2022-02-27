using OpenCvSharp;

namespace Emovision.Models.VideoCapture.Api
{
    public class DirectShow
        : IDriver
    {
        public VideoCaptureAPIs Api => VideoCaptureAPIs.DSHOW;

        public DeviceInfoCollection AvailableDevices => throw new System.NotImplementedException();
    }
}