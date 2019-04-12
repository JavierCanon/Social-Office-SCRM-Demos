// Global Events
function OnControlsInitialized(s, e) {
    ASPx.AnimationConstants.Durations.DEFAULT = 250;
}

// The code is taken from the CreateDatabaseControl.ascx file of ASPxGridView demos
var createTimer = -1;
var progressTimer = -1;
function OnCreateDbButtonClick(s, e) {
    CreateDbMessage.SetText("Creating Database...");
    CreateDbButton.SetVisible(false);
    CreateDbProgress.SetVisible(true);
    CreateDbCallback.PerformCallback("create");
    window.setInterval(function () { CreateDbCallback.PerformCallback("progress") }, 1000);
}
function OnCreateDbCallbackCallbackComplete(s, e) {
    if (e.parameter == "create") {
        window.clearTimeout(createTimer);
        if (eval(e.result)) {
            window.clearInterval(progressTimer);
            document.location.reload();
        }
    } else if (e.parameter == "progress") {
        var value = eval(e.result);
        if (value == -1) // created
            document.location.reload();
        CreateDbProgress.SetPosition(value);
    }
}
function OnCreateDbCallbackCallbackError(s, e) {
    e.handled = true;
    window.clearInterval(progressTimer);
    alert(e.message);
    document.location.reload();
}

// CallbackHelper
var CallbackHelper = {
    CallbackControlQueue: [],
    CurrentCallbackControl: null,
    UpdateContent: function (callbackControl, args, sender) {
        if (!this.CurrentCallbackControl) {
            this.CurrentCallbackControl = callbackControl;
            callbackControl.EndCallback.RemoveHandler(this.OnEndCallback);
            callbackControl.EndCallback.AddHandler(this.OnEndCallback);
            callbackControl.PerformCallback(args);
        } else
            this.PlaceInQueue(callbackControl, args, this.GetSenderId(sender));
    },
    GetSenderId: function (senderObject) {
        if (senderObject.constructor === String)
            return senderObject;
        return senderObject.name || senderObject.id;
    },
    PlaceInQueue: function (callbackControl, args, sender) {
        var queue = this.CallbackControlQueue;
        for (var i = 0; i < queue.length; i++) {
            if (queue[i].control == callbackControl && queue[i].sender == sender) {
                queue[i].args = args;
                return;
            }
        }
        queue.push({ control: callbackControl, args: args, sender: sender });
    },
    OnEndCallback: function (sender) {
        sender.EndCallback.RemoveHandler(CallbackHelper.OnEndCallback);
        CallbackHelper.CurrentCallbackControl = null;
        var queuedPanel = CallbackHelper.CallbackControlQueue.shift();
        if (queuedPanel)
            CallbackHelper.UpdateContent(queuedPanel.control, queuedPanel.args, queuedPanel.sender);
    }
};

// Footer Range Bar
function OnDataDependentControlInit(s, e) {
    DataDependentControlHelper.AddControl(s);
}
var DataDependentControlHelper = {
    controls: [],
    AddControl: function (control) {
        for (var i = 0; i < this.controls.length; i++) {
            if (this.controls[i] == control)
                return;
        }
        this.controls.push(control);
    },
    UpdateControls: function (sender) {
        for (var i = 0; i < this.controls.length; i++)
            CallbackHelper.UpdateContent(this.controls[i], null, sender);
    }
}
var RangeControlHelper = {
    inTracking: false,
    currentPositionStart: 0,
    currentPositionEnd: 0,
    trackBar: null,
    OnTrackBarInit: function (s) {
        RangeControlHelper.InitTrackBar(s);
    },
    InitTrackBar: function (trackBar) {
        this.trackBar = trackBar;
        this.currentPositionStart = trackBar.GetPositionStart();
        this.currentPositionEnd = trackBar.GetPositionEnd();
        trackBar.TrackStart.AddHandler(this.OnTrackBarTrackStart, this);
        trackBar.TrackEnd.AddHandler(this.OnTrackBarTrackEnd, this);
        trackBar.ValueChanged.AddHandler(this.OnTrackBarValueChanged, this);
    },
    OnTrackBarTrackStart: function () {
        this.inTracking = true;
    },
    OnTrackBarTrackEnd: function () {
        this.inTracking = false;
        this.OnTrackBarValueChanged();
    },
    OnTrackBarValueChanged: function () {
        if (!this.inTracking && this.IsRangeChanged()) {
            this.currentPositionStart = this.trackBar.GetPositionStart();
            this.currentPositionEnd = this.trackBar.GetPositionEnd();
            DataDependentControlHelper.UpdateControls(this.trackBar);
        }
    },
    IsRangeChanged: function () {
        return (this.trackBar.GetPositionStart() != this.currentPositionStart) || (this.trackBar.GetPositionEnd() != this.currentPositionEnd);
    },
    ChangeRangeYear: function (deltaYear) {
        CallbackHelper.UpdateContent(RangeControlCallbackPanel, deltaYear, this.trackBar);
        DataDependentControlHelper.UpdateControls(this.trackBar);
    }
}

// Header Menu
function OnMenuItemClick(menu, args) {
    if (args.item.name == "helpMenuItem")
        HelpMenuPopup.Show();
}
function RedirectToTrialPage(s) {
    var win = window.open(s.cpTrialUrl, '_blank');
    if (win)
        win.focus();
}
function HidePopup() {
    HelpMenuPopup.Hide();
}

// Map 
function CreateMap(lat, long) {
    var mapOptions = {
        credentials: "AhLIqqo1YVxwfwBG0zjwdZsAl4ZwkAXR-n9WZwSP-79hDUr1PFbPxzpTDnJ88DpE",
        center: new Microsoft.Maps.Location(lat, long),
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: 9,
        showScalebar: true,
        showMapTypeSelector: true,
        disableKeyboardInput: true
    }
    map = new Microsoft.Maps.Map(document.getElementById('mapHolder'), mapOptions);
    var center = map.getCenter();
    var pin = new Microsoft.Maps.Pushpin(center); 
    map.entities.push(pin);
    CustomersGridView.Focus();
}