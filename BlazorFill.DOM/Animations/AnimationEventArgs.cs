using JetBrains.Annotations;

namespace BlazorFill.DOM.Animations;

[UsedImplicitly]
public class AnimationEventArgs : AnimationTransitionArgs {}

public static class AnimationTypes
{
    public const string Start = "animationstart";
    public const string End = "animationend";
    public const string Cancel = "animationcancel";
    public const string Iteration = "animationiteration";
}