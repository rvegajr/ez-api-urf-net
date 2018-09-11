using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Ez.Common.Extentions
{
    /// <summary></summary>
    /// <summary></summary>
    public static class ExceptionExtentions
    {
        //http://stackoverflow.com/questions/9314172/getting-all-messages-from-innerexceptions
        /// <summary></summary>
        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            try
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(e, true);
                System.Diagnostics.StackFrame frame = st.GetFrame(0);
                string fileName = ((frame == null) ? null : frame.GetFileName());
                //string fileName = frame.GetFileName();
                if (fileName == null)
                {
                    fileName = frame.ToString();
                }
                else
                {
                    string rootpath = @"{SOLUTION_PATH}".ResolvePathVars();
                    fileName = fileName.Replace(rootpath, "");
                }
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();
                if (e.InnerException != null)
                    msgs += (msgs.Length > 0 ? "\r\nInnerException: " : "") + GetExceptionMessages(e.InnerException);
                return msgs.Replace("An error occurred while updating the entries. See the inner exception for details.", "").Replace(" - Inner Exception: ", "").Replace("\r\nInnerException: ", "").Trim() + string.Format(" [Module:{0}.{1} Line:{2} Col:{3}]", fileName, methodName, line, col);
            }
            catch (Exception ex)
            {
                return msgs + ".  Exception Error: " + ex.Message;
            }
        }
    }
}