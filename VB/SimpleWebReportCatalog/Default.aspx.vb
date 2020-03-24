Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
' ...


Namespace SimpleWebReportCatalog
	Partial Public Class [Default]
		Inherits System.Web.UI.Page

		Private reportsTable As New DataTable()
		Private connectionString As String = "Data Source=localhost;Initial Catalog=Reports;Integrated Security=True"
		Private reportsTableAdapter As SqlDataAdapter

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

			reportsTableAdapter = New SqlDataAdapter("Select * from ReportLayout", New SqlConnection(connectionString))
			Dim builder As New SqlCommandBuilder(reportsTableAdapter)
			reportsTableAdapter.InsertCommand = builder.GetInsertCommand()
			reportsTableAdapter.UpdateCommand = builder.GetUpdateCommand()
			reportsTableAdapter.DeleteCommand = builder.GetDeleteCommand()
			reportsTableAdapter.Fill(reportsTable)
			Dim keyColumns(0) As DataColumn
			keyColumns(0) = reportsTable.Columns(0)
			reportsTable.PrimaryKey = keyColumns

			If Not IsPostBack Then
				reportsList.DataSource = reportsTable
				reportsList.DataMember = "Reports"
				reportsList.DataTextField = "DisplayName"
				reportsList.DataValueField = "ReportId"
				Me.DataBind()
			End If
		End Sub


		Protected Sub NewReportButton_Click(ByVal sender As Object, ByVal e As EventArgs)
			Session("DesignerTask") = New DesignerTask With {.mode = ReportEdditingMode.NewReport}
			Response.Redirect("Designer.aspx")
		End Sub


		Protected Sub EditButton_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim selected As ListItem = reportsList.SelectedItem
			If selected IsNot Nothing Then
				Session("DesignerTask") = New DesignerTask With {.mode = ReportEdditingMode.ModifyReport, .reportID = selected.Value}
				Session("ReportID") = selected.Value
				Response.Redirect("Designer.aspx")
			End If
		End Sub


		Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim selected As ListItem = reportsList.SelectedItem

			If selected IsNot Nothing Then
				Dim row As DataRow = reportsTable.Rows.Find(Integer.Parse(selected.Value))
				If row IsNot Nothing Then
					row.Delete()
					reportsTableAdapter.Update(reportsTable)
					reportsTable.AcceptChanges()
				End If
				reportsList.Items.Remove(reportsList.SelectedItem)
			End If
		End Sub
	End Class
End Namespace