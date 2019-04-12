using System;
using System.Web.UI.HtmlControls;
using DataAccess;

public partial class DateSelectorControl : UserControlBase {
    private const string CurrentDateKey = "CurrentDateKey";

    public SelectionInterval SelectionInterval { get; set; }
    public string CallbackPanelId { get; set; }
    public string Text { get { return CurrentDate.ToString(GetFormatString()).ToUpper(); } }
    public DateTime CurrentDate {
        get {
            if(!DateSelectorHiddenField.Contains(CurrentDateKey))
                DateSelectorHiddenField.Add(CurrentDateKey, DateTime.Now);
            return (DateTime)DateSelectorHiddenField[CurrentDateKey];
        }
        set { DateSelectorHiddenField[CurrentDateKey] = value; }
    }


    protected void Page_Load(object sender, EventArgs e) {
        PrepareButton(PrevButton, IsPrevButtonEnabled(), -1, "prevButton");
        PrepareButton(NextButton, IsNextButtonEnabled(), 1, "nextButton");
    }
    private void PrepareButton(HtmlGenericControl button, bool isEnabled, int arg, string className) {
        button.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
        if(isEnabled) {
            button.Attributes["onclick"] = string.Format("CallbackHelper.UpdateContent({0}, {1}, {2})", CallbackPanelId, arg, button.ClientID);
            button.Attributes["class"] = className + " enabledElement";
        } else {
            button.Attributes.Remove("onclick");
            button.Attributes["class"] = className + " disabledElement";
        }
    }
    private string GetFormatString() {
        switch(SelectionInterval) {
            case SelectionInterval.Day:
                return "MMM dd, yyyy";
            case SelectionInterval.Month:
                return "MMMM, yyyy";
            default:
                return "";
        }
    }

    private bool IsPrevButtonEnabled() {
        DateTime minDate = SalesProvider.GetMinDate();
        DateTime currentDate = CurrentDate;
        minDate = DateTimeHelper.GetIntervalStartDate(minDate, SelectionInterval);
        currentDate = DateTimeHelper.GetIntervalStartDate(CurrentDate, SelectionInterval);
        return minDate < currentDate;
    }
    private bool IsNextButtonEnabled() {
        DateTime maxDate = SalesProvider.GetMaxDate();
        DateTime currentDate = CurrentDate;
        maxDate = DateTimeHelper.GetIntervalEndDate(maxDate, SelectionInterval);
        currentDate = DateTimeHelper.GetIntervalEndDate(CurrentDate, SelectionInterval);
        return maxDate > currentDate;
    }

    public void ChangeDate(int delta) {
        if((delta > 0 && IsNextButtonEnabled()) || (delta < 0 && IsPrevButtonEnabled())) {
            switch(SelectionInterval) {
                case SelectionInterval.Month:
                    CurrentDate = CurrentDate.AddMonths(delta);
                    break;
                case SelectionInterval.Day:
                    CurrentDate = CurrentDate.AddDays(delta);
                    break;
            }
        }
        PrepareButton(PrevButton, IsPrevButtonEnabled(), -1, "prevButton");
        PrepareButton(NextButton, IsNextButtonEnabled(), 1, "nextButton");
    }

}
