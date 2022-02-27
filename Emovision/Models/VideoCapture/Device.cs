using System.Text;
using OpenCvSharp;
using OpenCvSharpVideoCapture = OpenCvSharp.VideoCapture;

namespace Emovision.Models.VideoCapture
{
    public class Device
        : OpenCvSharpVideoCapture
    {
        public DeviceInfo Info { get; }
        public Device(DeviceInfo info)
            : base(info.Index, info.Driver.Api)
            => Info = info;

        public override string? ToString()
        {
            return Info.ToString();
        }
    }
}