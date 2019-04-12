using System;
using DataAccess;
using DevExpress.Web;

public partial class RangeControl : UserControlBase, IRangeControl {
    const string ChartUrlPattern = "~/Chart.aspx?w=1141px&h=50px&start={0:s}&end={1:s}",
                 CurrentYearSessionKey = "CurrentYear";

    protected DateTime MinDateTime { get; set; }
    protected DateTime MaxDateTime { get; set; }
    protected int CurrentYear {
        get {
            if(Session[CurrentYearSessionKey] == null)
                Session[CurrentYearSessionKey] = DateTime.Now.Year;
            return (int)Session[CurrentYearSessionKey];
        }
        set { Session[CurrentYearSessionKey] = value; }
    }

    public DateTime GetStartDate() {
        int selectedMonth = NormalizeMonth((int)SalesDateRange.PositionStart + 1);
        return DateTimeHelper.GetIntervalStartDate(new DateTime(CurrentYear, selectedMonth, 1), SelectionInterval.Month);
    }
    public DateTime GetEndDate() {
        int selectedMonth = NormalizeMonth((int)SalesDateRange.PositionEnd + 1);
        return DateTimeHelper.GetIntervalEndDate(new DateTime(CurrentYear, selectedMonth, 1), SelectionInterval.Month);
    }
    int NormalizeMonth(int month) {
        return Math.Max(Math.Min(month, 12), 1);
    }

    protected void Page_Init(object sender, EventArgs e) {
        UpdateMinMaxDate();
        PopulateTrackBarItems();
        if(!IsPostBack)
            InitializePositions();
    }
    protected void Page_Load(object sender, EventArgs e) {
        PrepareControl();
    }

    private string GetBackgroundChartImageUrl() {
        return string.Format(ChartUrlPattern, DateTimeHelper.GetIntervalStartDate(new DateTime(CurrentYear, 1, 1), SelectionInterval.Month),
                                              DateTimeHelper.GetIntervalEndDate(new DateTime(CurrentYear, GetLastPossibleMonth(), 1), SelectionInterval.Month));
    }
    private int GetLastPossibleMonth() {
        if(CurrentYear == DateTime.Now.Year)
            return DateTime.Now.Month;
        return 12;
    }
    private void UpdateMinMaxDate() {
        MinDateTime = DateTimeHelper.GetIntervalStartDate(SalesProvider.GetMinDate(), SelectionInterval.Month);
        MaxDateTime = DateTimeHelper.GetIntervalEndDate(SalesProvider.GetMaxDate(), SelectionInterval.Month);
    }
    private void PopulateTrackBarItems() {
        SalesDateRange.Items.Clear();
        DateTime startDate = new DateTime(CurrentYear, 1, 1);
        DateTime endDate = new DateTime(CurrentYear, 12, 1);
        if(endDate > MaxDateTime)
            endDate = MaxDateTime;
        if(startDate < MinDateTime)
            startDate = MinDateTime;

        DateTime tempDate = startDate;
        while(tempDate <= endDate) {
            string mask =
                DateTimeHelper.HasSameYearAndMonth(tempDate, startDate) || DateTimeHelper.HasSameYearAndMonth(tempDate, endDate) ? "MMM yyyy" : "MMM";
            SalesDateRange.Items.Add(tempDate.ToString(mask), tempDate.Month - 1);
            tempDate = tempDate.AddMonths(1);
        }
    }
    private void PrepareControl() {
        LeftShiftButton.Enabled = MinDateTime.Year < CurrentYear;
        RightShiftButton.Enabled = MaxDateTime.Year > CurrentYear;
        BackgroundChartImage.ImageUrl = GetBackgroundChartImageUrl();
    }
    protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e) {
        int deltaYear;
        if(Int32.TryParse(e.Parameter, out deltaYear)) {
            int newCurrentYear = CurrentYear + deltaYear;
            if(newCurrentYear < MinDateTime.Year)
                newCurrentYear = MinDateTime.Year;
            if(newCurrentYear > MaxDateTime.Year)
                newCurrentYear = MaxDateTime.Year;
            CurrentYear = newCurrentYear;
            PopulateTrackBarItems();
            PrepareControl();
            InitializePositions();
        }
    }
    private void InitializePositions() {
        SalesDateRange.PositionEnd = SalesDateRange.Items.Count - 1;
        SalesDateRange.PositionStart = 0;
    }
}
