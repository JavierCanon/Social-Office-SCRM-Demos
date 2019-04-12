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
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace SO.SalesDemo.Model {
    public class OleDataProvider : IDataProvider, IDisposable {
        protected enum GroupColumn {
            None,
            Channel,
            Product,
            Region,
            Sector
        }
        public static DbConnection connection;
        public OleDataProvider() { }
        public OleDataProvider(string dbPath) {
            connection = new OleDbConnection(GetConnectionString(dbPath));
            connection.Open();
        }
        public static string GetConnectionString(string dbPath) {
            // return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", dbPath);
      
            // Access 2016
            dbPath = dbPath.Replace(".mdb", ".accdb");
            return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Data Source={0}", dbPath);
        }
        protected string BuildQueryBySales(DateTime start, DateTime end, GroupingPeriod period) {
            string groupBy = GetGroupBy(period, "Sale_Date");
            string groupByExpression = "";
            if(!string.IsNullOrEmpty(groupBy))
                groupByExpression = string.Format(@", {0} AS SaleDate", groupBy);
            string query = String.Format(@"SELECT
                                                'TOTAL' AS Name,
	                                            SUM(Total_Cost) AS TotalCost, 
	                                            SUM(Units) AS Units
                                                {0}
                                           FROM 
                                                Sales
                                           WHERE 
                                                Sale_Date BETWEEN @PeriodStart and @PeriodEnd
                                           ",
                                            groupByExpression);
            if(!string.IsNullOrEmpty(groupBy))
                query += " GROUP BY " + groupBy;
            return query;
        }
        protected virtual DbCommand CreateQueryBySales(DateTime start, DateTime end, GroupingPeriod period) {
            string query = BuildQueryBySales(start, end, period);
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.Add(new OleDbParameter("@PeriodStart", OleDbType.Date)).Value = start;
            command.Parameters.Add(new OleDbParameter("@PeriodEnd", OleDbType.Date)).Value = end;
            command.Parameters.Add(new OleDbParameter("@DiffBase", OleDbType.BSTR)).Value = "0";
            return command;
        }
        protected virtual string Cstr(string value) {
            return string.Format("Cstr({0})", value);
        }
        protected string BuildJoinQuery(DateTime start, DateTime end, GroupingPeriod period, GroupColumn column) {
            string groupBy = GetGroupBy(period, "Sales.Sale_Date");
            string groupByField = string.Empty;
            string groupByCondition = string.Empty;
            if(!string.IsNullOrEmpty(groupBy)) {
                groupByField = ", " + groupBy + " AS SaleDate";
                groupByCondition = ", " + groupBy;
            }
            string keyColumn = string.Empty;
            string tableName = string.Empty;
            string columnName = string.Empty;
            if(column != GroupColumn.None) {
                string name = Enum.GetName(typeof(GroupColumn), column);
                keyColumn = name + "Id";
                tableName = name + "s";
                columnName = name + "_Name";
            }
            string query = string.Format(@"SELECT
	                                        {1} as Name,
	                                        SUM(Sales.Total_Cost) as TotalCost, 
	                                        SUM(Sales.Units) as Units
                                            {2}
                                        FROM 
                                            Sales
                                        INNER JOIN 
                                            {3} as LinkedTable on Sales.{0} = LinkedTable.Id
                                        WHERE 
                                            Sales.Sale_Date BETWEEN @PeriodStart and @PeriodEnd  
                                        GROUP BY
                                            {1} {4}", keyColumn, Cstr("LinkedTable." + columnName), groupByField, tableName, groupByCondition);
            return query;
        }
        protected virtual DbCommand CreateJoinQuery(DateTime start, DateTime end, GroupingPeriod period, GroupColumn column) {
            string query = BuildJoinQuery(start, end, period, column);
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.Add(new OleDbParameter("@PeriodStart", OleDbType.Date)).Value = start;
            command.Parameters.Add(new OleDbParameter("@PeriodEnd", OleDbType.Date)).Value = end;
            return command;
        }
        static string GetGroupBy(GroupingPeriod period, string column) {
            switch(period) {
                case GroupingPeriod.Hour:
                    return String.Format("DATEADD(\"h\", DATEDIFF(\"h\", 0," + column + "), 0)");
                case GroupingPeriod.Day:
                    return String.Format("DATEADD(\"d\", DATEDIFF(\"d\", 0," + column + "), 0)");
            }

            return string.Empty;
        }
        static DateTime MinDate = new DateTime(1900, 1, 1, 0, 0, 0);
        static DateTime MaxDate = new DateTime(2100, 12, 31, 11, 59, 59);
        IEnumerable<SalesGroup> ExecuteQuery(DateTime start, DateTime end, GroupingPeriod period, GroupColumn column) {
            if(start < MinDate) 
                start = MinDate;
            if(end > MaxDate) 
                end = MaxDate;
            IList<SalesGroup> result = null;
            if(connection.State != ConnectionState.Open && connection.State != ConnectionState.Connecting)
                connection.Open();
            DbCommand command;
            if(column == GroupColumn.None)
                command = CreateQueryBySales(start, end, period);
            else
                command = CreateJoinQuery(start, end, period, column);
            command.Connection = connection;
            command.Prepare();

            result = new List<SalesGroup>();
            using(var reader = command.ExecuteReader()) {
                while(reader.Read()) {
                    string name = reader["Name"].ToString();
                    decimal totalCost = 0;
                    int units = 0;
                    DateTime startDate = start;
                    DateTime endDate = end;
                    if(!(reader["TotalCost"] is DBNull)) {
                        totalCost = Convert.ToDecimal(reader["TotalCost"]);
                        units = Convert.ToInt32(reader["Units"]);
                        if(reader.FieldCount > 3) {
                            startDate = Convert.ToDateTime(reader["SaleDate"]);
                            endDate = GetEndDate(startDate, end, period);
                        }
                    }
                    result.Add(new SalesGroup(name, totalCost, units, startDate, endDate));
                }
            }
            return result;
        }
        DateTime GetEndDate(DateTime startDate, DateTime end, GroupingPeriod period) {
            if(period == GroupingPeriod.Day)
                return startDate.AddDays(1).AddMilliseconds(-1);
            else if(period == GroupingPeriod.Hour)
                return startDate.AddHours(1).AddMilliseconds(-1);
            return end;
        }
        #region IDataProvider
        public SalesGroup GetTotalSalesByRange(DateTime start, DateTime end) {
            var salesGroups = ExecuteQuery(start, end, GroupingPeriod.All, GroupColumn.None);
            if(salesGroups != null)
                foreach(var salesGroup in salesGroups)
                    return salesGroup;
            return null;
        }
        public IEnumerable<SalesGroup> GetSales(DateTime start, DateTime end, GroupingPeriod period) {
            return ExecuteQuery(start, end, period, GroupColumn.None);
        }
        public IEnumerable<SalesGroup> GetSalesByChannel(DateTime start, DateTime end, GroupingPeriod period) {
            return ExecuteQuery(start, end, period, GroupColumn.Channel);
        }
        public IEnumerable<SalesGroup> GetSalesByProduct(DateTime start, DateTime end, GroupingPeriod period) {
            return ExecuteQuery(start, end, period, GroupColumn.Product);
        }
        public IEnumerable<SalesGroup> GetSalesByRegion(DateTime start, DateTime end, GroupingPeriod period) {
            return ExecuteQuery(start, end, period, GroupColumn.Region);
        }
        public IEnumerable<SalesGroup> GetSalesBySector(DateTime start, DateTime end, GroupingPeriod period) {
            return ExecuteQuery(start, end, period, GroupColumn.Sector);
        }
        #endregion
        #region IDisposable
        public void Dispose() {
            if(connection.State == ConnectionState.Open)
                connection.Close();
            connection.Dispose();
        }
        #endregion
    }
}
