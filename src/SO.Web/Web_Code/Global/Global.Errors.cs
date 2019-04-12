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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;

using System.Web.SessionState;
using log4net.Appender;

using Errors;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace SO
{
    public partial class Global : System.Web.HttpApplication
    {


        #region Errors utils

        public static bool LogErrorsToTXT()
        {

            return Configuration.Logger.GetIsEnabledLoggerErrorsToTextApp_Data();
        }
        public static void Log_File_Write_Error(string sError)
        {
            var strFileName = "Err_dt_" + DateTime.Now.Month + "_" + DateTime.Now.Day
                                + "_" + DateTime.Now.Year + "_Time_" + DateTime.Now.Hour + "_" +
                                DateTime.Now.Minute + "_" + DateTime.Now.Second + "_"
                                + DateTime.Now.Millisecond + ".txt";

            strFileName = sDirLogs + strFileName;
            var fsOut = File.Create(strFileName);
            var sw = new StreamWriter(fsOut);

            sw.WriteLine(sError);
            sw.Flush();
            sw.Close();
            fsOut.Close();
        }
        #endregion Errors utils



    }
}
