using System;
using System.Web.UI.WebControls;
// ...

namespace SimpleWebReportCatalog {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void NewReportButton_Click(object sender, EventArgs e) {
            if(ReportIDTextBox.Text != "") {
                Session["DesignerTask"] = new DesignerTask {
                    mode = ReportEdditingMode.NewReport,
                    reportID = ReportIDTextBox.Text
                };
                Response.Redirect("Designer.aspx");
            }
        }

        protected void EditButton_Click(object sender, EventArgs e) {
            ListItem selected = ReportsList.SelectedItem;
            if(selected != null) {
                Session["DesignerTask"] = new DesignerTask {
                    mode = ReportEdditingMode.ModifyReport,
                    reportID = selected.Value
                };
                Session["ReportID"] = selected.Value;
                Response.Redirect("Designer.aspx");
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e) {
            ListItem selected = ReportsList.SelectedItem;
            if(selected != null) {
                ReportStorage reportStorage = new ReportStorage();
                reportStorage.RemoveReport(selected.Value);
                ReportsList.Items.Remove(ReportsList.SelectedValue);
            }
        }
    }
}
