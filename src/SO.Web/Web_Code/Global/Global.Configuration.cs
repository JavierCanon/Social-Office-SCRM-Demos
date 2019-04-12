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
using System.Configuration;

namespace SO
{
    public partial class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Clase para retornar facilmente atributos del aplicativo.
        /// </summary>
        public static class Configuration
        {


            /// <summary>
            /// App config
            /// </summary>
            public static class Application
            {

                /// <summary>
                /// Widgets config
                /// </summary>
                public static class Widgets
                {


                    /// <summary>
                    /// Widgets. 
                    /// </summary>
                    /// <returns></returns>
                    public static string GetWidgetNewsInsternational_RSS_URL()
                    {
                        return ConfigurationManager.AppSettings["SO:Widgets.RSS.News.International.URL"].ToString();


                    }

                    /// <summary>
                    /// Widgets.               
                    /// </summary>
                    /// <returns></returns>
                    public static string GetWidgetNewsSO_RSS_URL()
                    {
                        return ConfigurationManager.AppSettings["SO:Widgets.RSS.News.SO.URL"].ToString();


                    }


                }




                public static bool GetIsEnabledDemoMode()
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["Application:Demo.DemoModeEnabled"].ToString());

                }



                /// <summary>
                /// Validate browser version
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledBrowserValidation()
                {
                    string config = ConfigurationManager.AppSettings["Application:Browsers.EnableBrowserValidation"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// return if the app run in wan with a domain name
                /// </summary>
                /// <returns></returns>
                public static bool GetInstallationIsInWanWithInternetDomainName()
                {
                    string config = ConfigurationManager.AppSettings["Installation:IsInWanWithInternetDomainName"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }


                /// <summary>
                /// return if the app run in lan without a domain name
                /// </summary>
                /// <returns></returns>
                public static bool GetInstallationIsInLanOrLocal()
                {
                    string config = ConfigurationManager.AppSettings["Installation:IsInLanOrLocal"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// return if the app run in lan without a domain name
                /// </summary>
                /// <returns></returns>
                public static bool GetInstallationIsCustom()
                {
                    string config = ConfigurationManager.AppSettings["Installation:IsCustom"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetInstallationCustomFolder()
                {
                    string config = ConfigurationManager.AppSettings["Installation:Custom.Folder"].ToString();

                    if (null != config)
                    {
                        return config;
                    }
                    else
                    {
                        return null;
                    }
                }


            } //<<< aplication

            /// <summary>
            /// 
            /// </summary>
            public static class Optimization
            {

                /// <summary>
                /// return if use .min.js file version
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledOptimizationJavascriptUseMinifiedFiles()
                {
                    string config = ConfigurationManager.AppSettings["Optimization:Javascript.UseMinifiedFiles"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// return if use .min.css file version
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledOptimizationCSSUseMinifiedFiles()
                {
                    string config = ConfigurationManager.AppSettings["Optimization:CSS.UseMinifiedFiles"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledOptimizationBundling()
                {
                    string config = ConfigurationManager.AppSettings["Optimization:Bundling.Enabled"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }


            } //<<< optimization


            public static class Cache
            {

                /// <summary>
                /// Seconds to cache the page.
                /// </summary>
                /// <returns></returns>
                public static int GetCacheWeb4CMSPagesCacheDuration()
                {
                    string config = ConfigurationManager.AppSettings["Cache:Web4CMS.Pages.CacheDuration"].ToString();

                    if (null != config)
                    {
                        return Convert.ToInt32(config);
                    }
                    else
                    {
                        return 10;
                    }
                }

            }

            /// <summary>
            /// Dev settings
            /// </summary>
            public static class Development
            {


                /// <summary>
                /// custom folder to use without use autodetect domain name
                /// </summary>
                /// <returns></returns>
                public static string GetDevelopmentCustomFolder()
                {
                    return ConfigurationManager.AppSettings["Development:Custom.Folder"].ToString();


                }


                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledDebugDeveloperModeShowGenericPageErrors()
                {
                    string config = ConfigurationManager.AppSettings["Development:DeveloperMode.ShowGenericPageErrors"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledDebugDeveloperModeShowGlobalPageError()
                {
                    string config = ConfigurationManager.AppSettings["Development:DeveloperMode.ShowGlobalPageError"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// retorna si esta en modo desarrollo, es decir se saltan muchas opciones de seguridad y
                /// se asigan variables y parametros del aplicativo manualmente.
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledDeveloperMode()
                {
                    string config = ConfigurationManager.AppSettings["Development:DeveloperMode.Enabled"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }


                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetDebugWebsiteDomainName()
                {
                    return ConfigurationManager.AppSettings["Debug:Website.Domain.Name"].ToString();


                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetDebugWebsiteDomainGUID()
                {
                    return ConfigurationManager.AppSettings["Debug:Website.Domain.GUID"].ToString();


                }


                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetDebugWebsiteDirectory()
                {
                    return ConfigurationManager.AppSettings["Debug:Website.Directory"].ToString();


                }





            } //<<< development




            /// <summary>
            /// log settings
            /// </summary>
            public static class Logger
            {

                /// <summary>
                /// retorna si esta activo el logger
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledCacheLogger()
                {
                    string sString = null;
                    string skey = "Logger:Cache.Enable";

                    sString = ConfigurationManager.AppSettings[skey].ToString();

                    if (null != sString)
                    {
                        return Convert.ToBoolean(sString);
                    }
                    else
                    {
                        return false;
                    }
                }



                /// <summary>
                /// return the type of logger to file or to db.
                /// </summary>
                /// <returns></returns>
                public static int GetLoggerToTypeMode()
                {
                    string config = ConfigurationManager.AppSettings["Logger:ToTypeMode"].ToString();

                    if (null != config)
                    {
                        return Convert.ToInt32(config);
                    }
                    else
                    {
                        return 1;
                    }
                }

                /// <summary>
                /// return the type of logger to file or to db.
                /// </summary>
                /// <returns></returns>
                public static int GetUserLoggerToTypeMode()
                {
                    string config = ConfigurationManager.AppSettings["Logger:User.ToTypeMode"].ToString();

                    if (null != config)
                    {
                        return Convert.ToInt32(config);
                    }
                    else
                    {
                        return 1;
                    }
                }


                /// <summary>
                /// retorna si esta activo el logger
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledLogger()
                {
                    string config = ConfigurationManager.AppSettings["Logger:Enable"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// retorna si esta activo el logger
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledUserLogger()
                {
                    string config = ConfigurationManager.AppSettings["Logger:User.Enable"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }


                /// <summary>
                /// retorna si esta activo el logger
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledDeveloperLogger()
                {
                    string config = ConfigurationManager.AppSettings["Logger:Developer.Enable"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static bool GetIsEnabledLoggerErrorsToTextApp_Data()
                {
                    string config = ConfigurationManager.AppSettings["Logger:Errors.ToText.App_Data.Enable"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }
                }


                /// <summary>
                /// Buffer size for log output messages.
                /// </summary>
                /// <returns></returns>
                public static int GetLoggerProductionBufferSize()
                {
                    string config = ConfigurationManager.AppSettings["Logger:ProductionBufferSize"].ToString();

                    if (null != config)
                    {
                        return Convert.ToInt32(config);
                    }
                    else
                    {
                        return 100;
                    }
                }


            } // <<< class logger

            /// <summary>
            /// 
            /// </summary>
            public static class Security
            {
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetMD5Key()
                {
                    return ConfigurationManager.AppSettings["Security.MD5.Key"].ToString();

                }
            } //<<< Security

            /// <summary>
            /// 
            /// </summary>
            public static class Mail
            {

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetMailServer()
                {
                    return ConfigurationManager.AppSettings["MailServer:Host"].ToString();


                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetMailServerLogin()
                {
                    return ConfigurationManager.AppSettings["MailServer:Login"].ToString();

                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetMailServerPassword()
                {
                    return ConfigurationManager.AppSettings["MailServer:Password"].ToString();

                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static int GetMailServerPort()
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MailServer:Port"].ToString());


                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static bool GetMailServerIsEnableSSL()
                {
                    string config = ConfigurationManager.AppSettings["MailServer:SSL.Enable"].ToString();

                    if (null != config)
                    {
                        return Convert.ToBoolean(config);
                    }
                    else
                    {
                        return false;
                    }


                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetEmailSoporte()
                {
                    return ConfigurationManager.AppSettings["Emails:Soporte"].ToString();


                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetEmailContacto()
                {
                    return ConfigurationManager.AppSettings["Emails:Contacto"].ToString();


                }


            } // <<< mail

            /// <summary>
            /// 
            /// </summary>
            public static class DB
            {


                public static bool GetUseLocalDB()
                {

                    return Convert.ToBoolean(ConfigurationManager.AppSettings["Databases:UseLocalDB"].ToString());

                }


                /// <summary>
                /// retorna la cadena de conexion principal
                /// </summary>
                /// <returns></returns>
                public static string GetConnectionStringDBMain()
                {
                    if (Development.GetIsEnabledDeveloperMode())
                    {
                        return ConfigurationManager.ConnectionStrings["Development.MsSqlServer.Main"].ConnectionString;
                    }
                                       
                    if (GetUseLocalDB())
                    {
                        return ConfigurationManager.ConnectionStrings["LocalDB.MsSqlServer.Main"].ConnectionString;
                    }

                    if (Application.GetIsEnabledDemoMode())
                    {
                        return ConfigurationManager.ConnectionStrings["Demo.LocalDB.MsSqlServer.Main"].ConnectionString;
                    }


                    return ConfigurationManager.ConnectionStrings["Server.MsSqlServer.Main"].ConnectionString;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetConnectionStringDBLogServer()
                {

                    if (Development.GetIsEnabledDeveloperMode())
                    {
                        return ConfigurationManager.ConnectionStrings["Development.MsSqlServer.LogServer"].ConnectionString;
                    }

                    if (GetUseLocalDB())
                    {
                        return ConfigurationManager.ConnectionStrings["LocalDB.MsSqlServer.LogServer"].ConnectionString;
                    }

                    return ConfigurationManager.ConnectionStrings["Server.MsSqlServer.LogServer"].ConnectionString;

                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static string GetConnectionStringDBUserLogServer()
                {
                    if (Development.GetIsEnabledDeveloperMode())
                    {
                        return ConfigurationManager.ConnectionStrings["Development.MsSqlServer.UserLogServer"].ConnectionString;
                    }

                    if (GetUseLocalDB())
                    {
                        return ConfigurationManager.ConnectionStrings["LocalDB.MsSqlServer.UserLogServer"].ConnectionString;
                    }

                    return ConfigurationManager.ConnectionStrings["Server.MsSqlServer.UserLogServer"].ConnectionString;


                }

            } //<<< DB


        }






    }
}
