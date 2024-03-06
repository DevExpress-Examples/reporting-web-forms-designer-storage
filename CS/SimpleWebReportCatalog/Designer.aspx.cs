using System;
using DevExpress.DataAccess.Sql;

namespace SimpleWebReportCatalog {
    public partial class Designer : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            DesignerTask task = (DesignerTask)Session["DesignerTask"];
            if(task != null) {
                InitDesignerPage(task);
            }
            else if(!Page.IsCallback) {
                Response.Redirect("Default.aspx");
            }
        }

        private void InitDesignerPage(DesignerTask task) {
            BindToData();

            switch(task.mode) {
                case ReportEdditingMode.NewReport:
                    // Create a new report from the template.
                    ASPxReportDesigner1.OpenReport(new ReportTemplate());
                    break;
                case ReportEdditingMode.ModifyReport:
                    // Load a report from the report storage.
                    ASPxReportDesigner1.OpenReport(task.reportID);
                    break;
            }
        }

        private void BindToData() {
            SqlDataSource ds = new SqlDataSource("Northwind");
            CustomSqlQuery query = new CustomSqlQuery();
            query.Name = "Products";
            query.Sql = "SELECT * FROM Products";
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
 
            ASPxReportDesigner1.DataSources.Add("Northwind", ds);
        }
    }
}
