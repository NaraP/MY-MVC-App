using System;
using System.IO;
using System.Text;

namespace Utility.ErrorLog
{
    public class ErrorLoggers : IErrorLogger
    {
        public void ExceptionHandler(Exception ex, string strPolicy, string ModuleName)
        {
            try
            {
                if (ex != null && ex.Message != string.Empty)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("The following Error occured at ");
                    sbMessage.Append(DateTime.Now).Append("in ").Append(strPolicy).Append("in ").Append(ModuleName);

                    //System.Web.HttpContext.Current.Session["ErrorMessage"] = sbMessage.ToString();

                    if (ex.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        sbMessage.Append(" - While calling Data access layer.");
                    }
                    else if (ex.GetType() == typeof(System.ArgumentException))
                    {
                        sbMessage.Append(" - There is some wrong Argument passed to the methods.");
                    }
                    else if (ex.GetType() == typeof(System.IO.FileNotFoundException))
                    {
                        sbMessage.Append(" - File not found in the specified directory. ");
                    }
                    else if (ex.GetType() == typeof(System.IO.DirectoryNotFoundException))
                    {
                        sbMessage.Append(" - Directory not found.");
                    }
                    else if (ex.GetType() == typeof(System.NullReferenceException))
                    {
                        sbMessage.Append(" - The method or variable has the Null value.");
                    }
                    else
                    {
                        sbMessage.Append(" - The general exception occured.");
                    }

                    sbMessage.Append(Environment.NewLine);
                    sbMessage.Append("System Error :").Append(Environment.NewLine).Append(ex.Message);
                    sbMessage.Append(Environment.NewLine);
                    sbMessage.Append("Stack Trace Details:--").Append(Environment.NewLine);
                    sbMessage.Append(ex.StackTrace).Append(Environment.NewLine);

                    if (ex.InnerException != null)
                    {
                        sbMessage.Append("Inner Exception:").Append(Environment.NewLine);
                        sbMessage.Append(ex.InnerException.Message);
                    }
                    sbMessage.Append(Environment.NewLine).Append(Environment.NewLine);
                    if (System.Web.HttpContext.Current.Session["EMPNO"] != null)
                        sbMessage.Append("User: " + System.Web.HttpContext.Current.Session["EMPNO"]);
                    sbMessage.Append(Environment.NewLine);
                    sbMessage.Append("Page: " + System.Web.HttpContext.Current.Request.ServerVariables["URL"]);

                    //Logging error in to the physical file
                    LogErrorIntoFile(sbMessage.ToString());

                    SendMail(sbMessage.ToString());

                    //System.Web.HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                }
            }
            catch (Exception excep)
            {
                SendMail(excep.Message.ToString());
            }
        }

        public void ExceptionWriteIntoTextFile(Exception ex, string strPolicy, string strQuery, string ModuleName)
        {
            try
            {
                if (ex != null && ex.Message != string.Empty)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("The following Error occured at ");
                    sbMessage.Append(DateTime.Now).Append("in ").Append(strPolicy).Append("in ").Append(ModuleName); ;

                    System.Web.HttpContext.Current.Session["ErrorMessage"] = sbMessage.ToString();

                    if (ex.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        sbMessage.Append(" - While calling Data access layer.");
                    }
                    else
                    {
                        sbMessage.Append(" - The general exception occured.");
                    }

                    sbMessage.Append("\nSystem Error :\n").Append(ex.Message);
                    sbMessage.Append("\nStack Trace Details:--\n");
                    sbMessage.Append(ex.StackTrace);

                    if (ex.InnerException != null)
                    {
                        sbMessage.Append("\nInner Exception:\n");
                        sbMessage.Append(ex.InnerException.Message);
                    }

                    sbMessage.Append("Query:\n");
                    sbMessage.Append(strQuery);

                    sbMessage.Append("\n\n User: " + System.Web.HttpContext.Current.Session["EMPNO"]);
                    sbMessage.Append("\n Page: " + System.Web.HttpContext.Current.Request.ServerVariables["URL"]);

                    //Logging error in to the physical file
                    LogErrorIntoFile(sbMessage.ToString());

                    //SendMail(sbMessage.ToString());

                    //System.Web.HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                }
            }
            catch (Exception excep)
            {
                SendMail(excep.Message.ToString());
            }
        }

        #region Send Mail
        /// <summary>
        /// Method for sending the mail
        /// </summary>
        /// <param name="body">Method body</param>
        private void SendMail(string body)
        {

            EmailManager objEmailSend = new EmailManager();
            objEmailSend.FromMail = System.Configuration.ConfigurationSettings.AppSettings["FromMail"];
            objEmailSend.ToMail = System.Configuration.ConfigurationSettings.AppSettings["ToMail"];
            objEmailSend.CCMail = System.Configuration.ConfigurationSettings.AppSettings["CCMail"];
            objEmailSend.MailSubject = System.Configuration.ConfigurationSettings.AppSettings["Subject"] + " for user, " + System.Web.HttpContext.Current.Session["EMPNO"] + " in page " + System.Web.HttpContext.Current.Request.ServerVariables["URL"];
            string mailServer = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
            if (!string.IsNullOrEmpty(mailServer))
            {
                objEmailSend.MailServerName = mailServer;
            }
            objEmailSend.Body = body;
            objEmailSend.SendMail();
        }
        #endregion Send Mail

        private void LogErrorIntoFile(string errorMessage)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string filePath = System.Configuration.ConfigurationSettings.AppSettings["ErrorFilePath"];

            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy/MM/dd").Replace("/", string.Empty);
            filePath = filePath + "_" + date + ".txt";
            try
            {
                using (fs = File.Open(filePath,
                            FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (sw = new StreamWriter(fs))
                    {
                        sw.Write(errorMessage);
                        sw.Write(Environment.NewLine);
                        sw.Write("==========================================================================================================");
                        sw.Write(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                SendMail(ex.Message);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
                fs = null;
                sw.Dispose();
                sw = null;
            }
        }
    }
}
