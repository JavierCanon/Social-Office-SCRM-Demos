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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SO.SL
{
	public static class UserLogin
	{
		public enum EncryptionHashAlgorithm
		{
			MD5
			,
			MD5Cng
			,
			None
			,
			SHA256
			,
			SHA256Cng
			,
			SHA512
			,
			SHA512Cng
		}
		/// <summary>
		/// Check against DB if password is ok fof userLogin
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="encryptionHashType"></param>
		/// <param name="sqlConnection"></param>
		/// <returns></returns>
		public static bool CheckUserPassword(string username, string password, EncryptionHashAlgorithm encryptionHashType, string sqlConnection)
		{
			var strSql = @"
SELECT 
Password
FROM dbo.[User]
WHERE
Login = @UserLogin";

			var paramsToSP = new SqlParameter[] { new SqlParameter("@UserLogin", username)
			};

            var dbhash = "";//Softcanon.DAL.SqlApiSqlClient.GetStringRecordValue(strSql, paramsToSP, sqlConnection, 60);

			if (encryptionHashType.Equals(EncryptionHashAlgorithm.SHA512))
			{
				return SHA512HashAlgorithm.VerifyHashSHA512(password, dbhash);
			}

			return false;
		}

		/// <summary>
		/// Save a new password to DB for userId
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="password"></param>
		/// <param name="encryptionHashType"></param>
		/// <param name="sqlConnection"></param>
		public static void SaveNewUserPassword(string userId, string password, EncryptionHashAlgorithm encryptionHashType, string sqlConnection)
		{
			var newpass = string.Empty;

			if (encryptionHashType.Equals(EncryptionHashAlgorithm.SHA512))
			{
				newpass = SHA512HashAlgorithm.ComputeHashSHA512(password);
			}


			var strSql = @"
UPDATE dbo.[User] SET 
       Password = @UserPass
 WHERE 
 User_ID = @UserId
";

			var paramsToSP = new SqlParameter[] { new SqlParameter("@UserPass", password)
			, new SqlParameter("@UserId", userId)
			};


			//Softcanon.DAL.SqlApiSqlClient.ExecuteSqlString(strSql, paramsToSP, sqlConnection, 60);
		}

		/// <summary>
		/// Return an encrypted string
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="password"></param>
		/// <param name="encryptionHashType"></param>
		/// <param name="sqlConnection"></param>
		/// <returns></returns>
		public static string GetNewUserPassword(string userId, string password, EncryptionHashAlgorithm encryptionHashType, string sqlConnection)
		{
			var newpass = string.Empty;

			if (encryptionHashType.Equals(EncryptionHashAlgorithm.SHA512))
			{
				newpass = SHA512HashAlgorithm.ComputeHashSHA512(password);
			}

			return newpass;
		}


        /// <summary>
        /// Currently only SHA512
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptionHashType"></param>
        /// <returns></returns>
        public static string GetNewPasswordFromString(string password, EncryptionHashAlgorithm encryptionHashType)
        {
            var newpass = string.Empty;

            if (encryptionHashType.Equals(EncryptionHashAlgorithm.SHA512))
            {
                newpass = SHA512HashAlgorithm.ComputeHashSHA512(password);
            }

            return newpass;
        }



        /// <summary>
        /// Return SHA512 encoded string
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptionHashType"></param>
        /// <returns></returns>
        public static string GetNewPasswordFromString(string password)
        {
            return SHA512HashAlgorithm.ComputeHashSHA512(password);
        }
	}
}
