<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer.aspx.cs" Inherits="SimpleWebReportCatalog.Designer" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web.ClientControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/DesignerStyle.css" />
    <script type="text/javascript">
        // The CustomizeMenuActions event handler.
        function CustomizeMenuActions(s, e) {
            var actions = e.Actions;
            // Register the custom Save&Close menu command.
            actions.push({
                text: "Save&Close",
                imageClassName: "customButton",
                disabled: ko.observable(false),
                visible: true,
                // The clickAction function recieves the client-side report model
                // allowing you interact with the currently opened report.
                clickAction: function(report) {
                    report.save();
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
            <dx:ASPxReportDesigner
                ID="ASPxReportDesigner1" runat="server"
                OnSaveReportLayout="ASPxReportDesigner1_SaveReportLayout"
                ClientSideEvents-CustomizeMenuActions="CustomizeMenuActions">
            </dx:ASPxReportDesigner>
        </div>
    </form>
</body>
</html>
