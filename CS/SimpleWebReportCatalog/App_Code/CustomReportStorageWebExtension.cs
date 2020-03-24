using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SimpleWebReportCatalog
{
    public class CustomReportStorageWebExtension : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    {
        private DataTable reportsTable = new DataTable();
        private SqlDataAdapter reportsTableAdapter;
        string connectionString = "Data Source=localhost;Initial Catalog=Reports;Integrated Security=True";
        public CustomReportStorageWebExtension()
        {
            reportsTableAdapter = new SqlDataAdapter("Select * from ReportLayout", new SqlConnection(connectionString));
            SqlCommandBuilder builder = new SqlCommandBuilder(reportsTableAdapter);
            reportsTableAdapter.InsertCommand = builder.GetInsertCommand();
            reportsTableAdapter.UpdateCommand = builder.GetUpdateCommand();
            reportsTableAdapter.DeleteCommand = builder.GetDeleteCommand();
            reportsTableAdapter.Fill(reportsTable);
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = reportsTable.Columns[0];
            reportsTable.PrimaryKey = keyColumns;
        }
        public override bool CanSetData(string url)
        {
            return true;
        }
        public override byte[] GetData(string url)
        {
            // Get the report data from the storage.
            DataRow row = reportsTable.Rows.Find(int.Parse(url));
            if (row == null) return null;

            byte[] reportData = (Byte[])row["LayoutData"];
            return reportData;
        }
        public override Dictionary<string, string> GetUrls()
        {
            reportsTable.Clear();
            reportsTableAdapter.Fill(reportsTable);
            // Get URLs and display names for all reports available in the storage.
            var v = reportsTable.AsEnumerable()
                  .ToDictionary<DataRow, string, string>(dataRow => ((Int32)dataRow["ReportId"]).ToString(),
                                                         dataRow => (string)dataRow["DisplayName"]);
            return v;
        }
        public override bool IsValidUrl(string url)
        {
            return true;
        }
        public override void SetData(XtraReport report, string url)
        {
            // Write a report to the storage under the specified URL.
            DataRow row = reportsTable.Rows.Find(int.Parse(url));
            if (row != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    report.SaveLayoutToXml(ms);
                    row["LayoutData"] = ms.GetBuffer();
                }
                reportsTableAdapter.Update(reportsTable);
            }
        }
        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            // Save a report to the storage with a new URL. 
            // The defaultUrl parameter is the report name that the user specifies.
            DataRow row = reportsTable.NewRow();
            row["ReportId"] = 0;
            row["DisplayName"] = defaultUrl;
            using (MemoryStream ms = new MemoryStream())
            {
                report.SaveLayoutToXml(ms);
                row["LayoutData"] = ms.GetBuffer();
            }
            reportsTable.Rows.Add(row);
            reportsTableAdapter.Update(reportsTable);
            // Refill the dataset to obtain the actual value of the new row's autoincrement key field.
            reportsTable.Clear();
            reportsTableAdapter.Fill(reportsTable);
            return reportsTable.AsEnumerable().
                FirstOrDefault(x => x["DisplayName"].ToString() == defaultUrl)["ReportId"].ToString();
        }
    }
}