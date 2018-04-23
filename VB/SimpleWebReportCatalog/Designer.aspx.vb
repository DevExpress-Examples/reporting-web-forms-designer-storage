#Region "#DesignerUsings"
Imports System
Imports System.Web
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
' ...
#End Region ' #DesignerUsings

Namespace SimpleWebReportCatalog
    Partial Public Class Designer
        Inherits System.Web.UI.Page

        Private Shared reportStorage As New ReportStorage()
        Private task As DesignerTask
        Private model As ReportModel


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            task = DirectCast(Session("DesignerTask"), DesignerTask)
            If task IsNot Nothing Then
                InitDesignerPage()
            ElseIf Not Page.IsCallback Then
                Response.Redirect("Default.aspx")
            End If
        End Sub


        Private Sub InitDesignerPage()
            BindToData()

            Select Case task.mode
                Case ReportEdditingMode.NewReport
                    ' Create a new report from the template.
                    model = New ReportModel()
                    model.ReportId = task.reportID
                    ASPxReportDesigner1.OpenReport(New ReportTemplate With {.Name = model.ReportId})
                Case ReportEdditingMode.ModifyReport
                    ' Load an existing report from the catalog database.
                    model = reportStorage.GetReport(task.reportID)
                    ASPxReportDesigner1.OpenReportXmlLayout(model.LayoutData)
            End Select
        End Sub


        Protected Sub ASPxReportDesigner1_SaveReportLayout(ByVal sender As Object, ByVal e As DevExpress.XtraReports.Web.SaveReportLayoutEventArgs)
            ' If a report is new, write it to a new database record, othervise update the existing record.
            If task.mode = ReportEdditingMode.NewReport Then
                model.LayoutData = e.ReportLayout
                reportStorage.WriteReport(model)
                task.mode = ReportEdditingMode.ModifyReport
            Else
                model.LayoutData = e.ReportLayout
                reportStorage.UpdateReport(model)
            End If
        End Sub


        #Region "#BindToData"
        Private Sub BindToData()
            ' Create a SQL data source with the specified connection parameters.
            Dim connectionParameters As New Access97ConnectionParameters(HttpRuntime.AppDomainAppPath & "App_Data\nwind.mdb", "", "")
            Dim ds As New SqlDataSource(connectionParameters)

            ' Create a custom SQL query to access the Products data table.
            Dim query As New CustomSqlQuery()
            query.Name = "Product"
            query.Sql = "SELECT * FROM Products"
            ds.Queries.Add(query)
            ds.RebuildResultSchema()

            ' Add the created data source to the list of default data sources. 
            ASPxReportDesigner1.DataSources.Add("Northwind", ds)
        End Sub
        #End Region ' #BindToData
    End Class
End Namespace