using System;
using System.Data;
using System.Web.UI.WebControls;
using SimpleWebReportCatalog.CatalogDataTableAdapters;
// ...


namespace SimpleWebReportCatalog {
    public partial class Default : System.Web.UI.Page {
        private CatalogData catalogDataSet;
        private DataTable reportsTable;
        private ReportsTableAdapter reportsTableAdapter;


        protected void Page_Load(object sender, EventArgs e) {
            catalogDataSet = new CatalogData();
            reportsTableAdapter = new ReportsTableAdapter();
            reportsTableAdapter.Fill(catalogDataSet.Reports);
            reportsTable = catalogDataSet.Tables["Reports"];
            
            if(!IsPostBack) {
                reportsList.DataSource = catalogDataSet;
                reportsList.DataMember = "Reports";
                reportsList.DataTextField = "DisplayName";
                reportsList.DataValueField = "ReportID";
                this.DataBind();   
            }
        }


        protected void NewReportButton_Click(object sender, EventArgs e) {
            Session["DesignerTask"] = new DesignerTask {
                mode = ReportEdditingMode.NewReport,
            };
            Response.Redirect("Designer.aspx");
        }


        protected void EditButton_Click(object sender, EventArgs e) {
            ListItem selected = reportsList.SelectedItem;
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
            ListItem selected = reportsList.SelectedItem;          

            if(selected != null) {
                DataRow row = reportsTable.Rows.Find(int.Parse(selected.Value));
                if(row != null) {
                    row.Delete();
                    reportsTableAdapter.Update(catalogDataSet);
                    catalogDataSet.AcceptChanges();                    
                }
                reportsList.Items.Remove(reportsList.SelectedItem);  
            }
        }
    }
}