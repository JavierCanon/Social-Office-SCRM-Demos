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


        #region Application Configuration

        void SyncSOAPPandDBSettings(string dbConnection)
        {

            var config = ConfigurationManager.AppSettings;
            string tsql = "", setting, value;

            foreach (string key in config.AllKeys)
            {

                setting = key;
                value = config[key].ToString();


                SqlParameter[] paramsToSP = {
                 new SqlParameter(){ ParameterName= "@Name", DbType= DbType.AnsiString, Size= 60, Value= setting }
                ,new SqlParameter(){ ParameterName= "@Value", DbType= DbType.AnsiString, Size= 240, Value= value }
                 };

                tsql = string.Format("EXEC [Settings_Update_Value] '{0}', '{1}'", setting, value) + Environment.NewLine;

                if (Configuration.Development.GetIsEnabledDeveloperMode())
                {
                    LogDebug(null, EnumLogCategories.CONFIGURATION, "Sync Config >>> " + Environment.NewLine + tsql);
                }

                // need to use parameters becuase use or ilegal chars in tsql in config values.
                //Softcanon.DAL.SqlApiSqlClient.ExecuteSqlString("EXEC [Settings_Update_Value] @Name, @Value ;", paramsToSP, dbConnection);


            }





            // is custom?
            /*
            if (Configuration.Application.GetInstallationIsCustom())
            {

                string folder = "";
                folder = Configuration.GetInstallationCustomFolder();
                tsql = "";

                if (!string.IsNullOrEmpty(folder))
                {

                    var webconfig = WebConfigurationManager.OpenWebConfiguration("~/Sites/" + folder + "/").AppSettings;
                    foreach (string key in webconfig.Settings.AllKeys)
                    {
                        setting = key;
                        value = webconfig.Settings[key].Value.ToString();

                        
                        SqlParameter[] paramsToSP2 = {
                         new SqlParameter(){ ParameterName= "@Name", DbType= DbType.AnsiString, Size= 60, Value= setting }
                        ,new SqlParameter(){ ParameterName= "@Value", DbType= DbType.AnsiString, Size= 240, Value= value }
                             };

                        tsql = string.Format("EXEC [Settings_Update_Value] @Name, @Value");
                                               
                        Softcanon.DAL.SqlApiSqlClient.ExecuteSqlString(tsql, paramsToSP2, dbConnection);
                        

                        tsql += string.Format("EXEC [Settings_Update_Value] '[0]', '[1]'", setting, value) + Environment.NewLine;
                    
                    }

                    if (Configuration.Development.GetIsEnabledDeveloperMode())
                    {
                        LogDebug(null, EnumLogCategories.CONFIGURATION, "Sync Config >>> " + folder  + Environment.NewLine + tsql);
                    }

                    Softcanon.DAL.SqlApiSqlClient.ExecuteSqlString(tsql, dbConnection);

                }
                

            }*/




        }
        #endregion Application Configuration

        #region Optimization
        /// <summary>
        /// use minification javascript file version
        /// </summary>
        /// <returns></returns>
        public static string UseMinJS()
        {
            if (IsDebugJs)
                return ".min";
            else
                return "";

        }
        /// <summary>
        /// use minification css styleshet file version
        /// </summary>
        /// <returns></returns>
        public static string UseMinCSS()
        {
            if (IsDebugCSS)
                return ".min";
            else
                return "";

        }
        #endregion Optimization



        #region Gets info



        public static string GetAppName()
        {
            return _AppName;

        }


        public static string GetCredits()
        {
            if (!string.IsNullOrEmpty(_AppCredits)) return _AppCredits;

            _AppCredits = "Softcanon <a href=\"http://www.softcanon.com/gestaventa/\" target=\"_blank\">SO</a> Ver.[" + Global.Versions.GetAppVersion() + "] &copy; 2013 - " + System.DateTime.Now.Year.ToString() + " <a href=\"http://www.softcanon.com/\" target=\"_blank\">Softcanon</a>";

            return _AppCredits;
        }


        public static string GetImageLogo()
        {

            return Global.Utils.Webpath.GetRequestApplicationUrlPath() + "Content/Images/LogoSO.png";

        }


        #endregion  Gets info



    }
}
