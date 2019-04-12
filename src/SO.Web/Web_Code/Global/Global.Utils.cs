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

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;



namespace SO
{
    public partial class Global : System.Web.HttpApplication
    {
        public static class Utils
        {


            #region Webpath Utils

            public static partial class Webpath
            {

                public static class Directories
                {

                    public static string CheckDirNeedExist( string relativePath )
                    {
                        string path = HttpContext.Current.Server.MapPath( relativePath );

                        if (!Directory.Exists( path ))
                            Directory.CreateDirectory( path );

                        return relativePath;
                    }


                }

                // http://msdn.microsoft.com/en-us/library/System.Web.VirtualPathUtility(v=vs.110).aspx
                // System.Web.VirtualPathUtility

                private static string RequestDomainName = null;
                /// <summary>
                /// Return the domain part of the request object
                /// </summary>
                /// <returns></returns>
                public static string GetRequestDomainName()
                {
                    if (RequestDomainName != null)
                    {
                        return RequestDomainName;
                    }
                    if (Configuration.Development.GetIsEnabledDeveloperMode())
                    {
                        RequestDomainName = Configuration.Development.GetDebugWebsiteDomainName();
                    }
                    else
                    {
                        RequestDomainName = HttpContext.Current.Request.Headers["host"].Split( ':' )[0];
                    }
                    return RequestDomainName;
                }

                private static string _RequestApplicationUrlPath = null;
                /// <summary>
                /// Retorna la URL donde esta instalado el aplicativo, termina en slash '/'
                /// </summary>
                /// <returns></returns>
                public static string GetRequestApplicationUrlPath()
                {

                    if (_RequestApplicationUrlPath != null)
                        return _RequestApplicationUrlPath;

                    var applicationPath = string.Empty;

                    if (HttpContext.Current.Request.Url != null)
                    {
                        applicationPath = HttpContext.Current.Request.Url.AbsoluteUri.Substring(
                        0, HttpContext.Current.Request.Url.AbsoluteUri.ToLower().IndexOf(
                        HttpContext.Current.Request.ApplicationPath.ToLower(),
                            HttpContext.Current.Request.Url.AbsoluteUri.ToLower().IndexOf(
                        HttpContext.Current.Request.Url.Authority.ToLower() ) +
                        HttpContext.Current.Request.Url.Authority.Length ) +
                        HttpContext.Current.Request.ApplicationPath.Length );
                    }

                    if (!applicationPath.EndsWith( "/" ))
                        applicationPath += "/";

                    _RequestApplicationUrlPath = applicationPath;
                    return _RequestApplicationUrlPath;

                }

                /*
                private static string RequestWebsiteUrlPath = null;
                /// <summary>
                /// Return the URL of the client virtual folder
                /// </summary>
                /// <param name="page"></param>
                /// <param name="clientDirName"></param>
                /// <returns></returns>
                public static string GetRequestWebsiteUrlPath()
                {
                    if (RequestWebsiteUrlPath != null) return RequestWebsiteUrlPath;

                    RequestWebsiteUrlPath = GetRequestApplicationUrlPath() + "Sites/"
                        + GetWebsitePathFolderName() + "/";

                    return RequestWebsiteUrlPath;
                }
                */



                /// <summary>
                /// Returns physical path from virtual relative path.
                /// </summary>
                /// <param name="virtualPath"></param>
                /// <returns></returns>
                public static string GetMapPathFromVirtual( string virtualPath )
                {

                    return HttpContext.Current.Server.MapPath( virtualPath );
                }

                /// <summary>
                ///  Convert physical path into relative path
                /// </summary>
                /// <param name="page"></param>
                /// <param name="AbsolutePath"></param>
                /// <returns></returns>
                public static string GetVirtualPathFromPhysical( string PhysicalPath )
                {
                    return PhysicalPath.Replace( HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty ).Replace( Path.DirectorySeparatorChar, '/' );

                }

            }


        }
        #endregion Webpath Utils





        #region Email

        // need min. .net 4.5



        // http://www.codeproject.com/Articles/667461/Send-asynchronous-mail-using-asp-net
        // https://msdn.microsoft.com/en-us/library/x5x13z6h(v=vs.110).aspx
        //TODO: pass email message object System.Net.Mail.MailMessage 
        public static partial class Emails
        {


