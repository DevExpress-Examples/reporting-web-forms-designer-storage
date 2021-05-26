<!-- default file list -->
*Files to look at*:

* [CustomReportStorageWebExtension.cs](./CS/SimpleWebReportCatalog/App_Code/CustomReportStorageWebExtension.cs) (VB: [CustomReportStorageWebExtension.vb](./VB/SimpleWebReportCatalog/App_Code/CustomReportStorageWebExtension.vb))
* [Default.aspx](./CS/SimpleWebReportCatalog/Default.aspx) (VB: [Default.aspx](./VB/SimpleWebReportCatalog/Default.aspx))
* [Default.aspx.cs](./CS/SimpleWebReportCatalog/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/SimpleWebReportCatalog/Default.aspx.vb))
* [Designer.aspx](./CS/SimpleWebReportCatalog/Designer.aspx) (VB: [Designer.aspx](./VB/SimpleWebReportCatalog/Designer.aspx))
* [Designer.aspx.cs](./CS/SimpleWebReportCatalog/Designer.aspx.cs) (VB: [Designer.aspx.vb](./VB/SimpleWebReportCatalog/Designer.aspx.vb))
* [DesignerTask.cs](./CS/SimpleWebReportCatalog/DesignerTask.cs) (VB: [DesignerTask.vb](./VB/SimpleWebReportCatalog/DesignerTask.vb))
* [Global.asax.cs](./CS/SimpleWebReportCatalog/Global.asax.cs) (VB: [Global.asax.vb](./VB/SimpleWebReportCatalog/Global.asax.vb))
<!-- default file list end -->

# How to integrate the Web Report Designer into a web application and Add, Edit, and Remove Reports from a Database storage

## Overview
The example demonstrates how to integrate the [End-User Report Designer](https://docs.devexpress.com/XtraReports/17103/web-reporting/asp-net-webforms-reporting/end-user-report-designer) into an ASP.NET WebForms application and implement a **Microsoft SQL Server database [report storage](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension)** to **add**, **edit** and **delete** reports. This example also demonstrates how to add [custom commands to the report designer menu](https://docs.devexpress.com/XtraReports/17626/web-reporting/asp-net-webforms-reporting/end-user-report-designer/customization/customize-the-report-designer-toolbar) at runtime. A custom **Close** menu command redirects you to the homepage.

**Note**
The report storage implementation is for demonstration purposes only. Create your own implementation for use in production.

## Before you start
* Create a **Reports** database in the local Microsoft SQL Server. Add the **ReportLayout** table with the following script:
### SQL
```SQL
USE [Reports]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportLayout](
     [ReportId] [int] IDENTITY(1,1) NOT NULL,
     [DisplayName] [nvarchar](https://github.com/DevExpress-Examples/Reporting_how-to-integrate-the-web-report-designer-into-a-web-application-t178798/blob/19.2.3%2B/50) NULL,
     [LayoutData] [varbinary](https://github.com/DevExpress-Examples/Reporting_how-to-integrate-the-web-report-designer-into-a-web-application-t178798/blob/19.2.3%2B/max) NULL,
     [ReportId] ASC
CONSTRAINT [PK_ReportLayout6] PRIMARY KEY CLUSTERED 
(
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =  OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```
### SQL Server 2016 - v13.0.x.x
```SQL
USE [Reports]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportLayout](
        [ReportId] [int] IDENTITY(1,1) NOT NULL ,
        [DisplayName] [nvarchar](50) NULL,
        [LayoutData] [varbinary](max) NULL,
        CONSTRAINT PK_ReportLayout PRIMARY KEY (ReportId)
);
SELECT * 
FROM [dbo].[ReportLayout]
ORDER BY [ReportId] ASC        
GO
```

* Add the [Northwind database](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs) to your local MS SQL server. 

## See also
* [How to Integrate Web Report Designer in an MVC Web Application](https://www.devexpress.com/Support/Center/p/T190370)
* [ASPxReportDesigner - How to Create an ASP.NET End-User Reporting Application with the File Report Storage Managed by the ASPxFileManager Control](https://www.devexpress.com/Support/Center/p/T227679)


