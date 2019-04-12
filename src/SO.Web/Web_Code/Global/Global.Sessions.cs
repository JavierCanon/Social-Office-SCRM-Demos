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


        #region Sessions

        public class Sessions
        {


            public static void EndUserSession()
            {
                // note: can remove all session becuse need use of onstart session values
                // only force login on check on secure pages
                HttpContext.Current.Session.Remove( "User.UserID" );
                HttpContext.Current.Session.Remove( "User.UserGUID" );
                HttpContext.Current.Session.Remove( "User.UserLogin" );
                HttpContext.Current.Session.Remove( "User.UserEmail" );
                HttpContext.Current.Session.Remove( "User.UserFirstName" );
                HttpContext.Current.Session.Remove( "User.UserLastName" );
                HttpContext.Current.Session.Remove( "User.Alias" );
                HttpContext.Current.Session.Remove( "User.DisplayName" );
                HttpContext.Current.Session.Remove( "User.UserRolID" );
                HttpContext.Current.Session.Remove( "User.UserRolGUID" );
                HttpContext.Current.Session.Remove( "User.UserRolName" );
                HttpContext.Current.Session.Remove( "User.UserRolDisplayName" );



            }

            public static void SetDeveloperUserSessionVariables()
            {

                HttpContext.Current.Session["User.UserID"] = ConfigurationManager.AppSettings["Debug:User.ID"].ToString();
                HttpContext.Current.Session["User.UserGUID"] = ConfigurationManager.AppSettings["Debug:User.GUID"].ToString();
                HttpContext.Current.Session["User.UserLogin"] = ConfigurationManager.AppSettings["Debug:User.Login"].ToString();
                HttpContext.Current.Session["User.Alias"] = ConfigurationManager.AppSettings["Debug:User.Alias"].ToString();
                HttpContext.Current.Session["User.UserEmail"] = ConfigurationManager.AppSettings["Debug:User.Email"].ToString();

                HttpContext.Current.Session["User.UserFirstName"] = ConfigurationManager.AppSettings["Debug:User.FirstName"].ToString();
                HttpContext.Current.Session["User.UserLastName"] = ConfigurationManager.AppSettings["Debug:User.FirstName"].ToString();
                HttpContext.Current.Session["User.DisplayName"] = ConfigurationManager.AppSettings["Debug:User.FirstName"].ToString();

                HttpContext.Current.Session["User.UserRolID"] = ConfigurationManager.AppSettings["Debug:User.Rol.ID"].ToString();
                HttpContext.Current.Session["User.UserRolGUID"] = ConfigurationManager.AppSettings["Debug:User.Rol.GUID"].ToString();
                HttpContext.Current.Session["User.UserRolName"] = ConfigurationManager.AppSettings["Debug:User.Rol.Name"].ToString();
                HttpContext.Current.Session["User.UserRolDisplayName"] = ConfigurationManager.AppSettings["Debug:User.Rol.Name"].ToString();


            }


            public static class User
            {

                public static string UserID
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserID"].ToString();

                    }
                }

                public static string UserGUID
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserGUID"].ToString();

                    }
                }

                public static string UserLogin
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserLogin"].ToString();

                    }
                }


                public static string UserEmail
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserEmail"].ToString();

                    }
                }

                public static string UserFirstName
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserFirstName"].ToString();

                    }
                }

                public static string UserLastName
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserLastName"].ToString();

                    }
                }

                public static string Alias
                {
                    get
                    {
                        return HttpContext.Current.Session["User.Alias"].ToString();

                    }
                }

                public static string DisplayName
                {
                    get
                    {
                        return HttpContext.Current.Session["User.DisplayName"].ToString();

                    }
                }



                public static string UserRolID
                {
                    get
                    {
                        return HttpContext.Current.Session["User.UserRolID"].ToString();

                    }
                }




            }


            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public static bool GetIsValidSession()
            {
                if (HttpContext.Current.Session["User.UserID"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool IsUserAuthenticated( HttpSessionState session )
            {

                if (session == null)
                    return false;
                return session["User.UserID"] == null ? false : true;
            }

            // TODO: manage all session task here.
            // set at login.
            // get a value.

            public static void UserCreateSession( HttpSessionState session, SqlDataReader reader )
            {

                session["User.UserID"] = reader["User_ID"].ToString();
                session["User.UserGUID"] = reader["User_GUID"].ToString();
                session["User.UserLogin"] = reader["Login"].ToString().TrimEnd();
                session["User.UserEmail"] = reader["Login"].ToString().TrimEnd();

                session["User.UserFirstName"] = reader["Names"].ToString().Trim();
                session["User.UserLastName"] = reader["Surnames"].ToString().Trim();

                session["User.Alias"] = reader["User_Display"].ToString().Trim();
                session["User.DisplayName"] = reader["User_Display"].ToString().Trim();

                session["User.UserRolID"] = reader["Rol_ID"].ToString();
                session["User.UserRolGUID"] = reader["Rol_GUID"].ToString();
                session["User.UserRolName"] = reader["User_Rol_Name"].ToString().Trim();
                session["User.UserRolDisplayName"] = reader["Rol_Display"].ToString().Trim();


            }





        }

        #endregion  Sessions



    }
}
