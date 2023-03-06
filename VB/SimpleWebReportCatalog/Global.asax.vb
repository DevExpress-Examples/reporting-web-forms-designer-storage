Imports System
Imports System.Web

Namespace SimpleWebReportCatalog

    Public Class [Global]
        Inherits HttpApplication

        Protected Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(New CustomReportStorageWebExtension())
        End Sub

        Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        End Sub
    End Class
End Namespace
