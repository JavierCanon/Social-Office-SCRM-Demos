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
namespace SO.Enums
{

    /// <summary>
    /// Modules of the app
    /// </summary>
    public enum EnumSoftModules : int
    {
        Need4SpeedCMS
        ,
        Web4Clients
        ,
        SO
        ,
        Web4Emails
        ,
        Web4Payments
        ,
        Web4Sales
        ,
        AnalistaVirtual

    }

    /// <summary>
    /// Submodules need to have same value that DB.
    /// </summary>
    public enum EnumSoftSubModules : int
    {
        /*
        1	CONTACTOS
2	FACTURACION
3	SERVICIO AL CLIENTE
4	VENTAS
5	ECOMMERCE
6	ADMINISTRADOR CONTENIDOS
7	REDES SOCIALES
8	EMAIL MARKETING
9	PROYECTOS
10	TAREAS
11	ADMINISTRACION CRM
        */
        Contacts = 1, //CONTACTOS
        Invoice = 2, //FACTURACION
        ServiceClient = 3, //SERVICIO AL CLIENTE
        Sales = 4, //VENTAS
        Ecommerce = 5, //ECOMMERCE
        CMS = 6, //ADMINISTRADOR CONTENIDOS
        SocialNetworks = 7, //REDES SOCIALES
        EmailMarketing = 8, //EMAIL MARKETING
        Projects = 9, //PROYECTOS
        Tasks = 10, //TAREAS
        AdminCRM = 11 //ADMINISTRACION CRM

    }


    public enum EnumMenuCategory : int
    {
        None
       ,
        Dashboards
       ,
        Query
       ,
        Report
       ,
        Sales
       ,
        Help
            ,
        AnalistaVirtual
            ,
        Geomarketing
        ,
        Chart
            ,
        Queries
        ,
        Tasks
        ,
        Import
    }

    public enum EnumSubMenuCategory : int
    {
        None
            ,
        Dashboard
            ,
        Dashboards_Sales
            ,
        Dashboards_Financial
            ,
        Dashboards_Administrative

            ,
        AVPivotReports
            ,
        Geomarketing_Maps
            ,
        Geomarketing_Search
            ,
        Geomarketing_Demography

            ,
        Chart_Sales
            ,
        Chart_Financial
            ,
        Chart_Administrative
            ,
        Queries_Sales
            ,
        Queries_Financial
            ,
        Queries_Administrative
        ,
        Tasks_UserTasks
        ,
        Import_Tables
    }


    public enum MessageAlertType
    {
        Danger,
        Info,
        Success,
        Warning
    }

}
