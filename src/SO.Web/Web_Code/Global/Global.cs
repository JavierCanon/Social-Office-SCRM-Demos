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

//------------------------------------------------------------------

using System;
using System.IO;
using System.Web;
using Errors;
using log4net.Appender;

/// <summary>
///
/// ----------------------------------------------------------------
/// HISTORIAL DE CAMBIOS
/// ----------------------------------------------------------------
///
///
///
/// TODO:
/// - On app start scan for templates and widgets and sinchronize DB.  SuperAdmin option in interfaz.
///
///
/// </summary>
namespace SO
{
    public partial class Global : System.Web.HttpApplication
    {

        #region fields and properties


        static string _AppCredits, _AppName = "Social Office SCRM";
        static public string AppComercialName = "Social Office SCRM";
        
        private static string sDirLogs, sDatabaseCnn, sTemplateKey, sWebsitePathFolderName, sClientGMapsKey, sClientWebAppKey;
        private static bool bClientValidSerialKey, isFirstRequestInitialization = true;
        private static bool IsDebugJs, IsDebugCSS;
        
        // >>> Logger
        private static log4net.ILog loggerFileInfo;
        private static log4net.ILog loggerFileError;
        private static log4net.ILog loggerUser;

        private AdoNetAppender sqlAppender;
        private static log4net.ILog loggerDBInfo;

        private static EnumLoggerToType eLoggerToType = EnumLoggerToType.FILE;
        private static EnumLoggerToType eUserLoggerToType = EnumLoggerToType.DB;
        
        private static bool bEnableDeveloperLogger = false, bEnableLogger = false, bEnableUserLogger = false;
        // <<<

        // DB Connections
        private static string ConnectionStringDBMain = null;
        private static string ConnectionStringDBData = null;
        private static string ConnectionStringDBMaster = null;

        // <<<

        #region App Data

        public static class AppData
        {
            public static DateTime Data_LastDayWithSales;

        }
        #endregion App Data

        #endregion fields and properties

        #region Application Object

        private void Application_Start( object sender, EventArgs e )
        {

            // >>> variables
            IsDebugJs = Configuration.Optimization.GetIsEnabledOptimizationJavascriptUseMinifiedFiles() ? true : false;
            IsDebugCSS = Configuration.Optimization.GetIsEnabledOptimizationCSSUseMinifiedFiles() ? true : false;

            // RSS news
            // Website.WidgetsRSSUrlNewsInternational = Configuration.Application.Widgets.GetWidgetNewsInsternational_RSS_URL();
            // Website.WidgetsRSSUrlNewsSO = Configuration.Application.Widgets.GetWidgetNewsSO_RSS_URL(); 


            #region logger            
            eLoggerToType = (EnumLoggerToType)Configuration.Logger.GetLoggerToTypeMode();
            eUserLoggerToType = (EnumLoggerToType)Configuration.Logger.GetUserLoggerToTypeMode();
            bEnableDeveloperLogger = Configuration.Logger.GetIsEnabledDeveloperLogger();
            bEnableLogger = Configuration.Logger.GetIsEnabledLogger();
            bEnableUserLogger = Configuration.Logger.GetIsEnabledUserLogger();
            bEnableDeveloperLogger = Configuration.Logger.GetIsEnabledDeveloperLogger();

            // In development save logs to file
            if (Configuration.Development.GetIsEnabledDeveloperMode())
                eLoggerToType = EnumLoggerToType.FILE;

            if ((eLoggerToType == EnumLoggerToType.FILE) && loggerFileInfo == null)
            {
                log4net.Util.LogLog.InternalDebugging = false; // set false on production servers...
                log4net.Config.XmlConfigurator.Configure();

                loggerFileInfo = log4net.LogManager.GetLogger( "file" );
                loggerFileError = log4net.LogManager.GetLogger( "fileError" );
            }

            if ((eLoggerToType == EnumLoggerToType.DB) && loggerDBInfo == null)
            {
                loggerDBInfo = log4net.LogManager.GetLogger( "sqlserverdb" );
                var logdb = (log4net.Repository.Hierarchy.Logger)loggerDBInfo.Logger;

                logdb.AddAppender( ConfigureLog4NetCustomFields( "Log", Configuration.DB.GetConnectionStringDBLogServer(), true ) );

                logdb.Repository.Configured = true;
            }

            if (bEnableUserLogger)
            {
                /* NOT WORKING log4net to use DB...
                loggerUser = log4net.LogManager.GetLogger("sqlserverdb");
                var logdbuser = (log4net.Repository.Hierarchy.Logger)loggerUser.Logger;

                logdbuser.AddAppender(ConfigureLog4NetCustomFields("UserLog", Configuration.GetConnectionStringDBUserLogServer(), true));

                logdbuser.Repository.Configured = true;
                */

                connUserLog = Configuration.DB.GetConnectionStringDBUserLogServer();


            }



            sDirLogs = Server.MapPath( "~" ) + "\\App_Data\\Logs\\";
            if (!Directory.Exists( sDirLogs ))
                Directory.CreateDirectory( sDirLogs );
            
            #endregion logger
            


            // >>> CMS
            //Application["Theme_Name"] = "Softcanon";
            //Application["CMS_SitioWebId"] = "1";

            // >>> CACHE DATA

            // <<< CACHE

            LogInfo( null, EnumLogCategories.GENERAL, "Aplicativo Iniciado..." );
            Application["App.Start.Datetime"] = System.DateTime.Now;


            // Web Api
            //System.Web.Http.GlobalConfiguration.Configure( WebApiConfig.Register );

            // Log callback errors
            // Assign Application_Error as a callback error handler
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler( Application_Error );


        }

