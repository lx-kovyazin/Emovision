
namespace Emovision.Models.VideoCapture
{
    public record DeviceInfo(
        IDriver Driver,
        int     Index,
        string  Name,
        string  Path
        );
}