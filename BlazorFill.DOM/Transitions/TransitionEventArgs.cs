using JetBrains.Annotations;

namespace BlazorFill.DOM.Transitions;

[UsedImplicitly]
public class TransitionEventArgs : AnimationTransitionArgs {}

public static class AnimationTypes
{
    public const string Start = "transitionstart";
    public const string End = "transitionend";
    public const string Cancel = "transitioncancel";
    public const string Run = "transitionrun";
}