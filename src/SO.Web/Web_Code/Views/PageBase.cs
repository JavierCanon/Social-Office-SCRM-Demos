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
using System.Web;
using SO.SL;

namespace SO.Web_Code.Views
{
    public abstract class PageBase : SO.Web_Code.Views.PageBaseCommon
    {
        //public string PageTitle = ""; // variable for controls can access early to page title before render.
        //public int MenuCategory = (int)EnumMenuCategory.None;
        //public int SubMenuCategory = (int)EnumSubMenuCategory.None;


        #region Security
        public static bool SecurityCanInsert;
        public static bool SecurityCanUpdate;
        public static bool SecurityCanDelete;


        #endregion Security
        /*
        protected new void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);


        }


        /// <summary>
        /// valida sesion.
        /// </summary>
        /// <param name="e"></param>
        protected new void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }

        protected new void Page_OnInit(EventArgs e)
        {
            base.Page_OnInit(e);

        }
        */

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void Page_Init(object sender, EventArgs e)
        {
            //base.Page_Init(sender, e);


            //SetDeveloperVariables();

            if (!CheckIsValidSession())
            {
                if (IsCallback)
                {
                    Response.RedirectLocation = ResolveUrl("~/Web/SO/Default.aspx?ReturnUrl=" + Request.RawUrl);
                }
                else
                {
                    Response.Redirect( "~/Web/SO/Default.aspx?ReturnUrl=" + Request.RawUrl);
                }
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

        }

        /*
        protected new void Page_OnInitComplete(EventArgs e)
        {
            base.Page_OnInitComplete(e);


        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

        }
        */
        protected void Page_PreRender(object sender, EventArgs e)
        {
            /*
            // set page title
            if (string.IsNullOrEmpty(Page.Header.Title))
            {
                Page.Title = this.PageTitle;

            }
            */
        }



        /// <summary>
        /// chequea que la sessión sea valida, si no redirecciona al login.
        /// </summary>
        virtual protected void CheckSession()
        {
           // if (Session["User.UserID"] == null)
           if(!Global.Sessions.GetIsValidSession())
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect( "~/Web/SO/Default.aspx?ReturnUrl=" + Request.RawUrl);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        virtual protected bool CheckIsValidSession()
        {
            return Global.Sessions.GetIsValidSession();

            /*
            if (Session["User.UserID"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }


        #region Datasources

        protected void DatasourcesCachesInit(string skey)
        {
            // CACHE
            if (Cache[skey] == null)
            {
                Cache[skey] = DateTime.Now;
            }
        }

        protected void DatasourcesCachesInvalidate(string skey)
        {
            // CACHE
            if (Cache[skey] != null)
            {
                Cache[skey] = DateTime.Now;
            }

        }


        #endregion Datasources



        #region User Session





        #endregion UserSession



        #region Security

        public string SecurityGetDisableClass(UserActionsEnum action)
        {
            if (action == UserActionsEnum.Insert)
            {
                if (SecurityCanInsert)
                {
                    return string.Empty;
                }
                else
                {
                    return "disabled";
                }
            }

            if (action == UserActionsEnum.Update)
            {
                if (SecurityCanUpdate)
                {
                    return string.Empty;
                }
                else
                {
                    return "disabled";
                }
            }

            if (action == UserActionsEnum.Delete)
            {
                if (SecurityCanDelete)
                {
                    return string.Empty;
                }
                else
                {
                    return "disabled";
                }
            }

            return string.Empty;
        }
        #endregion Security


    }
}
