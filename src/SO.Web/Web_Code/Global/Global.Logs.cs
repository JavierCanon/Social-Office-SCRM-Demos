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

        
        #region  Log4Net
        public AdoNetAppender ConfigureLog4Net( string _LOGGING_CONNECTIONSTRING, bool DEBUGINFO )
        {
            var sqlAppender = new AdoNetAppender();
            sqlAppender.CommandType = CommandType.Text;
            sqlAppender.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data,Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

            sqlAppender.ConnectionString = _LOGGING_CONNECTIONSTRING;
            sqlAppender.CommandText = "INSERT INTO Log ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @Exception)";

            var param1 = new AdoNetAppenderParameter();
            param1.ParameterName = "@log_date";
            param1.Layout = new log4net.Layout.RawTimeStampLayout();
            param1.DbType = DbType.DateTime;
            sqlAppender.AddParameter( param1 );

            var param2 = new AdoNetAppenderParameter();
            param2.ParameterName = "@log_level";
            param2.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%level" ) );
            param2.DbType = DbType.AnsiString;
            param2.Size = 50;
            sqlAppender.AddParameter( param2 );

            var param3 = new AdoNetAppenderParameter();
            param3.ParameterName = "@thread";
            param3.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%thread" ) );
            param3.DbType = DbType.AnsiString;
            param3.Size = 255;
            sqlAppender.AddParameter( param3 );

            var param4 = new AdoNetAppenderParameter();
            param4.ParameterName = "@logger";
            param4.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%logger" ) );
            param4.DbType = DbType.AnsiString;
            param4.Size = 255;
            sqlAppender.AddParameter( param4 );

            var param5 = new AdoNetAppenderParameter();
            param5.ParameterName = "@message";
            param5.DbType = DbType.AnsiString;
            param5.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%message" ) );
            param5.Size = 4000;
            sqlAppender.AddParameter( param5 );

            var param6 = new AdoNetAppenderParameter();
            param6.ParameterName = "@Exception";
            param6.DbType = DbType.AnsiString;
            param6.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.ExceptionLayout() );
            param6.Size = 4000;
            sqlAppender.AddParameter( param6 );

            var filter = new log4net.Filter.LevelRangeFilter();

            if (bEnableDeveloperLogger)
            {
                sqlAppender.BufferSize = 1;
            }
            else
            {
                sqlAppender.BufferSize = Configuration.Logger.GetLoggerProductionBufferSize();
            }
            sqlAppender.ActivateOptions();

            return sqlAppender;
        }

        /// <summary>
        /// Nota: Validar los valores de los campos que se graban sean validos segun tipo de datos
        /// si no no hace el log, ej. int debe ir 0 si esta en blanco intval = "" debe ser intval="0" para que graba el log.
        /// </summary>
        /// <param name="_LOGGING_CONNECTIONSTRING"></param>
        /// <param name="DEBUGINFO"></param>
        /// <returns></returns>
        public AdoNetAppender ConfigureLog4NetCustomFields( string tableName, string _LOGGING_CONNECTIONSTRING, bool DEBUGINFO )
        {
            var sqlAppender = new AdoNetAppender();
            sqlAppender.CommandType = CommandType.Text;
            sqlAppender.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data,Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

            sqlAppender.ConnectionString = _LOGGING_CONNECTIONSTRING;
            sqlAppender.CommandText =
            @"INSERT INTO [" + tableName + @"]
           ([Date]
           ,[Thread]
           ,[Level]
           ,[Logger]
           ,[Message]
           ,[Exception]
           ,[Category]
           ,[UserId]
           ,[RolId]
           ,[Ip]
           ,[Url]
           ,[UserAgent])
     VALUES
           (
             @log_date
            ,@thread
            ,@log_level
            ,@logger
            ,@message
            ,@Exception
            ,@Category
            ,@UserId
            ,@RolId
            ,@Ip
            ,@Url
            ,@UserAgent
           )";


            var param1 = new AdoNetAppenderParameter();
            param1.ParameterName = "@log_date";
            param1.Layout = new log4net.Layout.RawTimeStampLayout();
            param1.DbType = DbType.DateTime;
            sqlAppender.AddParameter( param1 );

            var param2 = new AdoNetAppenderParameter();
            param2.ParameterName = "@log_level";
            param2.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%level" ) );
            param2.DbType = DbType.AnsiString;
            param2.Size = 50;
            sqlAppender.AddParameter( param2 );

            var param3 = new AdoNetAppenderParameter();
            param3.ParameterName = "@thread";
            param3.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%thread" ) );
            param3.DbType = DbType.AnsiString;
            param3.Size = 255;
            sqlAppender.AddParameter( param3 );

            var param4 = new AdoNetAppenderParameter();
            param4.ParameterName = "@logger";
            param4.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%logger" ) );
            param4.DbType = DbType.AnsiString;
            param4.Size = 255;
            sqlAppender.AddParameter( param4 );

            var param5 = new AdoNetAppenderParameter();
            param5.ParameterName = "@message";
            param5.DbType = DbType.AnsiString;
            param5.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%message" ) );
            param5.Size = 4000;
            sqlAppender.AddParameter( param5 );


#if LOG4NETUSE_MDC
        AdoNetAppenderParameter param6 = new AdoNetAppenderParameter();
        param6.ParameterName = "@Exception";
        param6.DbType = DbType.AnsiString;
        param6.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{Exception}"));
        param6.Size = 4000;
        sqlAppender.AddParameter(param6);


        AdoNetAppenderParameter param12 = new AdoNetAppenderParameter();
        param12.ParameterName = "@Category";      
        param12.DbType = DbType.AnsiString;
        param12.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{Category}"));
        param12.Size = 50;
        sqlAppender.AddParameter(param12);
                

        AdoNetAppenderParameter param7 = new AdoNetAppenderParameter();
        param7.ParameterName = "@UserId";
        param7.DbType = DbType.Int32;
        param7.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{UserId}"));
        param7.Size = 10;
        sqlAppender.AddParameter(param7);

        AdoNetAppenderParameter param8 = new AdoNetAppenderParameter();
        param8.ParameterName = "@RolId";
        param8.DbType = DbType.Int32;
        param8.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{RolId}"));
        param8.Size = 10;
        sqlAppender.AddParameter(param8);

        AdoNetAppenderParameter param9 = new AdoNetAppenderParameter();
        param9.ParameterName = "@Ip";
        param9.DbType = DbType.AnsiString;
        param9.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{Ip}"));
        param9.Size = 255;
        sqlAppender.AddParameter(param9);

        AdoNetAppenderParameter param10 = new AdoNetAppenderParameter();
        param10.ParameterName = "@Url";
        param10.DbType = DbType.AnsiString;
        param10.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{Url}"));
        param10.Size = 255;
        sqlAppender.AddParameter(param10);

        AdoNetAppenderParameter param11 = new AdoNetAppenderParameter();
        param11.ParameterName = "@UserAgent";
        param11.DbType = DbType.AnsiString;
        param11.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%X{UserAgent}"));
        param11.Size = 255;
        sqlAppender.AddParameter(param11);
#else

            var param6 = new AdoNetAppenderParameter();
            param6.ParameterName = "@Exception";
            param6.DbType = DbType.AnsiString;
            param6.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{Exception}" ) );
            param6.Size = 4000;
            sqlAppender.AddParameter( param6 );


            var param12 = new AdoNetAppenderParameter();
            param12.ParameterName = "@Category";
            param12.DbType = DbType.AnsiString;
            param12.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{Category}" ) );
            param12.Size = 50;
            sqlAppender.AddParameter( param12 );


            var param7 = new AdoNetAppenderParameter();
            param7.ParameterName = "@UserId";
            param7.DbType = DbType.Int32;
            param7.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{UserId}" ) );
            param7.Size = 10;
            sqlAppender.AddParameter( param7 );

            var param8 = new AdoNetAppenderParameter();
            param8.ParameterName = "@RolId";
            param8.DbType = DbType.Int32;
            param8.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{RolId}" ) );
            param8.Size = 10;
            sqlAppender.AddParameter( param8 );

            var param9 = new AdoNetAppenderParameter();
            param9.ParameterName = "@Ip";
            param9.DbType = DbType.AnsiString;
            param9.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{Ip}" ) );
            param9.Size = 255;
            sqlAppender.AddParameter( param9 );

            var param10 = new AdoNetAppenderParameter();
            param10.ParameterName = "@Url";
            param10.DbType = DbType.AnsiString;
            param10.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{Url}" ) );
            param10.Size = 255;
            sqlAppender.AddParameter( param10 );

            var param11 = new AdoNetAppenderParameter();
            param11.ParameterName = "@UserAgent";
            param11.DbType = DbType.AnsiString;
            param11.Layout = new log4net.Layout.Layout2RawLayoutAdapter( new log4net.Layout.PatternLayout( "%property{UserAgent}" ) );
            param11.Size = 255;
            sqlAppender.AddParameter( param11 );
#endif

            var filter = new log4net.Filter.LevelRangeFilter();

            if (bEnableDeveloperLogger)
            {
                sqlAppender.BufferSize = 1;
            }
            else
            {
                sqlAppender.BufferSize = Configuration.Logger.GetLoggerProductionBufferSize();
            }
            sqlAppender.ActivateOptions();


            return sqlAppender;
        }

        #endregion  Log4Net
        
        #region Logger
        public enum EnumLoggerToType : int
        {
            DB = 2
            ,
            FILE = 1
        }

        private enum EnumLoggerLevel : int
        {
            DEBUG
            ,
            ERROR
            ,
            FATAL
            ,
            INFO
            ,
            WARN
        }

        public enum EnumLogCategories : int
        {
            CONFIGURATION
            ,
            DATABASE
            ,
            DEBUG
            ,
            GENERAL
            ,
            SECURITY
            ,

            EMAIL
                ,
            CONTENT
            ,
            WEB4CMS
            ,
            WEB4CRM
            ,
            WEB4EMAILS
            ,
            WEB4PAYMENTS
            ,
            WEB4SALES
                ,
            WEB4SOCIAL

        }

        public static void LogInfo( HttpContext context, EnumLogCategories logCategory, string text )
        {
            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }


                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    loggerFileInfo.Info(
                    "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                    );
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {



#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif

                    loggerDBInfo.Info( text );


#if LOG4NETUSE_MDC
                // limpiar
                MDC.Set("Exception", "");
                MDC.Set("Category", "");
                MDC.Set("UserId", "0");
                MDC.Set("RolId", "0");
                MDC.Set("Ip", "");
                MDC.Set("Url", "");
                MDC.Set("UserAgent", "");
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif

                }
            }
        }

        public static void LogError( HttpContext context, EnumLogCategories logCategory, string text )
        {
            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }

                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    loggerFileError.Error(
                    "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                    );
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {

#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif

                    loggerDBInfo.Error( text );

#if LOG4NETUSE_MDC
               MDC.Set("Exception", "");
               MDC.Set("Category", "");
               MDC.Set("UserId", "0");
               MDC.Set("RolId", "0");
               MDC.Set("Ip", "");
               MDC.Set("Url", "");
               MDC.Set("UserAgent", "");
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif

                }
            }
        }

        public static void LogError( HttpContext context, EnumLogCategories logCategory, string text, Exception Exception )
        {
            if (context == null || text == null || Exception == null)
                return;


            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }

                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    if (Exception != null)
                    {
                        loggerFileError.Error(
                        "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                        + Environment.NewLine + Exception.Message + Environment.NewLine + Exception.StackTrace );
                    }
                    else
                    {
                        loggerFileError.Error(
                        "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                        );
                    }
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {
#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
                if (Exception != null)
                {
                    MDC.Set("Exception", Exception.Message + Environment.NewLine + Exception.StackTrace);
                    
                }
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
                    if (Exception != null)
                    {
                        log4net.ThreadContext.Properties["Exception"] = Exception.Message + Environment.NewLine + Exception.StackTrace;
                    }
#endif



                    loggerDBInfo.Error( text );


#if LOG4NETUSE_MDC
                MDC.Set("Exception", "");
                MDC.Set("Category", "");
                MDC.Set("UserId", "0");
                MDC.Set("RolId", "0");
                MDC.Set("Ip", "");
                MDC.Set("Url", "");
                MDC.Set("UserAgent", "");
                
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;

#endif

                }
            }
        }

        public static void LogFatal( HttpContext context, EnumLogCategories logCategory, string text )
        {
            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }

                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    loggerFileError.Fatal(
                    "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                    );
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {
#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif

                    loggerDBInfo.Fatal( text );

#if LOG4NETUSE_MDC
                MDC.Set("Exception", "");
                MDC.Set("Category", "");
                MDC.Set("UserId", "0");
                MDC.Set("RolId", "0");
                MDC.Set("Ip", "");
                MDC.Set("Url", "");
                MDC.Set("UserAgent", "");
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif

                }
            }
        }

        public static void LogWarn( HttpContext context, EnumLogCategories logCategory, string text )
        {
            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }

                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    loggerFileInfo.Warn(
                    "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                    );
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {
#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif

                    loggerDBInfo.Warn( text );

#if LOG4NETUSE_MDC
                MDC.Set("Exception", "");
                MDC.Set("Category", "");
                MDC.Set("UserId", "0");
                MDC.Set("RolId", "0");
                MDC.Set("Ip", "");
                MDC.Set("Url", "");
                MDC.Set("UserAgent", "");
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif

                }
            }
        }

        public static void LogDebug( HttpContext context, EnumLogCategories logCategory, string text )
        {
            if (bEnableLogger || bEnableDeveloperLogger)
            {
                var UsuId = "0";
                var UsuRolId = "0";
                var UsuLogin = string.Empty;
                var UsuRolNombre = string.Empty;
                var ip = string.Empty;
                var agent = string.Empty;
                var url = string.Empty;


                if (context != null)
                {
                    if (context.Request != null)
                    {
                        ip = context.Request.UserHostAddress;
                        agent = context.Request.UserAgent;
                        url = context.Request.CurrentExecutionFilePath;
                    }

                    if (context.Session != null)
                    {
                        if (context.Session["User.UserID"] != null)
                        {
                            UsuId = context.Session["User.UserID"].ToString();
                        }
                        if (context.Session["User.UserRolID"] != null)
                        {
                            UsuRolId = context.Session["User.UserRolID"].ToString();
                        }
                        if (context.Session["User.UserLogin"] != null)
                        {
                            UsuLogin = context.Session["User.UserLogin"].ToString();
                        }
                        if (context.Session["User.UserRolName"] != null)
                        {
                            UsuRolNombre = context.Session["User.UserRolName"].ToString();
                        }
                    }
                }

                if (eLoggerToType == EnumLoggerToType.FILE)
                {
                    loggerFileInfo.Debug(
                    "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                    );
                }
                if (eLoggerToType == EnumLoggerToType.DB)
                {
#if LOG4NETUSE_MDC
                MDC.Set("Category", logCategory.ToString());
                MDC.Set("UserId", UsuId);
                MDC.Set("RolId", UsuRolId);
                MDC.Set("Ip", ip);
                MDC.Set("Url", url);
                MDC.Set("UserAgent", agent);
#else
                    log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
                    log4net.ThreadContext.Properties["UserId"] = UsuId;
                    log4net.ThreadContext.Properties["RolId"] = UsuRolId;
                    log4net.ThreadContext.Properties["Ip"] = ip;
                    log4net.ThreadContext.Properties["Url"] = url;
                    log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif

                    loggerDBInfo.Debug( text );

#if LOG4NETUSE_MDC
                MDC.Set("Exception", "");
                MDC.Set("Category", "");
                MDC.Set("UserId", "0");
                MDC.Set("RolId", "0");
                MDC.Set("Ip", "");
                MDC.Set("Url", "");
                MDC.Set("UserAgent", "");
#else
                    log4net.ThreadContext.Properties["Exception"] = string.Empty;
                    log4net.ThreadContext.Properties["Category"] = string.Empty;
                    log4net.ThreadContext.Properties["UserId"] = "0";
                    log4net.ThreadContext.Properties["RolId"] = "0";
                    log4net.ThreadContext.Properties["Ip"] = string.Empty;
                    log4net.ThreadContext.Properties["Url"] = string.Empty;
                    log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif
                }
            }
        }
        #endregion Logger

        #region User Log

        static string connUserLog;
        // http://stackoverflow.com/questions/14903887/warning-this-call-is-not-awaited-execution-of-the-current-method-continues
        // A method marked as async can return void, Task or Task<T>. What are the differences between them?
        // A Task<T> returning async method can be awaited, and when the task completes it will proffer up a T.
        // A Task returning async method can be awaited, and when the task completes, the continuation of the task is scheduled to run.
        // A void returning async method cannot be awaited; it is a "fire and forget" method. It does work asynchronously, and you have no way of telling when it is done. This is more than a little bit weird; as SLaks says, normally you would only do that when making an asynchronous event handler. The event fires, the handler executes; no one is going to "await" the task returned by the event handler because event handlers do not return tasks, and even if they did, what code would use the Task for something? It's usually not user code that transfers control to the handler in the first place.

        /// <summary>
        /// Log user action in DB or File according var eUserLoggerToType (EnumLoggerToType)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logLevel"></param>
        /// <param name="logCategory"></param>
        /// <param name="logMessage"></param>
        /// <param name="logException"></param>
        /// <returns></returns>
        public static async void LogUser( HttpContext context, string logLevel, string logCategory, string logMessage, string logException )
        {
            if (!bEnableUserLogger)
            {
                return;
            }

            var UsuId = "0";
            var UsuRolId = "0";
            var UsuLogin = string.Empty;
            var UsuRolNombre = string.Empty;
            var Ip = string.Empty;
            var UserAgent = string.Empty;
            var Url = string.Empty;


            if (context != null)
            {
                if (context.Request != null)
                {
                    Ip = context.Request.UserHostAddress;
                    UserAgent = context.Request.UserAgent;
                    Url = context.Request.CurrentExecutionFilePath;
                }

                if (context.Session != null)
                {
                    if (context.Session["User.UserID"] != null)
                    {
                        UsuId = context.Session["User.UserID"].ToString();
                    }
                    if (context.Session["User.UserRolID"] != null)
                    {
                        UsuRolId = context.Session["User.UserRolID"].ToString();
                    }
                    if (context.Session["User.UserLogin"] != null)
                    {
                        UsuLogin = context.Session["User.UserLogin"].ToString();
                    }
                    if (context.Session["User.UserRolName"] != null)
                    {
                        UsuRolNombre = context.Session["User.UserRolName"].ToString();
                    }
                }
            }


            if (eUserLoggerToType == EnumLoggerToType.FILE)
            {
                /*
                loggerUser.Info(
                "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
                );
                 */
            }

            if (eUserLoggerToType == EnumLoggerToType.DB)
            {

                using (SqlConnection connection = new SqlConnection() { ConnectionString = connUserLog })
                {

                    await connection.OpenAsync();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandTimeout = 60 * 5;

                    /*
                    command.CommandText = string.Format(@"INSERT INTO [UserLog]
       ([Date]
       ,[Thread]
       ,[Level]
       ,[Logger]
       ,[Message]
       ,[Exception]
       ,[Category]
       ,[UserId]
       ,[RolId]
       ,[Ip]
       ,[Url]
       ,[UserAgent])
 VALUES
       (
         GETDATE() --@log_date
        ,NULL @thread
        ,{0} --@log_level
        ,NULL --@logger
        ,{1} --@message
        ,{2} --@Exception
        ,{3} --@Category
        ,{4} --@UserId
        ,{5} --@RolId
        ,{6} --@Ip
        ,{7} --@Url
        ,{8} --@UserAgent
       )"

        ,logLevel //{0} --@log_level
        ,logMessage //{1} --@message
        ,logException//{2} --@Exception
        ,logCategory //{3} --@Category
        ,UsuId //{4} --@UserId
        ,UsuRolId //{5} --@RolId
        ,ip //{6} --@Ip
        ,url //{7} --@Url
        ,agent //{8} --@UserAgent
                           
                            
                            
          );
        */

                    command.CommandText = @"INSERT INTO [User_Log]
           ([Date]
           ,[Thread]
           ,[Level]
           ,[Logger]
           ,[Message]
           ,[Exception]
           ,[Category]
           ,[User_Id]
           ,[Rol_Id]
           ,[Ip]
           ,[Url]
           ,[UserAgent])
     VALUES
           (
             GETDATE() --@log_date
            ,NULL --@thread
            ,@logLevel
            ,'DB' --@logger
            ,@logMessage
            ,@logException
            ,@logCategory
            ,@UserId
            ,@RolId
            ,@Ip
            ,@Url
            ,@UserAgent
           )";

                    var parameters = new SqlParameter[] {
              new SqlParameter{ ParameterName="logLevel", DbType= DbType.AnsiString, Size=60, Value= logLevel}
            , new SqlParameter{ ParameterName="logMessage", DbType= DbType.AnsiString, Size=8000, Value= logMessage }
            , new SqlParameter{ ParameterName="logException", DbType= DbType.AnsiString, Size=8000, Value= logException}
            , new SqlParameter{ ParameterName="logCategory", DbType= DbType.AnsiString, Size=60, Value= logCategory}
            , new SqlParameter{ ParameterName="UserId", DbType= DbType.Int32, Size=255, Value= UsuId}
            , new SqlParameter{ ParameterName="RolId", DbType= DbType.Int32, Size=255, Value= UsuRolId}
            , new SqlParameter{ ParameterName="Ip", DbType= DbType.AnsiString, Size=255, Value= Ip}
            , new SqlParameter{ ParameterName="Url", DbType= DbType.AnsiString, Size=255, Value= Url}
            , new SqlParameter{ ParameterName="UserAgent", DbType= DbType.AnsiString, Size=255, Value= UserAgent}
            };

                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add( parameter );
                    }


                    int result = await command.ExecuteNonQueryAsync();

                }



            }

        }


        /*
public static void LogUser(HttpContext context, string logCategory, string text)
{
    if (bEnableUserLogger)
    {
        var UsuId = "0";
        var UsuRolId = "0";
        var UsuLogin = string.Empty;
        var UsuRolNombre = string.Empty;
        var ip = string.Empty;
        var agent = string.Empty;
        var url = string.Empty;


        if (context != null)
        {
            if (context.Request != null)
            {
                ip = context.Request.UserHostAddress;
                agent = context.Request.UserAgent;
                url = context.Request.CurrentExecutionFilePath;
            }

            if (context.Session != null)
            {
                if (context.Session["User.UserID"] != null)
                {
                    UsuId = context.Session["User.UserID"].ToString();
                }
                if (context.Session["User.UserRolID"] != null)
                {
                    UsuRolId = context.Session["User.UserRolID"].ToString();
                }
                if (context.Session["User.UserLogin"] != null)
                {
                    UsuLogin = context.Session["User.UserLogin"].ToString();
                }
                if (context.Session["User.UserRolName"] != null)
                {
                    UsuRolNombre = context.Session["User.UserRolName"].ToString();
                }
            }
        }


        if (eUserLoggerToType == EnumLoggerToType.FILE)
        {
            loggerUser.Info(
            "[" + logCategory + "][" + UsuId + " " + UsuLogin + "][" + UsuRolId + " " + UsuRolNombre + "][" + ip + "][" + agent + "][" + url + "] " + Environment.NewLine + text
            );
        }

        if (eUserLoggerToType == EnumLoggerToType.DB)
        {



#if LOG4NETUSE_MDC
        MDC.Set("Category", logCategory.ToString());
        MDC.Set("UserId", UsuId);
        MDC.Set("RolId", UsuRolId);
        MDC.Set("Ip", ip);
        MDC.Set("Url", url);
        MDC.Set("UserAgent", agent);
#else
            log4net.ThreadContext.Properties["Category"] = logCategory.ToString();
            log4net.ThreadContext.Properties["UserId"] = UsuId;
            log4net.ThreadContext.Properties["RolId"] = UsuRolId;
            log4net.ThreadContext.Properties["Ip"] = ip;
            log4net.ThreadContext.Properties["Url"] = url;
            log4net.ThreadContext.Properties["UserAgent"] = agent;
#endif
 
            loggerUser.Info(text);


#if LOG4NETUSE_MDC
        // limpiar
        MDC.Set("Exception", "");
        MDC.Set("Category", "");
        MDC.Set("UserId", "0");
        MDC.Set("RolId", "0");
        MDC.Set("Ip", "");
        MDC.Set("Url", "");
        MDC.Set("UserAgent", "");
#else
            log4net.ThreadContext.Properties["Exception"] = string.Empty;
            log4net.ThreadContext.Properties["Category"] = string.Empty;
            log4net.ThreadContext.Properties["UserId"] = "0";
            log4net.ThreadContext.Properties["RolId"] = "0";
            log4net.ThreadContext.Properties["Ip"] = string.Empty;
            log4net.ThreadContext.Properties["Url"] = string.Empty;
            log4net.ThreadContext.Properties["UserAgent"] = string.Empty;
#endif

        }
    }
}
*/

        #endregion User Log



    }
}
