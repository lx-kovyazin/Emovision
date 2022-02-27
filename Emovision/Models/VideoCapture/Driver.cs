using System;
using Emovision.Models.VideoCapture.Api;
using OpenCvSharp;

namespace Emovision.Models.VideoCapture
{
    public static class Driver
    {
        public static IDriver Create()
            => Environment.OSVersion.Platform switch {
                PlatformID.Unix => new V4L2(),
                PlatformID.Win32NT => new DirectShow(),
                _ => throw new NotImplementedException()
            };
    }
}