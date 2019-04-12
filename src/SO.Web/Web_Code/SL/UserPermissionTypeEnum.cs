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
namespace SO.SL
{

    /// <summary>
    /// Enum for control users permissions. Needs to be synchronized with db ids.
    /// Enum value = db id (UserPerTypeId).
    /// </summary>
    public enum UserPermissionTypeEnum
    {
        /// <summary>
        /// PERMISSION FOR CAT.(3) ACCOUNTS, ACCESS
        /// </summary>
        ACCOUNTS_ACCESS = 300,
        /// <summary>
        /// PERMISSION FOR CAT.(3) ACCOUNTS, ACCESS_REPORTS
        /// </summary>
        ACCOUNTS_ACCESS_REPORTS = 304,
        /// <summary>
        /// PERMISSION FOR CAT.(3) ACCOUNTS, DELETE
        /// </summary>
        ACCOUNTS_DELETE = 303,
        /// <summary>
        /// PERMISSION FOR CAT.(3) ACCOUNTS, INSERT
        /// </summary>
        ACCOUNTS_INSERT = 302,
        /// <summary>
        /// PERMISSION FOR CAT.(3) ACCOUNTS, UPDATE
        /// </summary>
        ACCOUNTS_UPDATE = 301,
        /// <summary>
        /// PERMISSION FOR CAT.(7) CALLS, ACCESS
        /// </summary>
        CALLS_ACCESS = 715,
        /// <summary>
        /// PERMISSION FOR CAT.(7) CALLS, ACCESS_REPORTS
        /// </summary>
        CALLS_ACCESS_REPORTS = 719,
        /// <summary>
        /// PERMISSION FOR CAT.(7) CALLS, DELETE
        /// </summary>
        CALLS_DELETE = 718,
        /// <summary>
        /// PERMISSION FOR CAT.(7) CALLS, INSERT
        /// </summary>
        CALLS_INSERT = 717,
        /// <summary>
        /// PERMISSION FOR CAT.(7) CALLS, UPDATE
        /// </summary>
        CALLS_UPDATE = 716,
        /// <summary>
        /// PERMISSION FOR CAT.(8) CHAT, ACCESS
        /// </summary>
        CHAT_ACCESS = 820,
        /// <summary>
        /// PERMISSION FOR CAT.(8) CHAT, ACCESS_REPORTS
        /// </summary>
        CHAT_ACCESS_REPORTS = 824,
        /// <summary>
        /// PERMISSION FOR CAT.(8) CHAT, DELETE
        /// </summary>
        CHAT_DELETE = 823,
        /// <summary>
        /// PERMISSION FOR CAT.(8) CHAT, INSERT
        /// </summary>
        CHAT_INSERT = 822,
        /// <summary>
        /// PERMISSION FOR CAT.(8) CHAT, UPDATE
        /// </summary>
        CHAT_UPDATE = 821,
        /// <summary>
        /// PERMISSION FOR CAT.(2) CLIENTS, ACCESS
        /// </summary>
        CLIENTS_ACCESS = 200,
        /// <summary>
        /// PERMISSION FOR CAT.(9) CLIENTSREPORTS, ACCESS
        /// </summary>
        CLIENTSREPORTS_ACCESS = 925,
        /// <summary>
        /// PERMISSION FOR CAT.(9) CLIENTSREPORTS, ACCESS_REPORTS
        /// </summary>
        CLIENTSREPORTS_ACCESS_REPORTS = 929,
        /// <summary>
        /// PERMISSION FOR CAT.(9) CLIENTSREPORTS, DELETE
        /// </summary>
        CLIENTSREPORTS_DELETE = 928,
        /// <summary>
        /// PERMISSION FOR CAT.(9) CLIENTSREPORTS, INSERT
        /// </summary>
        CLIENTSREPORTS_INSERT = 927,
        /// <summary>
        /// PERMISSION FOR CAT.(9) CLIENTSREPORTS, UPDATE
        /// </summary>
        CLIENTSREPORTS_UPDATE = 926,
        /// <summary>
        /// PERMISSION FOR CAT.(4) CONTACTS, ACCESS
        /// </summary>
        CONTACTS_ACCESS = 400,
        /// <summary>
        /// PERMISSION FOR CAT.(4) CONTACTS, ACCESS_REPORTS
        /// </summary>
        CONTACTS_ACCESS_REPORTS = 404,
        /// <summary>
        /// PERMISSION FOR CAT.(4) CONTACTS, DELETE
        /// </summary>
        CONTACTS_DELETE = 403,
        /// <summary>
        /// PERMISSION FOR CAT.(4) CONTACTS, INSERT
        /// </summary>
        CONTACTS_INSERT = 402,
        /// <summary>
        /// PERMISSION FOR CAT.(4) CONTACTS, UPDATE
        /// </summary>
        CONTACTS_UPDATE = 401,
        /// <summary>
        /// PERMISSION FOR CAT.(13) ECOMMERCE, ACCESS
        /// </summary>
        ECOMMERCE_ACCESS = 1345,
        /// <summary>
        /// PERMISSION FOR CAT.(13) ECOMMERCE, ACCESS_REPORTS
        /// </summary>
        ECOMMERCE_ACCESS_REPORTS = 1349,
        /// <summary>
        /// PERMISSION FOR CAT.(13) ECOMMERCE, DELETE
        /// </summary>
        ECOMMERCE_DELETE = 1348,
        /// <summary>
        /// PERMISSION FOR CAT.(13) ECOMMERCE, INSERT
        /// </summary>
        ECOMMERCE_INSERT = 1347,
        /// <summary>
        /// PERMISSION FOR CAT.(13) ECOMMERCE, UPDATE
        /// </summary>
        ECOMMERCE_UPDATE = 1346,
        /// <summary>
        /// PERMISSION FOR CAT.(1) GENERAL, ACCESS
        /// </summary>
        GENERAL_ACCESS = 1,
        /// <summary>
        /// PERMISSION FOR CAT.(11) INVOICES, ACCESS
        /// </summary>
        INVOICES_ACCESS = 1135,
        /// <summary>
        /// PERMISSION FOR CAT.(11) INVOICES, ACCESS_REPORTS
        /// </summary>
        INVOICES_ACCESS_REPORTS = 1139,
        /// <summary>
        /// PERMISSION FOR CAT.(11) INVOICES, DELETE
        /// </summary>
        INVOICES_DELETE = 1138,
        /// <summary>
        /// PERMISSION FOR CAT.(11) INVOICES, INSERT
        /// </summary>
        INVOICES_INSERT = 1137,
        /// <summary>
        /// PERMISSION FOR CAT.(11) INVOICES, UPDATE
        /// </summary>
        INVOICES_UPDATE = 1136,
        /// <summary>
        /// For non valid Enum when search for enum string value 
        /// </summary>
        NoValidEnum = -1,
        /// <summary>
        /// PERMISSION FOR CAT.(15) OPPORTUNITIES, ACCESS
        /// </summary>
        OPPORTUNITIES_ACCESS = 1555,
        /// <summary>
        /// PERMISSION FOR CAT.(15) OPPORTUNITIES, ACCESS_REPORTS
        /// </summary>
        OPPORTUNITIES_ACCESS_REPORTS = 1559,
        /// <summary>
        /// PERMISSION FOR CAT.(15) OPPORTUNITIES, DELETE
        /// </summary>
        OPPORTUNITIES_DELETE = 1558,
        /// <summary>
        /// PERMISSION FOR CAT.(15) OPPORTUNITIES, INSERT
        /// </summary>
        OPPORTUNITIES_INSERT = 1557,
        /// <summary>
        /// PERMISSION FOR CAT.(15) OPPORTUNITIES, UPDATE
        /// </summary>
        OPPORTUNITIES_UPDATE = 1556,
        /// <summary>
        /// PERMISSION FOR CAT.(10) ORDERS, ACCESS
        /// </summary>
        ORDERS_ACCESS = 1030,
        /// <summary>
        /// PERMISSION FOR CAT.(10) ORDERS, ACCESS_REPORTS
        /// </summary>
        ORDERS_ACCESS_REPORTS = 1034,
        /// <summary>
        /// PERMISSION FOR CAT.(10) ORDERS, DELETE
        /// </summary>
        ORDERS_DELETE = 1033,
        /// <summary>
        /// PERMISSION FOR CAT.(10) ORDERS, INSERT
        /// </summary>
        ORDERS_INSERT = 1032,
        /// <summary>
        /// PERMISSION FOR CAT.(10) ORDERS, UPDATE
        /// </summary>
        ORDERS_UPDATE = 1031,
        /// <summary>
        /// PERMISSION FOR CAT.(14) POINTOFSALES, ACCESS
        /// </summary>
        POINTOFSALES_ACCESS = 1450,
        /// <summary>
        /// PERMISSION FOR CAT.(14) POINTOFSALES, ACCESS_REPORTS
        /// </summary>
        POINTOFSALES_ACCESS_REPORTS = 1454,
        /// <summary>
        /// PERMISSION FOR CAT.(14) POINTOFSALES, DELETE
        /// </summary>
        POINTOFSALES_DELETE = 1453,
        /// <summary>
        /// PERMISSION FOR CAT.(14) POINTOFSALES, INSERT
        /// </summary>
        POINTOFSALES_INSERT = 1452,
        /// <summary>
        /// PERMISSION FOR CAT.(14) POINTOFSALES, UPDATE
        /// </summary>
        POINTOFSALES_UPDATE = 1451,
        /// <summary>
        /// PERMISSION FOR CAT.(5) PROSPECTS, ACCESS
        /// </summary>
        PROSPECTS_ACCESS = 505,
        /// <summary>
        /// PERMISSION FOR CAT.(5) PROSPECTS, ACCESS_REPORTS
        /// </summary>
        PROSPECTS_ACCESS_REPORTS = 509,
        /// <summary>
        /// PERMISSION FOR CAT.(5) PROSPECTS, DELETE
        /// </summary>
        PROSPECTS_DELETE = 508,
        /// <summary>
        /// PERMISSION FOR CAT.(5) PROSPECTS, INSERT
        /// </summary>
        PROSPECTS_INSERT = 507,
        /// <summary>
        /// PERMISSION FOR CAT.(5) PROSPECTS, UPDATE
        /// </summary>
        PROSPECTS_UPDATE = 506,
        /// <summary>
        /// PERMISSION FOR CAT.(16) QUOTES, ACCESS
        /// </summary>
        QUOTES_ACCESS = 1660,
        /// <summary>
        /// PERMISSION FOR CAT.(16) QUOTES, ACCESS_REPORTS
        /// </summary>
        QUOTES_ACCESS_REPORTS = 1664,
        /// <summary>
        /// PERMISSION FOR CAT.(16) QUOTES, DELETE
        /// </summary>
        QUOTES_DELETE = 1663,
        /// <summary>
        /// PERMISSION FOR CAT.(16) QUOTES, INSERT
        /// </summary>
        QUOTES_INSERT = 1662,
        /// <summary>
        /// PERMISSION FOR CAT.(16) QUOTES, UPDATE
        /// </summary>
        QUOTES_UPDATE = 1661,
        /// <summary>
        /// PERMISSION FOR CAT.(12) RECEIVABLES, ACCESS
        /// </summary>
        RECEIVABLES_ACCESS = 1240,
        /// <summary>
        /// PERMISSION FOR CAT.(12) RECEIVABLES, ACCESS_REPORTS
        /// </summary>
        RECEIVABLES_ACCESS_REPORTS = 1244,
        /// <summary>
        /// PERMISSION FOR CAT.(12) RECEIVABLES, DELETE
        /// </summary>
        RECEIVABLES_DELETE = 1243,
        /// <summary>
        /// PERMISSION FOR CAT.(12) RECEIVABLES, INSERT
        /// </summary>
        RECEIVABLES_INSERT = 1242,
        /// <summary>
        /// PERMISSION FOR CAT.(12) RECEIVABLES, UPDATE
        /// </summary>
        RECEIVABLES_UPDATE = 1241,
        /// <summary>
        /// PERMISSION FOR CAT.(17) SALESREPORTS, ACCESS
        /// </summary>
        SALESREPORTS_ACCESS = 1765,
        /// <summary>
        /// PERMISSION FOR CAT.(17) SALESREPORTS, ACCESS_REPORTS
        /// </summary>
        SALESREPORTS_ACCESS_REPORTS = 1769,
        /// <summary>
        /// PERMISSION FOR CAT.(17) SALESREPORTS, DELETE
        /// </summary>
        SALESREPORTS_DELETE = 1768,
        /// <summary>
        /// PERMISSION FOR CAT.(17) SALESREPORTS, INSERT
        /// </summary>
        SALESREPORTS_INSERT = 1767,
        /// <summary>
        /// PERMISSION FOR CAT.(17) SALESREPORTS, UPDATE
        /// </summary>
        SALESREPORTS_UPDATE = 1766,
        /// <summary>
        /// PERMISSION FOR CAT.(6) TICKETS, ACCESS
        /// </summary>
        TICKETS_ACCESS = 610,
        /// <summary>
        /// PERMISSION FOR CAT.(6) TICKETS, ACCESS_REPORTS
        /// </summary>
        TICKETS_ACCESS_REPORTS = 614,
        /// <summary>
        /// PERMISSION FOR CAT.(6) TICKETS, DELETE
        /// </summary>
        TICKETS_DELETE = 613,
        /// <summary>
        /// PERMISSION FOR CAT.(6) TICKETS, INSERT
        /// </summary>
        TICKETS_INSERT = 612,
        /// <summary>
        /// PERMISSION FOR CAT.(6) TICKETS, UPDATE
        /// </summary>
        TICKETS_UPDATE = 611
    }


}