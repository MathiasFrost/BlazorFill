using Microsoft.JSInterop;

namespace BlazorFill.DOM.Document;

public static class Document
{
    /// <inheritdoc cref="Dom"/>
    private static IJSObjectReference? _module;

    private const string Path = "./_content/BlazorFill.DOM/document.js";
}