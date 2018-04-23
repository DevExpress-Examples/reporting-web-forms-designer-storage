#region #DesignerUsings
using System;
using System.Web;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
// ...
#endregion #DesignerUsings

namespace SimpleWebReportCatalog {
    public partial class Designer : System.Web.UI.Page {
        static ReportStorage reportStorage = new ReportStorage();
        private DesignerTask task;
        private ReportModel model;


        protected void Page_Load(object sender, EventArgs e) {
            task = (DesignerTask)Session["DesignerTask"];
            if(task != null) {
                InitDesignerPage();
            } else if(!Page.IsCallback) {
                Response.Redirect("Default.aspx");
            }
        }


        private void InitDesignerPage() {
            BindToData();

            switch(task.mode) {
                case ReportEdditingMode.NewReport:
                    // Create a new report from the template.
                    model = new ReportModel();
                    model.ReportId = task.reportID;
                    ASPxReportDesigner1.OpenReport(new ReportTemplate { Name = model.ReportId });
                    break;
                case ReportEdditingMode.ModifyReport:
                    // Load an existing report from the catalog database.
                    model = reportStorage.GetReport(task.reportID);
                    ASPxReportDesigner1.OpenReportXmlLayout(model.LayoutData);
                    break;
            }
        }


        protected void ASPxReportDesigner1_SaveReportLayout(object sender, DevExpress.XtraReports.Web.SaveReportLayoutEventArgs e) {
            // If a report is new, write it to a new database record, othervise update the existing record.
            if(task.mode == ReportEdditingMode.NewReport) {
                model.LayoutData = e.ReportLayout;
                reportStorage.WriteReport(model);
                task.mode = ReportEdditingMode.ModifyReport;
            } else {
                model.LayoutData = e.ReportLayout;
                reportStorage.UpdateReport(model);
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