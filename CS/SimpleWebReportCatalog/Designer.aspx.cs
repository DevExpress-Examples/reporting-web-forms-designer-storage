#region #DesignerUsings
using System;
using System.Web;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
// ...
#endregion #DesignerUsings


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
                    // Load an existing report from the report storage.
                    ASPxReportDesigner1.OpenReport(task.reportID);
                    break;
            }
        }


        #region #BindToData
        private void BindToData() {
            // Create a SQL data source with the specified connection parameters.
            Access97ConnectionParameters connectionParameters =
                new Access97ConnectionParameters(HttpRuntime.AppDomainAppPath + "App_Data\\nwind.mdb", "", "");
            SqlDataSource ds = new SqlDataSource(connectionParameters);

            // Create a custom SQL query to access the Products data table.
            CustomSqlQuery query = new CustomSqlQuery();
            query.Name = "Products";
            query.Sql = "SELECT * FROM Products";
            ds.Queries.Add(query);
            ds.RebuildResultSchema();

            // Add the created data source to the list of default data sources. 
            ASPxReportDesigner1.DataSources.Add("Northwind", ds);
        }
        #endregion #BindToData
    }
}
