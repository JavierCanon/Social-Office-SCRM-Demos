using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Softcanon.DAL;

namespace SO.SL
{


	/// <summary>
	/// Descripción breve de Permisos
	/// http://msdn.microsoft.com/en-us/library/c3s1ez6e(v=VS.90).aspx
	/// http://stackoverflow.com/questions/424366/c-string-enums
	///
	/// CACHE DE LOS PERMISOS DE USUARIO:
	/// Se utiliza System.Web.HttpRuntime.Cache
	///
	/// Con tipo sliding 
	///
	/// http://msdn.microsoft.com/en-us/library/18c1wd61(v=vs.100).aspx
	///
	///
	/// </summary>
	public static class Permissions
	{

		/// <summary>
		/// Get permission for a user with user rol assigned
		/// </summary>
		/// <param name="userPerType"></param>
		/// <param name="session"></param>
		/// <param name="strDBConnection"></param>
		/// <returns></returns>
		public static bool GetPermission(UserPermissionTypeEnum userPerType, System.Web.SessionState.HttpSessionState session, string strDBConnection)
		{
			if (session["User.UserRolId"].ToString().Equals(UserRolEnum.System) || session["User.UserRolId"].ToString().Equals(UserRolEnum.SuperAdministrator))
			{
				return true;
			}

			var key = session.SessionID + "_Permission_" + userPerType.ToString();

			var bresult = GetFromCache(key);
			if (bresult == null)
			{
				bresult = GetPermissionDB(Convert.ToString((int)userPerType), session["User.UserId"].ToString(), session["User.UserRolId"].ToString(), strDBConnection);
				HttpRuntime.Cache.Insert(key, bresult, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
			}

			return (bool)bresult;
		}

        /// <summary>
        /// Get permission using HttpContext.Current.Session
        /// </summary>
        /// <param name="userPerType"></param>
        /// <param name="strDBConnection"></param>
        /// <returns></returns>
        public static bool GetPermission(UserPermissionTypeEnum userPerType, string strDBConnection)
        {
            if (HttpContext.Current.Session["User.UserRolId"].ToString().Equals(UserRolEnum.System) || HttpContext.Current.Session["User.UserRolId"].ToString().Equals(UserRolEnum.SuperAdministrator))
            {
                return true;
            }

            var key = HttpContext.Current.Session.SessionID + "_Permission_" + userPerType.ToString();

            var bresult = GetFromCache(key);
            if (bresult == null)
            {
                bresult = GetPermissionDB(Convert.ToString((int)userPerType), HttpContext.Current.Session["User.UserId"].ToString(), HttpContext.Current.Session["User.UserRolId"].ToString(), strDBConnection);
                HttpRuntime.Cache.Insert(key, bresult, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
            }

            return (bool)bresult;
        }


        /// <summary>
        /// Get permission using HttpContext.Current.Session
        /// </summary>
        /// <param name="userPerType"></param>
        /// <param name="strDBConnection"></param>
        /// <returns></returns>
        public static bool GetPermission(int userPerType, string strDBConnection)
        {
            if (HttpContext.Current.Session["User.UserRolId"].ToString().Equals(UserRolEnum.System) || HttpContext.Current.Session["User.UserRolId"].ToString().Equals(UserRolEnum.SuperAdministrator))
            {
                return true;
            }

            var key = HttpContext.Current.Session.SessionID + "_Permission_" + userPerType.ToString();

            var bresult = GetFromCache(key);
            if (bresult == null)
            {
                bresult = GetPermissionDB(userPerType.ToString(), HttpContext.Current.Session["User.UserId"].ToString(), HttpContext.Current.Session["User.UserRolId"].ToString(), strDBConnection);
                HttpRuntime.Cache.Insert(key, bresult, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
            }

            return (bool)bresult;
        }


		private static bool? GetFromCache(string key)
		{
			if (HttpRuntime.Cache[key] == null)
			{
				return null;
			}
			else
			{
				return Convert.ToBoolean(HttpRuntime.Cache[key].ToString());
			}
		}

		/// <summary>
		/// Get UserPermissionTypeEnum from an string name
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static UserPermissionTypeEnum GetEnumUserPermissionTypeFromStringName(string str)
		{
			UserPermissionTypeEnum c;

			if (Enum.IsDefined(typeof(UserPermissionTypeEnum), str))
			{
				c = (UserPermissionTypeEnum)Enum.Parse(typeof(UserPermissionTypeEnum), str, true);
			}
			else
			{
				c = UserPermissionTypeEnum.NoValidEnum;
			}
			return c;
		}

		/// <summary>
		/// Get rol enum from string rol name
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static UserRolEnum GetEnumUserRolFromStringName(string str)
		{
			UserRolEnum c;

			if (Enum.IsDefined(typeof(UserRolEnum), str))
			{
				c = (UserRolEnum)Enum.Parse(typeof(UserRolEnum), str, true);
			}
			else
			{
				c = UserRolEnum.NoValidEnum;
			}
			return c;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPerType"></param>
        /// <param name="userId"></param>
        /// <param name="userRolId"></param>
        /// <param name="strDBConnection"></param>
        /// <returns></returns>
		private static bool GetPermissionDB(UserPermissionTypeEnum userPerType, string userId, UserRolEnum userRolId, string strDBConnection)
		{
			if (userRolId == UserRolEnum.System || userRolId == UserRolEnum.SuperAdministrator)
			{
				return true;
			}

			var bReturn = false;
			var strSql = "Permission_QueryUser";


			var paramsToSP = new SqlParameter[] { new SqlParameter("@UserId", userId )
			,  new SqlParameter("@UserRolId", userRolId )
			,  new SqlParameter("@UserPerTypeId", userPerType )

			};

			var sqlapi = new SqlApiSqlClient();
			using (sqlapi.Connection = new SqlConnection(strDBConnection))
			{
				var reader = sqlapi.DataReaderSqlSP(strSql, paramsToSP);

				if (!reader.HasRows)
				{
					bReturn = false;
				}
				else
				{
					if (reader.Read())
					{
						if (reader.IsDBNull(0))
						{
							bReturn = false;
						}
						else
						{
							bReturn = Convert.ToBoolean(reader[0]);
						}
					}
					else
					{
						bReturn = false;
					}
				}

				reader.Close();
				sqlapi.Connection.Close();
			}


			return bReturn;
		}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPerType"></param>
        /// <param name="userId"></param>
        /// <param name="userRolId"></param>
        /// <param name="strConnection"></param>
        /// <returns></returns>
		private static bool GetPermissionDB(string userPerType, string userId, string userRolId, string strConnection)
		{
			if (userRolId.Equals(Convert.ToString((int)UserRolEnum.System)) || userRolId.Equals(Convert.ToString((int)UserRolEnum.SuperAdministrator)))
			{
				return true;
			}

			var bReturn = false;
			var strSql = "EXEC Permission_QueryUser @UserId, @UserRolId, @UserPerTypeId ;";
            
			var paramsToSP = new SqlParameter[] { 
             new SqlParameter() { ParameterName="@UserId", DbType= DbType.Int32, Value= userId}
			,new SqlParameter() { ParameterName="@UserRolId", DbType= DbType.Int32, Value= userRolId}
			,new SqlParameter() { ParameterName="@UserPerTypeId", DbType= DbType.Int32, Value= userPerType}

			};


            bReturn = SqlApiSqlClient.GetBitRecordValue(strSql, paramsToSP, strConnection);


            /*
			var sqlapi = new SqlApiSqlClient();
			using (sqlapi.Connection = new SqlConnection(strConnection))
			{
				var reader = sqlapi.DataReaderSqlSP(strSql, paramsToSP);

				if (!reader.HasRows)
				{
					bReturn = false;
				}
				else
				{
					if (reader.Read())
					{
						if (reader.IsDBNull(0))
						{
							bReturn = false;
						}
						else
						{
							bReturn = Convert.ToBoolean(reader[0]);
						}
					}
					else
					{
						bReturn = false;
					}
				}

				reader.Close();
				sqlapi.Connection.Close();
			}
            */

			return bReturn;
		}
	}
}