            private static void SendCompletedCallback( object sender, AsyncCompletedEventArgs e )
            {
                // Get the unique identifier for this asynchronous operation.
                string token = (string)e.UserState;
                //string token = e.UserState.ToString();


                if (e.Cancelled)
                {
                    // Console.WriteLine( "[{0}] Send canceled.", token );
                }
                if (e.Error != null)
                {
                    // Console.WriteLine( "[{0}] {1}", token, e.Error.ToString() );
                    Global.LogError( HttpContext.Current, EnumLogCategories.EMAIL, "Error sending email: " + token + " " + e.Error );


                }
                else
                {
                    // Console.WriteLine( "Message sent." );
                    Global.LogDebug( HttpContext.Current, EnumLogCategories.EMAIL, "Email Send " + token );
                }
                // mailSent = true;
            }

            // http://stackoverflow.com/questions/7276375/what-are-best-practices-for-using-smtpclient-sendasync-and-dispose-under-net-4/7276819#7276819

            /// <summary>
            /// Call using:
            /// using System.Threading.Tasks;
            /// var t = Task.Run( () => Global.Utils.Emails.SendEmailAsync( "desarrollo@softcanon.com", asunto, "enviado desde helpdesk", false ) );
            /// t.Wait();
            /// </summary>
            /// <param name="toEmailAddress"></param>
            /// <param name="emailSubject"></param>
            /// <param name="emailMessage"></param>
            /// <param name="isBodyHtml"></param>
            /// <returns></returns>    
            public static async Task SendEmailAsync( string[] to, string from, string[] CC, string[] BCC, string emailSubject, string emailMessage, bool isBodyHtml )
            {
                var message = new MailMessage();
                //message.To.Add( toEmailAddress );

                // from?
                message.From = new MailAddress( from );

                // to?
                if (to != null)
                    foreach (string s in to)
                    {

                        if (s != null)
                            message.To.Add( s );
                    }

                // CC?
                if (CC != null)
                    foreach (string s in CC)
                    {

                        if (s != null)
                            message.To.Add( s );
                    }

                // BCC?
                if (BCC != null)
                    foreach (string s in BCC)
                    {

                        if (s != null)
                            message.To.Add( s );
                    }


                message.Subject = emailSubject;
                message.Body = emailMessage;
                message.IsBodyHtml = isBodyHtml;

                message.From = new MailAddress( Configuration.Mail.GetMailServerLogin() );


                //Proper Authentication Details need to be passed when sending email from gmail
                NetworkCredential mailAuthentication = new NetworkCredential( Configuration.Mail.GetMailServerLogin(), Configuration.Mail.GetMailServerPassword() );

                using (var smtpClient = new SmtpClient())
                {

                    // server
                    smtpClient.Host = Configuration.Mail.GetMailServer();
                    smtpClient.Port = Configuration.Mail.GetMailServerPort();
                    smtpClient.EnableSsl = Configuration.Mail.GetMailServerIsEnableSSL();
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = mailAuthentication;


                    if (Global.Configuration.Development.GetIsEnabledDeveloperMode())
                        smtpClient.Timeout = 5000;
                    else
                        smtpClient.Timeout = 180000;  //An Int32 that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).


                    // Set the method that is called back when the send operation ends.
                    // smtpClient.SendCompleted += new SendCompletedEventHandler( SendCompletedCallback );

                    // The userState can be any object that allows your callback 
                    // method to identify this send operation.
                    // For this example, the userToken is a string constant.
                    string userState = emailSubject.Replace( " ", "" ) + "_" + DateTime.Now.Ticks.ToString();


                    // send
                    try
                    {
                        await smtpClient.SendMailAsync( message );
                        // smtpClient.Send( message ); // only works with this...
                        // smtpClient.SendAsync( message, userState );
                    }
                    catch (Exception e)
                    {

                        Global.LogError( HttpContext.Current, EnumLogCategories.EMAIL, e.Message + Environment.NewLine + e.InnerException );
                    }


                }
            }



        }

        #endregion Email



    }
}
