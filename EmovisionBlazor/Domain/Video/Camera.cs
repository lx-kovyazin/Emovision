namespace EmovisionBlazor.Domain.Video;

public class Camera
    : Source
{
	public Camera()
		: base("setupCameraSource")
	{ }

	//        public Camera(MediaDeviceInfo deviceInfo)
	//            : this(deviceInfo, CameraSettings.Default)
	//        { }

	//        public Camera(MediaDeviceInfo deviceInfo, CameraSettings settings)
	//        {
	//            DeviceInfo = deviceInfo;
	//            Settings   = settings;
	//        }

	//        public MediaDeviceInfo DeviceInfo { get; set; }
	//        public CameraSettings  Settings { get; set; }

	//        //public override string ToString()
	//        //{
	//        //    return DeviceInfo.Name;
	//        //}
	//    }
}
