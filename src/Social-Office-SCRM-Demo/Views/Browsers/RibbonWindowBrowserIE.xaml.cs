// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;

namespace SODemo.Views.Browsers
{

    // https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.webbrowser?view=netframework-4.7.2
    // http://justyouraveragegeek.com/blog/index.php/2010/03/comvisible-class-with-a-non-comvisible-baseclass/
    // https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/noncomvisiblebaseclass-mda

    /// <summary>
    /// Interaction logic for RibbonWindowBrowserIE.xaml
    /// </summary>    ///
    // Object used for communication from JS -> WPF
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public partial class RibbonWindowBrowserIE : DevExpress.Xpf.Core.ThemedWindow, IRibbonWindowBrowserIE
    {
        string jsToInject, jsToInjectName, _URL;


        public RibbonWindowBrowserIE(string URL)
        {
            InitializeComponent();

            ribbonControl.IsMinimized = true;


            _URL = URL;

            // can callback from js <a href="whatever" onclick="window.external.JSTest('called from script code')">CLICK ME</a>
            // class need to be public
            //((WebBrowser)sender).ObjectForScripting = this; // new Scripts();  // not working in external class, maybe need to make webbrower in code not xaml
            // webBrowser.ObjectForScripting = this;

            /*
#if DEBUG
            webBrowser.AllowNavigation = true;
            webBrowser.AllowWebBrowserDrop = true;
            webBrowser.IsWebBrowserContextMenuEnabled = true;
            webBrowser.ScriptErrorsSuppressed = false;
            webBrowser.ScrollBarsEnabled = true;
            webBrowser.WebBrowserShortcutsEnabled = true;
            //LoadUrl(Program.Configuration.Webserver.Webserver_Development);
#else
            webBrowser.AllowNavigation = true;
            webBrowser.AllowWebBrowserDrop = true;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.ScrollBarsEnabled = true;
            webBrowser.WebBrowserShortcutsEnabled = true;
            //LoadUrl(Program.Configuration.Webserver.Webserver_Production);
#endif
*/

            //SHDocVw.WebBrowser wbCOMmain = (SHDocVw.WebBrowser)webBrowser.ActiveXInstance;
            //wbCOMmain.NewWindow3 += wbCOMmain_NewWindow3;

            // https://stackoverflow.com/questions/6470842/how-do-i-display-a-popup-from-a-webbrowser-in-another-window-i-created
            //(webBrowser.ActiveXInstance as SHDocVw.WebBrowser).NewWindow3 += wbCOMmain_NewWindow3;

        }

        /*
                void wbCOMmain_NewWindow3(ref object ppDisp,
                          ref bool Cancel,
                          uint dwFlags,
                          string bstrUrlContext,
                          string bstrUrl)
        {
            // cancel the PopUp event  
            Cancel = true; // stop the navigation

            // send the popup URL to the WebBrowser control  
            //webBrowser.Navigate(URL);

            OpenNewBrowserTab("", bstrUrl, true, true);

        }
            */


        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.LogInfo("RibbonWindowBrowserIE loaded");

            //Create the JavascriptInterface object.
            JavascriptInterface objJavascriptInterface = new JavascriptInterface(this);

            //Set the JavascriptInterface object to the web browser control.
            this.webBrowser.ObjectForScripting = objJavascriptInterface;

            //jsToInjectName = "JSInvokeFromWPF";
            //jsToInject = "alert('HELLO JS FROM WPF!')";
            //LoadUrlInjectJS("about:blank", jsToInjectName, jsToInject);

            if (!string.IsNullOrEmpty(_URL))
            {
                LoadUrl(_URL);
            }
            else
            {
                LoadUrl("about:blank");
            }
        }

