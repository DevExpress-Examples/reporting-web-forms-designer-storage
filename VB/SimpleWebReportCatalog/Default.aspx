<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SimpleWebReportCatalog.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/CatalogStyle.css" />
</head>
<body>
    <form id="CatalogForm" runat="server">
    <div>
        <h1>Report Catalog </h1>
        <p>Use the form below to manage reports in the catalog.</p>
    </div>
    <div>
        <asp:ListBox ID="ReportsList" runat="server" Width="600px" 
            DataSourceID="AccessDataSource1" DataTextField="ReportID" 
            DataValueField="ReportID" Height="150px" >
        </asp:ListBox>        
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/catalog.mdb" SelectCommand="SELECT * FROM [Reports]">
        </asp:AccessDataSource>
    </div>
    <div>  
        <asp:Button CssClass="catalogButton" ID="EditButton" runat="server" Text="Edit" 
            onclick="EditButton_Click" />
        <asp:Button CssClass="catalogButton" ID="DeleteButton" runat="server" Text="Delete" 
            onclick="DeleteButton_Click" />
    </div>
    <hr />
    <div>
        <asp:Button CssClass="catalogButton" ID="NewReportButton" runat="server" 
            Text="New Report" onclick="NewReportButton_Click" />

        <asp:Label ID="Label1" runat="server" Text="New Report ID: " style="margin:4px"></asp:Label>
        <asp:TextBox ID="ReportIDTextBox" runat="server" Width="373px"></asp:TextBox>
    </div>
    </form> 
</body>
</html>