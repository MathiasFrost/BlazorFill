using JetBrains.Annotations;

namespace BlazorFill.DOM;

[PublicAPI]
public class AnimationTransitionArgs
{
    public string AnimationName { get; set; } = "";
    public bool Bubbles { get; set; }
    public bool CancelBubble { get; set; }
    public bool Cancelable { get; set; }
    public bool Composed { get; set; }
    public bool DefaultPrevented { get; set; }
    public double ElapsedTime { get; set; }
    public double EventPhase { get; set; }
    public bool IsTrusted { get; set; }
    public string PseudoElement { get; set; } = "";
    public double TimeStamp { get; set; }
    public string Type { get; set; } = "";
}

[PublicAPI]
public class AnimationTransitionOptions
{
    public bool? Start { get; set; }
    public bool? End { get; set; }
    public bool? Cancel { get; set; }
}