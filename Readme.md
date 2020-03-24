<!-- default file list -->
*Files to look at*:

* [CustomReportStorageWebExtension.cs](./CS/SimpleWebReportCatalog/CustomReportStorageWebExtension.cs) (VB: [CustomReportStorageWebExtension.vb](./VB/SimpleWebReportCatalog/CustomReportStorageWebExtension.vb))
* [Default.aspx](./CS/SimpleWebReportCatalog/Default.aspx) (VB: [Default.aspx](./VB/SimpleWebReportCatalog/Default.aspx))
* [Default.aspx.cs](./CS/SimpleWebReportCatalog/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/SimpleWebReportCatalog/Default.aspx.vb))
* [Designer.aspx](./CS/SimpleWebReportCatalog/Designer.aspx) (VB: [Designer.aspx](./VB/SimpleWebReportCatalog/Designer.aspx))
* [Designer.aspx.cs](./CS/SimpleWebReportCatalog/Designer.aspx.cs) (VB: [Designer.aspx.vb](./VB/SimpleWebReportCatalog/Designer.aspx.vb))
* [DesignerTask.cs](./CS/SimpleWebReportCatalog/DesignerTask.cs) (VB: [DesignerTask.vb](./VB/SimpleWebReportCatalog/DesignerTask.vb))
* [Global.asax.cs](./CS/SimpleWebReportCatalog/Global.asax.cs) (VB: [Global.asax.vb](./VB/SimpleWebReportCatalog/Global.asax.vb))
<!-- default file list end -->

# How to Integrate Web Report Designer in a Web Application

The application allows you to add, delete, and edit reports stored a Microsoft SQL Server database. A project implements the [ReportStorageWebExtension](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension) descendant to access the database.

This example also demonstrates how to add custom commands to the report designer menu at runtime. A custom _Close_ menu command redirects you to the homepage.

> Before you start:
>- Create the **Reports** database in the local Microsoft SQL Server. Add the **ReportLayout** table with the following script:
>
>   ```SQL
>   USE [Reports]
>   GO
>   SET ANSI_NULLS ON
>   GO
>   SET QUOTED_IDENTIFIER ON
>   GO
>   CREATE TABLE [dbo].[ReportLayout](
>	    [ReportId] [int] IDENTITY(1,1) NOT NULL,
>	    [DisplayName] [nvarchar](50) NULL,
>	    [LayoutData] [varbinary](max) NULL,
>	    [ReportId] ASC
>   CONSTRAINT [PK_ReportLayout6] PRIMARY KEY CLUSTERED 
>   (
>   )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =  OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
>   ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
>   GO
>   ```
>- Add the [Northwind database](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs) to your local MS SQL server. 

**See also:**
* [How to Integrate Web Report Designer in an MVC Web Application<](https://www.devexpress.com/Support/Center/p/T190370)
* [ASPxReportDesigner - How to Create an ASP.NET End-User Reporting Application with the File Report Storage Managed by the ASPxFileManager Control](https://www.devexpress.com/Support/Center/p/T227679)


