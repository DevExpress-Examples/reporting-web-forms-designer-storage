using System;
using System.Data;
using SimpleWebReportCatalog.CatalogDataTableAdapters;
// ...

namespace SimpleWebReportCatalog {
    public class ReportStorage {
        private CatalogData catalogDataSet;
        private DataTable reportsTable;
        private ReportsTableAdapter reportsTableAdapter;

        public ReportStorage() {
            catalogDataSet = new CatalogData();
            reportsTableAdapter = new ReportsTableAdapter();
            reportsTableAdapter.Fill(catalogDataSet.Reports);

            reportsTable = catalogDataSet.Tables["Reports"];
        }

        public void WriteReport(ReportModel reportModel) {
            DataRow row = reportsTable.NewRow();

            row["ReportID"] = reportModel.ReportId;
            row["LayoutData"] = reportModel.LayoutData;

            reportsTable.Rows.Add(row);
            reportsTableAdapter.Update(catalogDataSet);
            catalogDataSet.AcceptChanges();
        }


        public ReportModel GetReport(string reportId) {
            DataRow row = reportsTable.Rows.Find(reportId);

            if(row != null) {
                ReportModel model = new ReportModel();
                model.ReportId = (string)row["ReportID"];
                model.LayoutData = (Byte[])row["LayoutData"];
                return model;
            } else {
                return null;
            }
        }

        public void UpdateReport(ReportModel reportModel) {
            DataRow row = reportsTable.Rows.Find(reportModel.ReportId);

            if(row != null) {
                row["LayoutData"] = reportModel.LayoutData;
                reportsTableAdapter.Update(catalogDataSet);
                catalogDataSet.AcceptChanges();
            }
        }

        public void RemoveReport(string reportId) {
            DataRow row = reportsTable.Rows.Find(reportId);

            if(row != null) {
                row.Delete();
                reportsTableAdapter.Update(catalogDataSet);
                catalogDataSet.AcceptChanges();
            }
        }
    }
}







