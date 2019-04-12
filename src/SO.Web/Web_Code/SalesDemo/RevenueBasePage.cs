using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

public abstract class RevenueBasePage : BasePage {
    protected class DateRange {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DateRange(DateTime startDate, DateTime endDate) {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    protected abstract VerticalChartControlBase DailySalesPerformanceVerticalChart { get; }
    protected abstract VerticalChartControlBase UnitSalesVerticalChart { get; }

    protected abstract IEnumerable<ChartData> GetChartSalesRevenueCollection(DateTime startDate, DateTime endDate);
    protected abstract IEnumerable<ChartData> GetChartSalesCountCollection(DateTime startDate, DateTime endDate);

    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        DailySalesPerformanceVerticalChart.RangeSelectionChanged += (s, arg) => PopulateDailySalesPerformanceChart();
        UnitSalesVerticalChart.RangeSelectionChanged += (s, arg) => PopulateUnitSalesChart();
    }
    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        if(!IsCallback) {
            PopulateDailySalesPerformanceChart();
            PopulateUnitSalesChart();
        }
    }

    protected void PopulateDailySalesPerformanceChart() {
        PopulateChart(DailySalesPerformanceVerticalChart, GetTodayRange(), GetYesterdayRange(), GetLastWeekRange(), GetChartCurrentRange(), GetChartPreviousRange(),
            getChartDataFunc: (startDate, endDate) => GetChartSalesRevenueCollection(startDate, endDate),
            getValueFunc: (startDate, endDate) => SalesProvider.GetSalesRevenue(startDate, endDate),
            dateToStringFunc: date => DateTimeHelper.GetDateString(date, "d"));
    }
    protected void PopulateUnitSalesChart() {
        PopulateChart(UnitSalesVerticalChart, GetCurrentMonthRange(), GetLastMonthRange(), GetYTDRange(), GetChartCurrentRange(true), GetChartPreviousRange(true),
            getChartDataFunc: (startDate, endDate) => GetChartSalesCountCollection(startDate, endDate),
            getValueFunc: (startDate, endDate) => SalesProvider.GetSaleCount(startDate, endDate),
            dateToStringFunc: date => date.ToString("MMMM"));
    }
    private void PopulateChart(VerticalChartControlBase chart, DateRange current, DateRange previous, DateRange custom, DateRange currentChart, DateRange previousChart,
        Func<DateTime, DateTime, IEnumerable<ChartData>> getChartDataFunc, Func<DateTime, DateTime, double> getValueFunc,
        Func<DateTime, string> dateToStringFunc) {

        chart.CurrentValue = getValueFunc(current.StartDate, current.EndDate);
        chart.PreviousValue = getValueFunc(previous.StartDate, previous.EndDate);
        chart.CustomPeriodValue = getValueFunc(custom.StartDate, current.EndDate);

        chart.CurrentSeriesName = dateToStringFunc(currentChart.StartDate);
        chart.PreviousSeriesName = dateToStringFunc(previousChart.StartDate);
        chart.SetChartData(
            getChartDataFunc(currentChart.StartDate, currentChart.EndDate).ToList(),
            getChartDataFunc(previousChart.StartDate, previousChart.EndDate).ToList()
        );
    }

    #region Get DateTime Ranges
    protected DateRange GetTodayRange() {
        return GetRange(DateTime.Now, SelectionInterval.Day);
    }
    protected DateRange GetYesterdayRange() {
        return GetRange(DateTime.Now.AddDays(-1), SelectionInterval.Day);
    }
    protected DateRange GetLastWeekRange() {
        return GetRange(DateTime.Now.AddDays(-7), DateTime.Now, SelectionInterval.Day);
    }
    protected DateRange GetChartCurrentRange(bool monthRange = false) {
        DateTime selectedDate = monthRange ? UnitSalesVerticalChart.GetSelectedDate() : DailySalesPerformanceVerticalChart.GetSelectedDate();
        return GetRange(selectedDate, monthRange ? SelectionInterval.Month : SelectionInterval.Day);
    }
    protected DateRange GetChartPreviousRange(bool monthRange = false) {
        DateTime dayBeforeSelected = monthRange ? UnitSalesVerticalChart.GetSelectedDate().AddMonths(-1) : DailySalesPerformanceVerticalChart.GetSelectedDate().AddDays(-1);
        return GetRange(dayBeforeSelected, monthRange ? SelectionInterval.Month : SelectionInterval.Day);
    }
    protected DateRange GetCurrentMonthRange() {
        return GetRange(DateTime.Now, SelectionInterval.Month);
    }
    protected DateRange GetLastMonthRange() {
        return GetRange(DateTime.Now.AddMonths(-1), SelectionInterval.Month);
    }
    protected DateRange GetYTDRange() {
        return new DateRange(new DateTime(DateTime.Now.Year, 1, 1), DateTimeHelper.GetIntervalEndDate(DateTime.Now, SelectionInterval.Month));
    }
    private DateRange GetRange(DateTime date, SelectionInterval interval) {
        return GetRange(date, date, interval);
    }
    private DateRange GetRange(DateTime startDate, DateTime endDate, SelectionInterval interval) {
        return new DateRange(DateTimeHelper.GetIntervalStartDate(startDate, interval), DateTimeHelper.GetIntervalEndDate(endDate, interval));
    }
    #endregion
}
