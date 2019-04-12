using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

public static class DateTimeHelper {
    public static DateTime GetIntervalStartDate(DateTime date, SelectionInterval interval) {
        int lastDay = interval == SelectionInterval.Day ? date.Day : 1;
        return new DateTime(date.Year, date.Month, lastDay, 0, 0, 0);
    }
    public static DateTime GetIntervalEndDate(DateTime date, SelectionInterval interval) {
        int firstDay = interval == SelectionInterval.Day ? date.Day : DateTime.DaysInMonth(date.Year, date.Month);
        return (new DateTime(date.Year, date.Month, firstDay, 23, 59, 59));
    }

    public static string GetDateString(DateTime date, string formatString) {
        if(date.Date == DateTime.Now.Date)
            return "Today";
        if(date.Date == (DateTime.Now.AddDays(-1)).Date)
            return "Yesterday";
        return date.ToString(formatString);
    }
    public static string GetDateRangeString(DateTime startDate, DateTime endDate) {
        if(HasSameYearAndMonth(startDate, endDate))
            return startDate.ToString("MMM yyyy").ToUpper();
        return string.Format("{0:MMM yyyy} - {1:MMM yyyy}", startDate, endDate).ToUpper();
    }
    public static bool HasSameYearAndMonth(DateTime dt1, DateTime dt2) {
        return dt1.Year == dt2.Year && dt1.Month == dt2.Month;
    }
}
public static class ScaleHelper {
    public static string GetAbbreviationMask(string text, AbbreviationType type) {
        double value;
        if(Double.TryParse(text, out value)) {
            switch(type) {
                case AbbreviationType.Millions:
                    return string.Format("{0:0.#;;0}M", (value / 1000000));
                case AbbreviationType.Thousands:
                    return string.Format("{0:0.#;;0}K", (value / 1000));
            }
        }
        return text;
    }
    public static string GetCurrencyAbbreviationMask(string text, AbbreviationType type) {
        return string.Format(GetCurrencyPattern(), GetAbbreviationMask(text, type), CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol);
    }
    private static string GetCurrencyPattern() {
        switch(CultureInfo.CurrentCulture.NumberFormat.CurrencyPositivePattern) {
            case 0:
                return "{1}{0}";
            case 1:
                return "{0}{1}";
            case 2:
                return "{1} {0}";
            case 3:
                return "{0} {1}";
            default:
                return "{0}";
        }
    }
}

public static class PaletteHelper {
    private const string PalettePath = "~/Content/SalesViewerPalette.xcp";

    private static readonly object commonPaletteLock = new object();
    private static Palette fCommonPallete = null;
    private static Palette CommonPallete {
        get {
            lock(commonPaletteLock) {
                if(fCommonPallete == null)
                    fCommonPallete = GetCommonPalette();
                return fCommonPallete;
            }
        }
    }
    public static void LoadCommonPalette(WebChartControl control) {
        control.PaletteWrappers.Add(new PaletteWrapper(CommonPallete));
        control.PaletteName = CommonPallete.Name;
    }
    public static Color GetCommonPalletePointColor(int index) {
        index = index % CommonPallete.Count;
        return CommonPallete[index].Color;
    }
    public static string GetCommonPalletePointCssColor(int index) {
        Color c = GetCommonPalletePointColor(index);
        return String.Format("rgb({0}, {1}, {2})", c.R, c.G, c.B);
    }

    private static Palette GetCommonPalette() {
        Palette palette = new Palette("common");
        palette.LoadFromXml(HttpContext.Current.Server.MapPath(PalettePath));
        return palette;
    }
}

public enum AbbreviationType {
    NoAbbreviation,
    Millions,
    Thousands
}
public enum SelectionInterval {
    Day = 0,
    Month = 1
}
