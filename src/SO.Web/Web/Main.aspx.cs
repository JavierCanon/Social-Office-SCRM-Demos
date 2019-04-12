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
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SO.Web
{
    public partial class Main : Web_Code.Views.PageBase
    {
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            /*
            if (!SL.Permissions.GetPermission(
                SL.UserPermissionTypeEnum.BINUMBER_ACCESO_DASHBOARD_PREMIACION
                , Session
                , Global.DAL.GetConnectionStringDBData()))
            {
                Response.Redirect("~/ErrorPermission.html", true);
            }
            */
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && !IsCallback)
            { }



        }

        protected void ASPxDashboard1_DataLoading(object sender, DevExpress.DashboardWeb.DataLoadingWebEventArgs e)
        {



        }

        protected void ASPxDashboard1_ConfigureDataConnection(object sender, DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs e)
        {
            //MsSqlConnectionParameters parameters = (MsSqlConnectionParameters)e.ConnectionParameters;

            /*
        foreach (var dataSource in e.Dashboard.DataSources)
        {
            if (dataSource is DevExpress.DashboardCommon.DashboardSqlDataSource)
                (dataSource as DevExpress.DashboardCommon.DashboardSqlDataSource).ConnectionOptions.CommandTimeout = 600;
        }*/

        }

        protected void ASPxDashboard1_CustomParameters(object sender, DevExpress.DashboardWeb.CustomParametersWebEventArgs e)
        {
            // no parameters for pregenerated table
            if (Request.QueryString["t"] != null)
            {
                //  e.Parameters.Clear();
            }


            /*
            if (Request.QueryString["lastday"] != null)
            {

                var custIDParameter = e.Parameters.FirstOrDefault(p => p.Name == "fechaIni");
                if (custIDParameter != null)
                {
                    custIDParameter.Value = Convert.ToDateTime(BINumber_Web.Global.Data.Info.Apuestas.Hasta);
                }

                custIDParameter = e.Parameters.FirstOrDefault(p => p.Name == "fechaFin");
                if (custIDParameter != null)
                {
                    custIDParameter.Value = Convert.ToDateTime(BINumber_Web.Global.Data.Info.Apuestas.Hasta);
                }

            }
            */

        }

        protected void ASPxDashboard1_Load(object sender, EventArgs e)
        {

            return; /*********************** config this ****************************************/

            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXDocument(System.Xml.Linq.XDocument.Load(
                Server.MapPath("~/App_Data/Dashboards/Terminales_Ventas.xml")));


            //Change Dashboard Data

            // default values
//            dashboard.Parameters["Ano"].Value = DateTime.Now.Year;
  //          dashboard.Parameters["Mes"].Value = DateTime.Now.Month;

            // option 1: change query in xml
            // option 2: change query in data provider
            // >>> set dashboard source
            // https://documentation.devexpress.com/Dashboard/117050/Examples/Web-Dashboard-Examples/How-to-Connect-the-Web-Dashboard-to-an-SQL-Database

            ASPxDashboard1.OpenDashboard(dashboard.SaveToXDocument());
            // <<< set dashboard source
        }

        protected void ASPxDashboard1_Init(object sender, EventArgs e)
        {

        }
    }
}