        private void Application_End( object sender, EventArgs e )
        {
            LogInfo( null, EnumLogCategories.GENERAL, "Aplicativo finalizado..." );
        }

        private void Application_BeginRequest( Object sender, EventArgs e )
        {
            //var path = HttpContext.Current.Request.Url.AbsolutePath;

            // >>> Globalization fix >>> 
            // la cultura en español colombia que se define en el web.config,
            // pero para los numeros que sean de la forma 6,454,456.99 con punto como separador decimal para evitar errores sql.

            /*
          CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
          
          //newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
          //newCulture.DateTimeFormat.DateSeparator = "-";
          newCulture.NumberFormat.NumberDecimalSeparator = ".";
          newCulture.NumberFormat.NumberGroupSeparator = ",";
          newCulture.NumberFormat.CurrencyDecimalSeparator = ".";
          newCulture.NumberFormat.CurrencyGroupSeparator = ",";

          //Thread.CurrentThread.CurrentCulture = newCulture;
          Thread.CurrentThread.CurrentUICulture = newCulture;
            
           

            // spanish latinamerican *es-419* NOT SUPORTED ERROR AT RUNTIME * culture is best for store numbers 111,111.00 and date is dd/mm/yyyy but currency is bad US$ 
            newCulture.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            */

            // <<< Globalization fix <<<


            // security >>>

            // TODO: Error page in web4crm or in website????...

            // disable static CMS editor files to anonymous
            /*
            if (path.EndsWith(".CMS.aspx", true, CultureInfo.InvariantCulture))
            {
                if (Context.Session == null)
                {
                    if (!Configuration.Development.GetIsEnabledDeveloperMode())
                    {
                        Response.StatusCode = 403;
                        Response.Redirect("~/ErrorPageHttp403.aspx");
                        return;
                    }
                }
                else
                {
                    if (!path.Contains("/Sites/")) // in web4crm control panel?
                    {
                        Response.StatusCode = 403;
                        Response.Redirect("~/ErrorPageHttp403.aspx");
                        return;
                    }
                    else
                    {
                        if (Context.Session["User.UserID"] == null)
                            Response.Redirect("~/Default.aspx");
                        return;

                    }
                }

            }*/
            // <<< security

            // Iframe fix >>>
            // HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            // Iframe fix <<<

            // SEO >>>
            //var lowercaseURL = Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + path;

            // <<< SEO 
        }

        private void Application_EndRequest( object sender, EventArgs e )
        {
            if (bEnableDeveloperLogger)
            {
                // no for postback
                // first we will check the "__EVENTTARGET" because if post back made by the controls
                // which used "_doPostBack" function also available in Request.Form collection.

                /*
                if (Request.Params["__EVENTTARGET"] == null)
                {

                    var tsDuration = DateTime.Now.Subtract(Context.Timestamp);
                    LogDebug(Context, EnumLogCategories.GENERAL, "Solicitud completada en: " + tsDuration.ToString());
                }
                */
            }
        }

        // Exception _previousException = new Exception(); // for prevent multiple log of same error, ex. web attacks.
        string _previousPageError = "";

