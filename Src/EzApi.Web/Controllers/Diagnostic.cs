using System;
using System.Collections.Generic;
using System.Web.Http;
using Ez.Web.Infrastructure;
using Ez.Common;
using Ez.Entities;
using Ez.Entities.Models;

namespace Ez.Web.Controllers
{
    [RoutePrefix("api/diag")]
    public class DiagnosticController : ApiController
    {
        private bool WritetoTrace = false;
        DiagnosticDTO ret = null;
        [Route("")]
        [HttpGet]
        public DiagnosticDTO Get()
        {
            Dictionary<string, string> qs = HttpRequestHelper.GetQueryStrings(Request);
            WritetoTrace = qs.ContainsKey("trace");

            DiagnosticDTO ret = TestConnection(this.User.Identity.Name.Substring(this.User.Identity.Name.LastIndexOf(@"\") + 1));
            ret.UserName = this.User.Identity.Name.Substring(this.User.Identity.Name.LastIndexOf(@"\") + 1);
            ret.Environment = AppSettings.Instance.Environment;
            //ret.EzConnectionString = LinqSQLHelper.RemoveConnectionStringSecurity(AppSettings.Instance.ConnectionString);
            ret.EzConnectionString = AppSettings.Instance.ConnectionString;
            ret.Host = AppSettings.Instance.HostName;
            ret.ComputerName = System.Net.Dns.GetHostName().ToLower();
            ret.UserName = this.User.Identity.Name.Substring(this.User.Identity.Name.LastIndexOf(@"\") + 1);
            ret.DisableSecurity = false;
            ret.TraceEnable = false;
            ret.Role = "N/A";
            if (ret.DiagInfo == null) ret.DiagInfo = "";
            ret.DiagInfo += string.Format("Auth Info: AuthenticationType={0}, IsAuthenticated={1}, Name={2}, ImpersonationLevel={3}, IsAnonymous={4}", 
                this.User.Identity.AuthenticationType, this.User.Identity.IsAuthenticated, this.User.Identity.Name, ((System.Security.Principal.WindowsIdentity)this.User.Identity).ImpersonationLevel,
                ((System.Security.Principal.WindowsIdentity)this.User.Identity).IsAnonymous);
            try
            {
                if (!AppSettings.Instance.Environment.ToLower().StartsWith("local"))
                {
                    ActiveDirectoryHelper ad = new ActiveDirectoryHelper();
                    ADUserDetail userDetail = ad.GetUserByLoginName(ret.UserName);
                    List<string> roles = ad.GetUserGroupMembership(ret.UserName);

                    if (userDetail != null)
                    {
                        ret.FullNameFromAD = userDetail.FirstName + " " + userDetail.LastName;
                    }
                }
                else
                {
                    ret.FullNameFromAD = "N/A in LOCAL";
                }
            }
            catch (Exception ex)
            {
                ret.FullNameFromAD = "N/A in LOCAL... From Error:\n" + ex.ToString();
            }
            return ret;
        }

        private DiagnosticDTO TestConnection(string UserName)
        {
            DiagnosticDTO ret = new DiagnosticDTO();
            /* $$
            using (var ctx = new EzEntities())
            {
                ret.CanConnectionToEzDB = ctx.Database.Exists();
            }*/
            return ret;
        }
    }

    public class DiagnosticDTO
    {
        public string Host { get; set; }
        public string ComputerName { get; set; }
        public string Environment { get; set; }
        public string EzConnectionString { get; set; }
        public string EzxtraConnectionString { get; set; }
        public string UserName { get; set; }
        public bool CanConnectionToEzDB { get; set; }
        public bool TraceEnable { get; set; }
        public bool DisableSecurity { get; set; }
        public string FullNameFromAD { get; set; }
        public string Role { get; set; }
        public DateTime LinkerTimeStamp { get; set; }
        public string DiagInfo { get; set; }
    }

}