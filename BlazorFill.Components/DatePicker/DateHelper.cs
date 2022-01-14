using System.Globalization;
using JetBrains.Annotations;

namespace BlazorFill.Components.DatePicker;

internal static class DateHelper
{
    public static IEnumerable<string> GetWeekdayPrefixes()
    {
        var res = new List<string>();
        var reference = new DateTime();
        for (var i = 0; i < 7; i++)
        {
            var str = reference.ToString("D", CultureInfo.CurrentCulture);
            res.Add(str[..2]);
            reference = reference.AddDays(1);
        }

        return res;
    }

    public static DateCell GetDate(DateTime reference, int i, DateTime? date)
    {
        var startOfMonth = new DateTime(reference.Year, reference.Month, 1);
        var dayOfWeek = (int)startOfMonth.DayOfWeek;
        dayOfWeek -= 2;
        i -= dayOfWeek;

        var lastMonth = reference.AddMonths(-1);
        var daysInLastMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);

        var daysInMonth = DateTime.DaysInMonth(reference.Year, reference.Month);

        var selected = "";
        var today = "";

        if (i > 0 && i <= daysInMonth)
        {
            var thisDate = new DateTime(reference.Year, reference.Month, i);
            selected = thisDate.Equals(date) ? CssClasses.Selected : "";
            today = thisDate.Equals(DateTime.Today) ? CssClasses.Today : "";
        }

        if (i < 1) i += daysInLastMonth;
        else if (i > daysInMonth) i -= daysInMonth;
        else return new DateCell(i, CssClasses.Valid, today, selected);

        return new DateCell(i, CssClasses.Invalid, today, selected);
    }
}

[PublicAPI]
public class DateRange
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

internal sealed record DateCell(int Date, string Valid, string Today, string Selected);

internal static class CssClasses
{
    public const string Valid = "valid";
    public const string Invalid = "invalid";
    public const string Selected = "selected";
    public const string Today = "today";
}