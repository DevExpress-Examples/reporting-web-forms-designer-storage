Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Namespace SimpleWebReportCatalog
	Public Class CustomReportStorageWebExtension
		Inherits DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension

		Private reportsTable As New DataTable()
		Private reportsTableAdapter As SqlDataAdapter
		Private connectionString As String = "Data Source=localhost;Initial Catalog=Reports;Integrated Security=True"
		Public Sub New()
			reportsTableAdapter = New SqlDataAdapter("Select * from ReportLayout", New SqlConnection(connectionString))
			Dim builder As New SqlCommandBuilder(reportsTableAdapter)
			reportsTableAdapter.InsertCommand = builder.GetInsertCommand()
			reportsTableAdapter.UpdateCommand = builder.GetUpdateCommand()
			reportsTableAdapter.DeleteCommand = builder.GetDeleteCommand()
			reportsTableAdapter.Fill(reportsTable)
			Dim keyColumns(0) As DataColumn
			keyColumns(0) = reportsTable.Columns(0)
			reportsTable.PrimaryKey = keyColumns
		End Sub
		Public Overrides Function CanSetData(ByVal url As String) As Boolean
			Return True
		End Function
		Public Overrides Function GetData(ByVal url As String) As Byte()
			' Get the report data from the storage.
			Dim row As DataRow = reportsTable.Rows.Find(Integer.Parse(url))
			If row Is Nothing Then
				Return Nothing
			End If

			Dim reportData() As Byte = DirectCast(row("LayoutData"), Byte())
			Return reportData
		End Function
		Public Overrides Function GetUrls() As Dictionary(Of String, String)
			reportsTable.Clear()
			reportsTableAdapter.Fill(reportsTable)
			' Get URLs and display names for all reports available in the storage.
			Dim v = reportsTable.AsEnumerable().ToDictionary(Function(dataRow) CInt(Fix(dataRow("ReportId"))).ToString(), Function(dataRow) CStr(dataRow("DisplayName")))
			Return v
		End Function
		Public Overrides Function IsValidUrl(ByVal url As String) As Boolean
			Return True
		End Function
		Public Overrides Sub SetData(ByVal report As XtraReport, ByVal url As String)
			' Write a report to the storage under the specified URL.
			Dim row As DataRow = reportsTable.Rows.Find(Integer.Parse(url))
			If row IsNot Nothing Then
				Using ms As New MemoryStream()
					report.SaveLayoutToXml(ms)
					row("LayoutData") = ms.GetBuffer()
				End Using
				reportsTableAdapter.Update(reportsTable)
			End If
		End Sub
		Public Overrides Function SetNewData(ByVal report As XtraReport, ByVal defaultUrl As String) As String
			' Save a report to the storage with a new URL. 
			' The defaultUrl parameter is the report name that the user specifies.
			Dim row As DataRow = reportsTable.NewRow()
			row("ReportId") = 0
			row("DisplayName") = defaultUrl
			Using ms As New MemoryStream()
				report.SaveLayoutToXml(ms)
				row("LayoutData") = ms.GetBuffer()
			End Using
			reportsTable.Rows.Add(row)
			reportsTableAdapter.Update(reportsTable)
			' Refill the dataset to obtain the actual value of the new row's autoincrement key field.
			reportsTable.Clear()
			reportsTableAdapter.Fill(reportsTable)
			Return reportsTable.AsEnumerable().FirstOrDefault(Function(x) x("DisplayName").ToString() = defaultUrl)("ReportId").ToString()
		End Function
	End Class
End Namespace