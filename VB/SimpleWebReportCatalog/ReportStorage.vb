Imports System
Imports System.Data
Imports SimpleWebReportCatalog.CatalogDataTableAdapters
Imports CatalogDataTableAdapters

' ...

Namespace SimpleWebReportCatalog
    Public Class ReportStorage
        Private catalogDataSet As CatalogData
        Private reportsTable As DataTable
        Private reportsTableAdapter As ReportsTableAdapter


        Public Sub New()
            catalogDataSet = New CatalogData()
            reportsTableAdapter = New ReportsTableAdapter()
            reportsTableAdapter.Fill(catalogDataSet.Reports)

            reportsTable = catalogDataSet.Tables("Reports")
        End Sub


        Public Sub WriteReport(ByVal reportModel As ReportModel)
            Dim row As DataRow = reportsTable.NewRow()

            row("ReportID") = reportModel.ReportId
            row("LayoutData") = reportModel.LayoutData

            reportsTable.Rows.Add(row)
            reportsTableAdapter.Update(catalogDataSet)
            catalogDataSet.AcceptChanges()
        End Sub


        Public Function GetReport(ByVal reportId As String) As ReportModel
            Dim row As DataRow = reportsTable.Rows.Find(reportId)

            If row IsNot Nothing Then
                Dim model As New ReportModel()
                model.ReportId = DirectCast(row("ReportID"), String)
                model.LayoutData = DirectCast(row("LayoutData"), Byte())
                Return model
            Else
                Return Nothing
            End If
        End Function


        Public Sub UpdateReport(ByVal reportModel As ReportModel)
            Dim row As DataRow = reportsTable.Rows.Find(reportModel.ReportId)

            If row IsNot Nothing Then
                row("LayoutData") = reportModel.LayoutData
                reportsTableAdapter.Update(catalogDataSet)
                catalogDataSet.AcceptChanges()
            End If
        End Sub


        Public Sub RemoveReport(ByVal reportId As String)
            Dim row As DataRow = reportsTable.Rows.Find(reportId)

            If row IsNot Nothing Then
                row.Delete()
                reportsTableAdapter.Update(catalogDataSet)
                catalogDataSet.AcceptChanges()
            End If
        End Sub
    End Class
End Namespace







