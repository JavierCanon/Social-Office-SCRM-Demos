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
using System.Data.Common;
using System.Globalization;

namespace SO.Demos.SalesDBGenerator {
    public class SqlHelper<T, Command>
        where T : DbConnection
        where Command : DbCommand, new() {
        public object ReadValue(T connection, string selectQuery, params DbParameter[] pars) {
            using(var sql = CreateCommand(selectQuery, connection)) {
                sql.CommandTimeout = 1000;
                if(pars != null)
                    sql.Parameters.AddRange(pars);
                try {
                    return CheckDbNull(sql.ExecuteScalar());
                }
                catch { return null; }
            }
        }
        object CheckDbNull(object value) {
            if(value == null) return null;
            if(Object.ReferenceEquals(value, DBNull.Value)) return null;
            return value;
        }
        static DbCommand CreateCommand(string selectQuery, T connection) {
            return new Command() { CommandText = selectQuery, Connection = connection };
        }
        public List<object[]> ReadValues(T connection, string selectQuery, params DbParameter[] pars) {
            List<object[]> res = new List<object[]>();
            try {
                using(var sql = CreateCommand(selectQuery, connection)) {
                    sql.CommandTimeout = 5000;
                    if(pars != null) sql.Parameters.AddRange(pars);
                    using(DbDataReader reader = sql.ExecuteReader()) {
                        if(!reader.HasRows) return res;
                        while(reader.Read()) {
                            object[] values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            res.Add(values);
                        }
                    }
                }
            }
            catch { }
            return res;
        }
        public string GetString(object val) {
            if(val == DBNull.Value || val == null) return string.Empty;
            return val.ToString();
        }
        public DateTime GetDateInv(object val) {
            if(val == DBNull.Value || val == null) return DateTime.MinValue;
            return DateTime.ParseExact(val.ToString(), "d/M/yyyy", DateTimeFormatInfo.InvariantInfo);
        }
        public int GetInt(object val) {
            if(val == DBNull.Value || val == null) return 0;
            return Convert.ToInt32(val);
        }
        public DateTime GetDate(object value) {
            if(value == null || value == DBNull.Value) return DateTime.MinValue;
            if(value is DateTime) return (DateTime)value;
            return GetDateInv(value);
        }
        public decimal GetDecimal(object value) {
            if(value == null || value == DBNull.Value) return 0;
            return (decimal)Convert.ChangeType(value, typeof(decimal));
        }
        public Guid GetGuid(object value) {
            if(value == null || value == DBNull.Value) return Guid.Empty;
            return new Guid(value.ToString());
        }
        public bool GetBool(object value) {
            if(value == null || value == DBNull.Value) return false;
            if(value is bool) return (bool)value;
            if(value is int) return (int)value == 1;
            return false;
        }
    }
}
