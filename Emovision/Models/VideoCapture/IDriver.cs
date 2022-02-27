using OpenCvSharp;

namespace Emovision.Models.VideoCapture
{
    public interface IDriver
    {
        VideoCaptureAPIs Api { get; }
        DeviceInfoCollection AvailableDevices { get; }
    }
}