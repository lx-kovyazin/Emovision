using System.Text.RegularExpressions;

namespace EmovisionBlazor.Domain.Video
{
    public class RawFrame
    {
        protected string _type;

        public byte[] Data { get; init; } = Array.Empty<byte>();

        public static RawFrame? Create(string raw)
        {
            var m = Regex.Match(raw, @$"^data:image/(?<{nameof(_type)}>\w+);base64,(?<{nameof(Data)}>\S+)$");
            return !m.Success
                ? null
                : new RawFrame()
                {
                    _type = m.Groups[nameof(_type)].Value,
                    Data = Convert.FromBase64String(m.Groups[nameof(Data)].Value)
                };
        }
    }
}