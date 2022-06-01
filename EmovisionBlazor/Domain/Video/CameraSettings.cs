namespace EmovisionBlazor.Domain.Video
{
    public struct CameraSettings
    {
        public int Width;
        public int Height;
        public int Fps;

        public static CameraSettings Default
            => new()
            {
                Width  = 640,
                Height = 480,
                Fps    = 30 ,
            };
    }
}