        public void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                webBrowser.Navigate(url);

            }
        }

        public void LoadUrlInjectJS(string url, string sJSToInjectName, string sJSToInject)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                jsToInject = sJSToInject;
                jsToInjectName = sJSToInjectName;
                webBrowser.Navigate(url);

            }
        }


        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.navigation.navigationeventargs?view=netframework-4.7.2

            //urlTextBox.Text = webBrowser.Url.ToString();
            //toolStripStatusLabel1.Text = webBrowser.Document.Title;

            // Check if page is fully loaded or not
            if (e.Uri.AbsolutePath != (sender as System.Windows.Controls.WebBrowser).Source.AbsolutePath)
            {
                return;
            }

            //TODO: WebResponse.Headers check status 200, page loaded ok. 

            // Check page is finished loading 
            //if (this.webBrowser. ReadyState != WebBrowserReadyState.Complete) // not in wpf need check solution
            if (false)
            {
                return;
            }
            else
            {


                //Action to be taken on page loading completion

                // scraping data
                /*
                if (this.IsMdiChild)
                {

                    if (_ParentFormClass.Equals((int)FormMDIClass.MainForm))
                    {

                    }

                    if (_ParentFormClass.Equals((int)FormMDIClass.FacebookForm))
                    {
                        // update userID
                        if (webBrowser.Document.Url.ToString().Equals("https://www.facebook.com/"))
                        {
                            SetCurrentFBUserID();
                        }

                    }

                }
                */

                if (string.IsNullOrEmpty(this.Title))
                {
                    this.Title = (string)webBrowser.InvokeScript("eval", "document.title.toString()");
                }

                //TODO: show favicon as form icon
                //https://stackoverflow.com/questions/2593377/how-can-i-get-a-websites-favicon-when-using-the-webbrowser-control


                #region inject scripts
                // https://stackoverflow.com/questions/153748/how-to-inject-javascript-in-webbrowser-control

                if (!string.IsNullOrEmpty(jsToInject) && !string.IsNullOrEmpty(jsToInjectName))
                {

                    webBrowser.InvokeScript("eval", new object[] { jsToInject });
#if DEBUG
                    App.LogDebug("Injected script '" + jsToInjectName + "' to: " + webBrowser.Source.AbsoluteUri.ToString() + Environment.NewLine + jsToInject.Substring(0, 1000) + Environment.NewLine + "...");
#endif
                    jsToInject = null;
                    jsToInjectName = null;
                }



                #endregion inject scripts
            }
        }


        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void InjectDocumentJS(string jsName, string jsInject)
        {

            if (!string.IsNullOrEmpty(jsName) && !string.IsNullOrEmpty(jsInject))
            {

                webBrowser.InvokeScript("eval", new object[] { jsInject });
#if DEBUG
                App.LogDebug("Injected script '" + jsName + "' to: " + webBrowser.Source.AbsoluteUri.ToString() + Environment.NewLine + jsInject.Substring(0, 1000) + Environment.NewLine + "...");
#endif
            }

        }




    } // end class


    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class JavascriptInterface
    {
        // http://sekhartechblog.blogspot.com/2012/04/webbrowser-javascript-communication-in.html

        //MainWindow object.
        RibbonWindowBrowserIE objMainWindow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mainWindow">MainWindow object.</param>
        public JavascriptInterface(RibbonWindowBrowserIE mainWindow)
        {
            //Get the MainWindow object.
            this.objMainWindow = mainWindow;
        }

        public void JSInvokeTest(string msg)
        {

            MessageBox.Show("Message from javascript: " + msg);

        }



        public void OpenNewBrowserTab(string tabname, string url, bool showCloseButton, bool showPinButton)
        {

            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {

                App.NewBrowserWindowIE(url);
            }


        }


        public void OpenNewBrowserTabInjectJS(string tabname, string url, bool showCloseButton, bool showPinButton, string sJSToInjectName, string sJSToInject)
        {

            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                /*
                browser1.Text = tabname;
                // browser1.ControlBox = false; // if disable cant close tab
                browser1.Show();

                browser1.splashScreenManager.ShowWaitForm();
                browser1.LoadUrlInjectJS(url, sJSToInjectName, sJSToInject);
                browser1.splashScreenManager.CloseWaitForm();
                */
            }

        }


    }
}
