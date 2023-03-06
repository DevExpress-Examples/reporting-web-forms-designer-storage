'#Region "#DesignerUsings"
Imports System
Imports System.Web
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql

' ...
'#End Region  ' #DesignerUsings
Namespace SimpleWebReportCatalog

    Public Partial Class Designer
        Inherits UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim task As DesignerTask = CType(Session("DesignerTask"), DesignerTask)
            If task IsNot Nothing Then
                InitDesignerPage(task)
            ElseIf Not Page.IsCallback Then
                Response.Redirect("Default.aspx")
            End If
        End Sub

        Private Sub InitDesignerPage(ByVal task As DesignerTask)
            BindToData()
            Select Case task.mode
                Case ReportEdditingMode.NewReport
                    ' Create a new report from the template.
                    ASPxReportDesigner1.OpenReport(New ReportTemplate())
                Case ReportEdditingMode.ModifyReport
                    ' Load an existing report from the report storage.
                    ASPxReportDesigner1.OpenReport(task.reportID)
            End Select
        End Sub

'#Region "#BindToData"
        Private Sub BindToData()
            ' Create a SQL data source with the specified connection parameters.
            Dim connectionParameters As Access97ConnectionParameters = New Access97ConnectionParameters(HttpRuntime.AppDomainAppPath & "App_Data\nwind.mdb", "", "")
            Dim ds As SqlDataSource = New SqlDataSource(connectionParameters)
            ' Create a custom SQL query to access the Products data table.
            Dim query As CustomSqlQuery = New CustomSqlQuery()
            query.Name = "Products"
            query.Sql = "SELECT * FROM Products"
            ds.Queries.Add(query)
            ds.RebuildResultSchema()
            ' Add the created data source to the list of default data sources. 
            ASPxReportDesigner1.DataSources.Add("Northwind", ds)
        End Sub
'#End Region  ' #BindToData
    End Class
End Namespace
