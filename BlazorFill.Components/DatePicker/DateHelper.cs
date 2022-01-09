using System.Globalization;
using JetBrains.Annotations;

namespace BlazorFill.Components.DatePicker;

public static class DateHelper
{
    private static readonly string[] Months = { "January" };

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

    public static DateCell GetDate(DateTime reference, int i)
    {
        var startOfMonth = new DateTime(reference.Year, reference.Month, 1);
        var dayOfWeek = (int)startOfMonth.DayOfWeek;
        dayOfWeek -= 2;
        i -= dayOfWeek;

        var lastMonth = reference.AddMonths(-1);
        var daysInLastMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);

        var daysInMonth = DateTime.DaysInMonth(reference.Year, reference.Month);
        if (i < 1) i += daysInLastMonth;
        else if (i > daysInMonth) i -= daysInMonth;
        else return new DateCell(i.ToString(), "valid");

        return new DateCell(i.ToString(), "invalid");
    }
}

[PublicAPI]
public class DateRange
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public sealed record DateCell(string Date, string Valid);