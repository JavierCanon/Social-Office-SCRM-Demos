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
using SODemo.Views.Browsers;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;


/*
long stasks:
https://tech.labs.oliverwyman.com/blog/2015/02/24/asynchronous-interaction-between-a-webbrowser-control-and-the-hosted-web-page/

*/
namespace SODemo.Presenters.Browsers
{

    public class RibbonWindowBrowserIEPresenter
    {
        IRibbonWindowBrowserIE view;
        
        public RibbonWindowBrowserIEPresenter(IRibbonWindowBrowserIE ribbonWindowBrowserIEView) {

            view = ribbonWindowBrowserIEView;
        }

                     
        /*
         void Ini(FormBrowser form)
        {

#if DEBUG
            form.webBrowser.AllowNavigation = true;
            form.webBrowser.AllowWebBrowserDrop = true;
            form.webBrowser.IsWebBrowserContextMenuEnabled = true;
            form.webBrowser.ScriptErrorsSuppressed = false;
            form.webBrowser.ScrollBarsEnabled = true;
            form.webBrowser.WebBrowserShortcutsEnabled = true;
            //form.LoadUrl(Program.Configuration.Webserver.Webserver_Development);
#else
            form.webBrowser.AllowNavigation = true;
            form.webBrowser.AllowWebBrowserDrop = true;
            form.webBrowser.IsWebBrowserContextMenuEnabled = false;
            form.webBrowser.ScriptErrorsSuppressed = true;
            form.webBrowser.ScrollBarsEnabled = true;
            form.webBrowser.WebBrowserShortcutsEnabled = true;
            //form.LoadUrl(Program.Configuration.Webserver.Webserver_Production);
#endif
        }


        void Ini(FormTabBrowser form)
        {

#if DEBUG
            form.webBrowser.AllowNavigation = true;
            form.webBrowser.AllowWebBrowserDrop = true;
            form.webBrowser.IsWebBrowserContextMenuEnabled = true;
            form.webBrowser.ScriptErrorsSuppressed = false;
            form.webBrowser.ScrollBarsEnabled = true;
            form.webBrowser.WebBrowserShortcutsEnabled = true;
            //form.LoadUrl(Program.Configuration.Webserver.Webserver_Development);
#else
            form.webBrowser.AllowNavigation = true;
            form.webBrowser.AllowWebBrowserDrop = true;
            form.webBrowser.IsWebBrowserContextMenuEnabled = false;
            form.webBrowser.ScriptErrorsSuppressed = true;
            form.webBrowser.ScrollBarsEnabled = true;
            form.webBrowser.WebBrowserShortcutsEnabled = true;
            //form.LoadUrl(Program.Configuration.Webserver.Webserver_Production);
#endif
        }
        */



    }
}
