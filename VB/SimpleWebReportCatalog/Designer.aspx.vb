Imports System
Imports DevExpress.DataAccess.Sql

Namespace SimpleWebReportCatalog
	Partial Public Class Designer
		Inherits System.Web.UI.Page

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			Dim task As DesignerTask = DirectCast(Session("DesignerTask"), DesignerTask)
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
					' Load a report from the report storage.
					ASPxReportDesigner1.OpenReport(task.reportID)
			End Select
		End Sub

		Private Sub BindToData()
			Dim ds As New SqlDataSource("Northwind")
			Dim query As New CustomSqlQuery()
			query.Name = "Products"
			query.Sql = "SELECT * FROM Products"
			ds.Queries.Add(query)
			ds.RebuildResultSchema()

			ASPxReportDesigner1.DataSources.Add("Northwind", ds)
		End Sub
	End Class
End Namespace
