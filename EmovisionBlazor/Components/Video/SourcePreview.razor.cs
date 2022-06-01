using EmovisionBlazor.Domain.Video;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmovisionBlazor.Components.Video;

public partial class SourcePreview
{
    [Parameter, EditorRequired]
    public Source? Source { get; set; }
}
