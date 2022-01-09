using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorFill.DOM.Transitions;

public static class Transitions
{
    /// <inheritdoc cref="Dom"/>
    private static IJSObjectReference? _module;

    private const string Path = "./_content/BlazorFill.DOM/transitions.js";
    private static readonly Dictionary<int, Func<TransitionEventArgs, Task>> Events = new();

    public static async Task AddTransitionEventsAsync(this IJSRuntime jsRuntime, ElementReference elementReference,
        Func<TransitionEventArgs, Task> callback, TransitionEventOptions options)
    {
        await jsRuntime.AddTransitionAsync(elementReference, callback, options, false).ConfigureAwait(false);
    }

    public static async Task AddTransitionEventsOnceAsync(this IJSRuntime jsRuntime, ElementReference elementReference,
        Func<TransitionEventArgs, Task> callback, TransitionEventOptions options)
    {
        await jsRuntime.AddTransitionAsync(elementReference, callback, options, true).ConfigureAwait(false);
    }

    /// <summary>remove an async C# event that has been registered using <see cref="AddAnimationEventsAsync"/></summary>
    /// <param name="jsRuntime">injected instance</param>
    /// <param name="callback">C# function to remove</param>
    public static async Task RemoveTransitionEventAsync(this IJSRuntime jsRuntime,
        Func<TransitionEventArgs, Task> callback)
    {
        _module ??= await jsRuntime.ImportModuleAsync(Path).ConfigureAwait(false);

        Events.Remove(callback.GetHashCode());

        await _module.InvokeVoidAsync(JsEvents.RemoveEvent, nameof(HandleTransitionCallback),
            callback.GetHashCode()).ConfigureAwait(false);
    }

    private static async Task AddTransitionAsync(this IJSRuntime jsRuntime, ElementReference elementReference,
        Func<TransitionEventArgs, Task> callback, TransitionEventOptions options, bool once)
    {
        _module ??= await jsRuntime.ImportModuleAsync(Path).ConfigureAwait(false);
        var added = Events.TryAdd(callback.GetHashCode(), callback);
        if (!added) return;

        await _module.InvokeVoidAsync(once ? JsEvents.AddTransitionEventsOnce : JsEvents.AddTransitionEvents,
            nameof(HandleTransitionCallback), callback.GetHashCode(), elementReference, options).ConfigureAwait(false);
    }

    /// <summary>this function is called by JavaScript. never call from C#</summary>
    /// <param name="args">JavaScript Event object</param>
    /// <param name="hashCode">hashcode of function, stored in JavaScript</param>
    /// <param name="once">if true, remove event from dictionary</param>
    [JSInvokable]
    public static async Task HandleTransitionCallback(TransitionEventArgs args, int hashCode, bool once)
    {
        try
        {
            var callback = Events[hashCode];
            await callback.Invoke(args);
            if (once) Events.Remove(callback.GetHashCode());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Function with {hashCode}, called from JavaScript was not found" + e);
        }
    }
}