        private void Application_Error( object sender, EventArgs e )
        {
            //https://msdn.microsoft.com/en-us/library/24395wz3.aspx

            HttpServerUtility server = HttpContext.Current.Server;
            Exception exception = server.GetLastError();
            string currentPageRequest = HttpContext.Current.Request.FilePath;


            if (Configuration.Development.GetIsEnabledDebugDeveloperModeShowGlobalPageError())
            {

                if (exception.GetType() == typeof( HttpException ))
                {
                    Server.Transfer( "~/ErrorPageHttp.aspx" );
                }
                else
                {
                    Response.Write( "<h2>Global Page Error</h2>\n" );
                    Response.Write(
                        "<p>" + exception.Message + "</p>\n" );
                    Response.Write( "Return to the <a href='/Default.aspx'>" +
                        "Default Page</a>\n" );

                    if (Context.Request.IsLocal)
                    {
                        Response.Write( "<p>" + exception.Source + "</p>\n" );
                        Response.Write( "<p>" + exception.InnerException + "</p>\n" );
                        Response.Write( "<p>" + exception.StackTrace + "</p>\n" );
                    }
                }

                if (! _previousPageError.Equals( currentPageRequest ))
                {

                    ExceptionUtility.LogException( exception, currentPageRequest );
                    ExceptionUtility.NotifySystemOps( exception );
                }
            }

            // if not in DEV MODE, write error to .txt
            if (!Configuration.Development.GetIsEnabledDeveloperMode())
            {


                // devexpress callback error
                // Use HttpContext.Current to get a Web request processing helper

                // is http?
                if (exception is HttpUnhandledException)
                {



                    HttpException ex = (HttpException)Server.GetLastError();
                    //Exception innerexception = exception.InnerException;

                    // Log an exception
                    // TODO, send email to admin.
                    if (!_previousPageError.Equals( currentPageRequest ))
                    {

                        ExceptionUtility.LogException( exception, currentPageRequest );
                        ExceptionUtility.NotifySystemOps( exception );


                        // log to logger
                        if (HttpContext.Current != null && HttpContext.Current.Request != null)
                            Global.LogError( Context, Global.EnumLogCategories.GENERAL,
                            "HTTP " + ex.GetHttpCode() + ": " + Request.RawUrl.ToString(),
                            ex );
                    }

                    // options to show info to user:
                    // 1. show a blank page:
                    //Server.ClearError();
                    // 2. redirect to a page
                    // Response.Redirect("~/Errors/ErrorPageHttp.aspx");
                    // 3. do nothing, go to customErrors configuration in web.config and/or show asp.net error
                }
                else
                {
                    // other NO http errors
                    ExceptionUtility.LogException( exception, currentPageRequest );
                    ExceptionUtility.NotifySystemOps( exception );

                    // log to logger
                    if (HttpContext.Current != null && HttpContext.Current.Request != null)
                        Global.LogError( Context, Global.EnumLogCategories.GENERAL,
                        "Error " + exception.HResult + ": " + Request.RawUrl.ToString(),
                        exception );

                }

            }
            else
            {
                ExceptionUtility.LogException( exception, currentPageRequest );
                ExceptionUtility.NotifySystemOps( exception );

                // log to logger, no works in callback mode
                /*
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                    Global.LogError( Context, Global.EnumLogCategories.GENERAL,
                        "Error " + exception.HResult + ": " + Request.RawUrl.ToString(),
                        exception );
                        */

                ExceptionUtility.LogException( exception, currentPageRequest );
                ExceptionUtility.NotifySystemOps( exception );



            }

            

            //_previousException = exception;
            _previousPageError = currentPageRequest;


        }

        #endregion Application Object

        #region Session Object

        /// <summary>
        /// Código que se ejecuta cuando se inicia una nueva sesión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Session_Start( object sender, EventArgs e )
        {

            if (isFirstRequestInitialization)
            {
                isFirstRequestInitialization = false;

            } //<< end first request/session.

            //! >>> Always run

            // >>> CACHE DATA

            // <<< CACHE

            LogDebug( null, EnumLogCategories.CONFIGURATION, "Session Starterd, ID: " + Session.SessionID + " IP: " + Context.Request.UserHostAddress.ToString() );


            // set debug session vars.
            if (Configuration.Development.GetIsEnabledDeveloperMode())
            {
                Global.Debug.SetUserTestSessionVariables();
            }



            //! <<< Always run

        }

        /// <summary>
        /// Código que se ejecuta cuando finaliza una sesión.
        /// Nota: El evento Session_End se desencadena sólo cuando el modo sessionstate
        /// se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        /// o SQLServer, el evento no se genera. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Session_End( object sender, EventArgs e )
        {
        }



        #endregion Session Object

        #region Cache

        #endregion Cache

















    } // end Global


}
