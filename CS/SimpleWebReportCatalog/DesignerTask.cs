namespace SimpleWebReportCatalog {
    public enum ReportEdditingMode { NewReport, ModifyReport };

    public class DesignerTask {
        public ReportEdditingMode mode { get; set; }
        public string reportID { get; set; }
    }
}