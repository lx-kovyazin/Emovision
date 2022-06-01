namespace EmovisionBlazor.Domain.Video;

public class File
    : Source
{
    public File(string path)
        : base("setupFileSource")
    {
        Path = path;
    }

    public string Path { get; init; }
}
