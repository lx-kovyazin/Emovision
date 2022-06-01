using EmovisionBlazor.Domain.Video;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmovisionBlazor.Components.Video;

public partial class SourcePreview
{
	[Inject]
	public IJSRuntime JSRuntime { get; set; }

	[Parameter, EditorRequired]
    public Source? Source { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await Source?.ConfigureAsync(JSRuntime);
		}
		base.OnAfterRender(firstRender);
	}
}
