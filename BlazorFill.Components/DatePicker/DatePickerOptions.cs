using JetBrains.Annotations;

namespace BlazorFill.Components.DatePicker;

[PublicAPI]
public class DatePickerOptions
{
    public DatePickerMode Mode { get; set; } = DatePickerMode.Range;
}

[PublicAPI]
public enum DatePickerMode
{
    Single,
    Multi,
    Range
}