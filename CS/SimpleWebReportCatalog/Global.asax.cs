using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SimpleWebReportCatalog {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.EnableCustomSql();

            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new CustomReportStorageWebExtension());
            DevExpress.XtraReports.Web.ASPxReportDesigner.StaticInitialize();

        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}
