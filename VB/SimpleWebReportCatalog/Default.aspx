<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SimpleWebReportCatalog.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="CatalogForm" runat="server">
		<div>
			<h1>Report Catalog </h1>
			<p>Use the form below to manage reports in the catalog.</p>
		</div>
		<div>
			<asp:ListBox ID="reportsList" runat="server" Width="600px" Height="150px"></asp:ListBox>
		</div>
		<div>
			<asp:Button CssClass="catalogButton" ID="editButton" runat="server" Text="Edit"
				OnClick="EditButton_Click" />
			<asp:Button CssClass="catalogButton" ID="deleteButton" runat="server" Text="Delete"
				OnClick="DeleteButton_Click" />
		</div>
		<hr />
		<div>
			<asp:Button CssClass="catalogButton" ID="newReportButton" runat="server"
				Text="New Report" OnClick="NewReportButton_Click" />
		</div>


	</form>
</body>
</html>