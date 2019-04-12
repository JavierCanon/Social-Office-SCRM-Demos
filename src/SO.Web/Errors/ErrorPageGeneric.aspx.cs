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
using Errors;

namespace Errors
{
    public partial class ErrorPageGeneric : System.Web.UI.Page
    {
        protected Exception ex = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            


            var ex = Server.GetLastError();
            if (ex == null)
                return;

            var safeMsg = "A problem has occurred in the web site. ";

            if (ex.InnerException != null)
            {
                innerTrace.Text = ex.InnerException.StackTrace;
                InnerErrorPanel.Visible = Request.IsLocal;
                innerMessage.Text = ex.InnerException.Message;
            }
            if (Request.IsLocal)
            {
                exTrace.Visible = true;
            }
            else
            {
                ex = new Exception(safeMsg, ex);
            }
            exMessage.Text = ex.Message;
            exTrace.Text = ex.StackTrace;

            ExceptionUtility.LogException(ex, "Generic Error Page");
            ExceptionUtility.NotifySystemOps(ex);

            Server.ClearError();
        }
    }
}
