Imports System
Imports System.Web.UI.WebControls
' ...

Namespace SimpleWebReportCatalog
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        End Sub


        Protected Sub NewReportButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If ReportIDTextBox.Text <> "" Then
                Session("DesignerTask") = New DesignerTask With {.mode = ReportEdditingMode.NewReport, .reportID = ReportIDTextBox.Text}
                Response.Redirect("Designer.aspx")
            End If
        End Sub


        Protected Sub EditButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim selected As ListItem = ReportsList.SelectedItem
            If selected IsNot Nothing Then
                Session("DesignerTask") = New DesignerTask With {.mode = ReportEdditingMode.ModifyReport, .reportID = selected.Value}
                Session("ReportID") = selected.Value
                Response.Redirect("Designer.aspx")
            End If
        End Sub


        Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim selected As ListItem = ReportsList.SelectedItem
            If selected IsNot Nothing Then
                Dim reportStorage As New ReportStorage()
                reportStorage.RemoveReport(selected.Value)
                ReportsList.Items.Remove(ReportsList.SelectedValue)
            End If
        End Sub


    End Class
End Namespace






