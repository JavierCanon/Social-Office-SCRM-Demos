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


        #region User Settings


        public class Users
        {

            /// <summary>
            /// Get a user setting stored in DB or Cache
            /// </summary>
            /// <param name="sname">Name of setting</param>
            /// <returns></returns>
            public static string GetUserSetting(string suserID, string sname)
            {

                string key = "UserSetting_" + sname;

                // in cache?
                if (HttpRuntime.Cache[key] == null)
                {
                    string tsql = "EXEC [User_Settings_GetValue_ByName] " + suserID + ", '" + sname + "'";
                    return "";// Softcanon.DAL.SqlApiSqlClient.GetStringRecordValue(tsql, Global.DAL.GetConnectionStringDBMain());

                }
                else
                {
                    return HttpRuntime.Cache[key].ToString();

                }

            }

            /// <summary>
            /// Set or create a new user setting in DB
            /// </summary>
            public static void SetUserSetting(string suserID, string sname, string svalue)
            {
                // save
                string tsql = "EXEC [User_Settings_Update_Value]  " + suserID + ", '" + sname + "', '" + svalue + "'";
                //Softcanon.DAL.SqlApiSqlClient.ExecuteSqlString(tsql, Global.DAL.GetConnectionStringDBMain());

                // store in cache
                string key = "UserSetting_" + sname;
                HttpRuntime.Cache.Insert(key, svalue, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));

            }

        }
        #endregion User Settings





    }
}
