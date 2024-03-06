using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
// ...


namespace SimpleWebReportCatalog {
    public partial class Default : System.Web.UI.Page {
        private DataTable reportsTable = new DataTable();
        private SqlDataAdapter reportsTableAdapter;

        protected void Page_Load(object sender, EventArgs e) {
            string connectionString = ConfigurationManager.ConnectionStrings["catalogConnectionString"].ConnectionString;
            reportsTableAdapter = new SqlDataAdapter("Select * from ReportLayout", new SqlConnection(connectionString));
            SqlCommandBuilder builder = new SqlCommandBuilder(reportsTableAdapter);
            reportsTableAdapter.InsertCommand = builder.GetInsertCommand();
            reportsTableAdapter.UpdateCommand = builder.GetUpdateCommand();
            reportsTableAdapter.DeleteCommand = builder.GetDeleteCommand();
            reportsTableAdapter.Fill(reportsTable);
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = reportsTable.Columns[0];
            reportsTable.PrimaryKey = keyColumns;

            if (!IsPostBack) {
                reportsList.DataSource = reportsTable;
                reportsList.DataMember = "Reports";
                reportsList.DataTextField = "DisplayName";
                reportsList.DataValueField = "ReportId";
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

            if (selected != null)
            {
                DataRow row = reportsTable.Rows.Find(int.Parse(selected.Value));
                if (row != null)
                {
                    row.Delete();
                    reportsTableAdapter.Update(reportsTable);
                    reportsTable.AcceptChanges();
                }
                reportsList.Items.Remove(reportsList.SelectedItem);
            }
        }
    }
}
