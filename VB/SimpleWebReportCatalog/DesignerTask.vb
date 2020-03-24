Namespace SimpleWebReportCatalog
	Public Enum ReportEdditingMode
		NewReport
		ModifyReport
	End Enum

	Public Class DesignerTask
		Public Property mode() As ReportEdditingMode
		Public Property reportID() As String
	End Class
End Namespace