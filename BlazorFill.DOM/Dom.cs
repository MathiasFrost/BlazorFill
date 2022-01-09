using Microsoft.JSInterop;

namespace BlazorFill.DOM;

// the below doc is for _module fields
/// <summary>
/// JavaScript files <b>cannot</b> be unloaded, so this module is loaded first time an event is called,
/// then stored for the rest of the app's lifetime
/// </summary>
public static class Dom
{
    /// <summary>load the JavaScript file for this class</summary>
    /// <param name="jsRuntime"></param>
    /// <param name="path">relative path to JavaScript file</param>
    public static async Task<IJSObjectReference> ImportModuleAsync(this IJSRuntime jsRuntime, string path)
    {
        return await jsRuntime.InvokeAsync<IJSObjectReference>("import", path).ConfigureAwait(false);
    }
}