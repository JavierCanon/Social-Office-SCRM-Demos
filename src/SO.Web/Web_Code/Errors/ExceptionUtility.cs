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
using System.IO;
using System.Web;
using SO;

namespace Errors
{
	public sealed class ExceptionUtility
	{
		private ExceptionUtility()
		{
		}

		public static void LogException(Exception exc, string source)
		{

            if (!Global.LogErrorsToTXT()) return;


			var logFile = HttpContext.Current.Server.MapPath("~")
				+ "\\App_Data\\Logs\\Error_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + ".txt";

			var sw = new StreamWriter(logFile, true);
			sw.WriteLine("********** {0}, {1} **********", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
			if (exc.InnerException != null)
			{
				sw.Write("Inner Exception Type: ");
				sw.WriteLine(exc.InnerException.GetType().ToString());
				sw.Write("Inner Exception: ");
				sw.WriteLine(exc.InnerException.Message);
				sw.Write("Inner Source: ");
				sw.WriteLine(exc.InnerException.Source);
				if (exc.InnerException.StackTrace != null)
				{
					sw.WriteLine("Inner Stack Trace: ");
					sw.WriteLine(exc.InnerException.StackTrace);
				}
			}
			sw.Write("Exception Type: ");
			sw.WriteLine(exc.GetType().ToString());
			sw.WriteLine("Exception: " + exc.Message);
			sw.WriteLine("Source: " + source);
			sw.WriteLine("Stack Trace: ");
			if (exc.StackTrace != null)
			{
				sw.WriteLine(exc.StackTrace);
				sw.WriteLine();
			}
			sw.Flush();
			sw.Close();
		}

		public static void NotifySystemOps(Exception exc)
		{
            // send email?.

		}
	}
}
