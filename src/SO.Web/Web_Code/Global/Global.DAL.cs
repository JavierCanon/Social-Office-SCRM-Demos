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
using System.Web;
//using Softcanon.DAL;

namespace SO
{
    public partial class Global : System.Web.HttpApplication
    {


        public static class DAL
        {


            #region Connection DB Utils


            /// <summary>
            /// Connection to the CRM database.
            /// The DB of the clients need to be the same name of the domain.
            /// </summary>
            /// <returns></returns>
            public static string GetConnectionStringDBMain()
            {
                if (ConnectionStringDBMain != null)
                {
                    return ConnectionStringDBMain;
                }
                else
                {

                    ConnectionStringDBMain = Configuration.DB.GetConnectionStringDBMain();

                    /*
                    if (Configuration.Development.GetIsEnabledDeveloperMode())
                    {
                        var config = WebConfigurationManager.OpenWebConfiguration("~/Sites/" + Configuration.GetDevelopmentCustomFolder() + "/");
                        ConnectionStringDBMain = config.ConnectionStrings.ConnectionStrings["MsSqlServer.SO"].ConnectionString;
                    }
                    else
                    {
                        string folder = "";
                        if (Configuration.Application.GetInstallationIsCustom())
                        {
                            folder = Configuration.GetInstallationCustomFolder();
                        }

                        if (string.IsNullOrEmpty(folder)) folder = GetRequestDomainName().Replace(".", "_");

                        var config = WebConfigurationManager.OpenWebConfiguration("~/Sites/" + folder + "/");
                        ConnectionStringDBMain = config.ConnectionStrings.ConnectionStrings["MsSqlServer.SO"].ConnectionString;
                    }
                    */

                    /*
                    var builder =
                    new SqlConnectionStringBuilder(Configuration.DB.GetConnectionStringDBSO());

                    // builder["Database"] = "SO" + GetRequestDomainName().Replace(".", string.Empty);
                    builder.InitialCatalog = "SO" + GetRequestDomainName().Replace(".", "_");
                    ConnectionStringDB = builder.ConnectionString;
                    */

                    return ConnectionStringDBMain;
                }
            }




        }
        #endregion Connection DB Utils


        #region Configuration




        /// <summary>
        /// Parametros que se cargan de la DB para evitar tener que reiniciar el App.
        /// y que puedan ser actualizados en vivo y consultados por cada petición, ej. cambio del dolar.
        /// !!! DEBE DE COINCIDIR LOS IDS CON LOS DE LA BASE DE DATOS !!!.
        /// </summary>
        public static class ParametersAppClients
        {
            public enum ParamIdEnum : int
            {
                EMAIL_AVISO = 1
            }

            public enum GetValueByEnum : int
            {
                GetValueByID = 1
                ,
                GetValueByName = 2
            }

            public static string GetParameterForClient( GetValueByEnum getValueByEnum, ParamIdEnum parameter, string cacheKey, string strDBConnection )
            {
                if (getValueByEnum.Equals( GetValueByEnum.GetValueByID ))
                {
                    return GetParameterClientById( parameter, cacheKey, strDBConnection );
                }
                if (getValueByEnum.Equals( GetValueByEnum.GetValueByName ))
                {
                    return GetParameterClientByName( parameter, cacheKey, strDBConnection );
                }
                return null;
            }

            private static string GetParameterClientById( ParamIdEnum parameter, string cacheKey, string strDBConnection )
            {
                var result = (string)HttpRuntime.Cache["GetParameterClientById_" + parameter.ToString() + "_" + cacheKey];
                if (result == null)
                {
                    //result = SqlApiSqlClient.GetStringRecordValue( "EXEC PARAMETROS_CLIENTS_GET_VALUE_BY_ID " + Convert.ToInt32( parameter ).ToString(), strDBConnection );
                    HttpRuntime.Cache.Insert( "GetParameterClientById" + parameter.ToString() + cacheKey, result,
                                                                                    null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes( 5 ) );
                }
                return result;
            }

            private static string GetParameterClientByName( ParamIdEnum parameter, string cacheKey, string strDBConnection )
            {
                var result = (string)HttpRuntime.Cache["GetParameterClientByName_" + parameter.ToString() + "_" + cacheKey];
                if (result == null)
                {
                    //result = SqlApiSqlClient.GetStringRecordValue( "EXEC PARAMETROS_CLIENTS_GET_VALUE_BY_NAME '" + parameter.ToString() + "'", strDBConnection );
                    HttpRuntime.Cache.Insert( "GetParameterClientByName" + parameter.ToString() + cacheKey, result,
                                                                        null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes( 5 ) );
                }
                return result;
            }
        }



        #endregion Configuration


    }
}
