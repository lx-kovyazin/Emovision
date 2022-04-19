using OpenCvSharp;
using DirectShowLib;
using System.Linq;

namespace Emovision.Models.VideoCapture.Api
{
    public class DirectShow
        : IDriver
    {
        public VideoCaptureAPIs Api => VideoCaptureAPIs.DSHOW;

        public DeviceInfoCollection AvailableDevices
        {
            get {
                var devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)
                                      .Select((device, index) => new DeviceInfo(
                                          Driver: this,
                                          Index : index,
                                          Name  : device.Name,
                                          Path  : device.DevicePath
                                      ));

                return new DeviceInfoCollection(devices);
            }
        }
    }
}