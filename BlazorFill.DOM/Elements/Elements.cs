using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorFill.DOM.Elements;

public static class Elements
{
    /// <inheritdoc cref="Dom"/>
    private static IJSObjectReference? _module;

    private const string Path = "./_content/BlazorFill.DOM/elements.js";

    public static async Task<DomRect> GetBoundingClientRect(this IJSRuntime jsRuntime,
        ElementReference elementReference)
    {
        await using var module = await jsRuntime.ImportModuleAsync(Path).ConfigureAwait(false);
        return await module.InvokeAsync<DomRect>(JsEvents.GetBoundingClient, elementReference)
            .ConfigureAwait(false);
    }
}