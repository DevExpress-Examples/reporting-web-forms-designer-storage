Imports System
Imports System.Data
Imports System.Web.UI.WebControls
Imports SimpleWebReportCatalog.CatalogDataTableAdapters
' ...


Namespace SimpleWebReportCatalog
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Private catalogDataSet As CatalogData
        Private reportsTable As DataTable
        Private reportsTableAdapter As ReportsTableAdapter


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            catalogDataSet = New CatalogData()
            reportsTableAdapter = New ReportsTableAdapter()
            reportsTableAdapter.Fill(catalogDataSet.Reports)
            reportsTable = catalogDataSet.Tables("Reports")

            If Not IsPostBack Then
                reportsList.DataSource = catalogDataSet
                reportsList.DataMember = "Reports"
                reportsList.DataTextField = "DisplayName"
                reportsList.DataValueField = "ReportID"
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
                    reportsTableAdapter.Update(catalogDataSet)
                    catalogDataSet.AcceptChanges()
                End If
                reportsList.Items.Remove(reportsList.SelectedItem)
            End If
        End Sub
    End Class
End Namespace