
using System.Diagnostics;
using OpenCvSharp;

namespace Emovision.Models.VideoCapture.Api
{
    public class V4L2
        : IDriver
    {
        public V4L2()
        {
            using (var v4l2 = new Process() {
                    StartInfo = new ProcessStartInfo("v4l2-ctl", "--list-devices") {
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                    }
                })
            {
                v4l2.Start();
                var output = v4l2.StandardOutput.ReadToEnd();
            }
        }

        public VideoCaptureAPIs Api => VideoCaptureAPIs.V4L2;

        public DeviceInfoCollection AvailableDevices
            => new DeviceInfoCollection()
            {
                new DeviceInfo(this, 0, "cam 1", "/dev/video0"),
                new DeviceInfo(this, 1, "cam 2", "/dev/video1"),
            };
    }
}