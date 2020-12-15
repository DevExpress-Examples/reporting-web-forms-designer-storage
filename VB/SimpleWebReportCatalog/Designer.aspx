<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Designer.aspx.vb" Inherits="SimpleWebReportCatalog.Designer" %>

<%@ Register Assembly="DevExpress.XtraReports.v19.2.Web.WebForms, Version=19.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link rel="stylesheet" href="Styles/DesignerStyle.css" />
	<script type="text/javascript">
		// The CustomizeMenuActions event handler.
		function CustomizeMenuActions(s, e) {
			var actions = e.Actions;
			// Register the custom Close menu command.
			actions.push({
				text: "Close",
				imageClassName: "customButton",
				disabled: ko.observable(false),
				visible: true,
				// The clickAction function recieves the client-side report model
				// and allows you to interact with the opened report.
				clickAction: function (report) {
					window.location = "Default.aspx";
				},
				container: "menu"
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server" ClientSideEvents-CustomizeMenuActions="CustomizeMenuActions">
		</dx:ASPxReportDesigner>
	</div>
	</form>
</body>
</html>