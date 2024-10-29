using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using MudBlazor.Utilities;

namespace UxTracker.Web.Components.MudBlazorExtensions;

public class GridItem : MudItem
{
    /// <summary>
    /// Sets the number of columns to occupy at the 'extra, extra small' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int Xxs { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
    
        var extraClasses = new CssBuilder()
            .AddClass($"mud-grid-item-xxs-{Xxs}", Xxs != 0)
            .Build();

        Class = $"{Class} {extraClasses}";
    }}