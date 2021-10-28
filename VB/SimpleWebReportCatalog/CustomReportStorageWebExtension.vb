'#Region "#CustomReportStorage"
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports DevExpress.XtraReports.UI
Imports SimpleWebReportCatalog.CatalogDataTableAdapters

' ...
Namespace SimpleWebReportCatalog

    Public Class CustomReportStorageWebExtension
        Inherits DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension

        Private catalogDataSet As CatalogData

        Private reportsTable As DataTable

        Private reportsTableAdapter As ReportsTableAdapter

        Public Sub New()
            catalogDataSet = New CatalogData()
            reportsTableAdapter = New ReportsTableAdapter()
            reportsTableAdapter.Fill(catalogDataSet.Reports)
            reportsTable = catalogDataSet.Tables("Reports")
        End Sub

        Public Overrides Function CanSetData(ByVal url As String) As Boolean
            ' Check if the URL is available in the report storage.
            Return reportsTable.Rows.Find(Integer.Parse(url)) IsNot Nothing
        End Function

        Public Overrides Function GetData(ByVal url As String) As Byte()
            ' Get the report data from the storage.
            Dim row As DataRow = reportsTable.Rows.Find(Integer.Parse(url))
            If row Is Nothing Then Return Nothing
            Dim reportData As Byte() = CType(row("LayoutData"), Byte())
            Return reportData
        End Function

        Public Overrides Function GetUrls() As Dictionary(Of String, String)
            ' Get URLs and display names for all reports available in the storage.
            Return reportsTable.AsEnumerable().ToDictionary(Of DataRow, String, String)(Function(dataRow)(CInt(dataRow(CStr("ReportID")))).ToString(), Function(dataRow) CStr(dataRow("DisplayName")))
        End Function

        Public Overrides Function IsValidUrl(ByVal url As String) As Boolean
            ' Check if the specified URL is valid for the current report storage.
            ' In this example, a URL should be a string containing a numeric value that is used as a data row primary key.
            Dim n As Integer
            Return Integer.TryParse(url, n)
        End Function

        Public Overrides Sub SetData(ByVal report As XtraReport, ByVal url As String)
            ' Write a report to the storage under the specified URL.
            Dim row As DataRow = reportsTable.Rows.Find(Integer.Parse(url))
            If row IsNot Nothing Then
                Using ms As MemoryStream = New MemoryStream()
                    report.SaveLayoutToXml(ms)
                    row("LayoutData") = ms.GetBuffer()
                End Using

                reportsTableAdapter.Update(catalogDataSet)
                catalogDataSet.AcceptChanges()
            End If
        End Sub

        Public Overrides Function SetNewData(ByVal report As XtraReport, ByVal defaultUrl As String) As String
            ' Save a report to the storage under a new URL. 
            ' The defaultUrl parameter contains the report display name specified by a user.
            Dim row As DataRow = reportsTable.NewRow()
            row("DisplayName") = defaultUrl
            Using ms As MemoryStream = New MemoryStream()
                report.SaveLayoutToXml(ms)
                row("LayoutData") = ms.GetBuffer()
            End Using

            reportsTable.Rows.Add(row)
            reportsTableAdapter.Update(catalogDataSet)
            catalogDataSet.AcceptChanges()
            ' Refill the dataset to obtain the actual value of the new row's autoincrement key field.
            reportsTableAdapter.Fill(catalogDataSet.Reports)
            Return Enumerable.FirstOrDefault(Of CatalogData.ReportsRow)(catalogDataSet.Reports, CType(Function(x) CBool(Equals(x.DisplayName, defaultUrl)), Func(Of CatalogData.ReportsRow, Boolean))).ReportID.ToString()
        End Function
    End Class
End Namespace
'#End Region  ' #CustomReportStorage
