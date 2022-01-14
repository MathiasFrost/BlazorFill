using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorFill.DOM.Window;

public static class Window
{
    /// <inheritdoc cref="Dom"/>
    private static IJSObjectReference? _module;

    private const string Path = "./_content/BlazorFill.DOM/window.js";
    private static readonly Dictionary<int, Func<MouseEventArgs, Task>> Events = new();

    /// <summary>register an async C# event on window click</summary>
    /// <param name="jsRuntime">injected instance</param>
    /// <param name="callback">C# function to call on window click</param>
    public static async Task AddOnWindowClickAsync(this IJSRuntime jsRuntime, Func<MouseEventArgs, Task> callback)
    {
        await jsRuntime.AddWindowClickEventAsync(callback, false).ConfigureAwait(false);
    }

    /// <summary>register an async C# event on window click to be called once</summary>
    /// <param name="jsRuntime">injected instance</param>
    /// <param name="callback">C# function to call on window click</param>
    public static async Task AddOnWindowClickOnceAsync(this IJSRuntime jsRuntime, Func<MouseEventArgs, Task> callback)
    {
        await jsRuntime.AddWindowClickEventAsync(callback, true).ConfigureAwait(false);
    }

    /// <summary>remove an async C# event that has been registered using <see cref="AddOnWindowClickAsync"/></summary>
    /// <param name="jsRuntime">injected instance</param>
    /// <param name="callback">C# function to remove</param>
    public static async Task RemoveWindowClickEventAsync(this IJSRuntime jsRuntime, Func<MouseEventArgs, Task> callback)
    {
        // if _module is null here, this function was called before an event has been added
        if (_module == null) return;

        Events.Remove(callback.GetHashCode());

        await _module.InvokeVoidAsync(JsEvents.RemoveEvent, nameof(HandleWindowClickCallback),
            callback.GetHashCode()).ConfigureAwait(false);
    }

    /// <summary>this function is called by JavaScript. never call from C#</summary>
    /// <param name="args">JavaScript Event object</param>
    /// <param name="hashCode">hashcode of function, stored in JavaScript</param>
    /// <param name="once">if true, remove event from dictionary</param>
    [JSInvokable]
    public static async Task HandleWindowClickCallback(MouseEventArgs args, int hashCode, bool once)
    {
        try
        {
            var callback = Events[hashCode];
            await callback.Invoke(args);
            if (once) Events.Remove(callback.GetHashCode());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Function with {hashCode}, called from JavaScript was not found:\n" + e);
        }
    }

    private static async Task AddWindowClickEventAsync(this IJSRuntime jsRuntime, Func<MouseEventArgs, Task> callback,
        bool once)
    {
        _module ??= await jsRuntime.ImportModuleAsync(Path).ConfigureAwait(false);
        var added = Events.TryAdd(callback.GetHashCode(), callback);
        if (!added) return;

        await _module.InvokeVoidAsync(once ? JsEvents.AddOnWindowClickOnce : JsEvents.AddOnWindowClick,
            nameof(HandleWindowClickCallback), callback.GetHashCode()).ConfigureAwait(false);
    }
}