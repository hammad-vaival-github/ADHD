using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;
namespace waats.Classes
{
    public class EmailManager
    {
        
        public bool SendEmail(string to = null, string ccopy = null, string subject = null, MvcHtmlString mailBody = null, string FileName_report = null, string FileName_Letter = null)
        {
            try
            {

                string SMTPServer = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPServer"];
                string Login = System.Web.Configuration.WebConfigurationManager.AppSettings["Login"];
                string Secret = System.Web.Configuration.WebConfigurationManager.AppSettings["Secret"];
                string FromEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["FromEmail"];
                string EmailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo"];
                string EnableEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["EnableEmail"];
                string port = System.Web.Configuration.WebConfigurationManager.AppSettings["port"];
                //string sMessage;
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress;
                if (!string.IsNullOrEmpty(FromEmail))
                {
                    fromAddress = new MailAddress(FromEmail);
                }
                else
                {
                    fromAddress = new MailAddress(FromEmail); //for testing purpose ONLY
                }
                MailAddress toAddress = new MailAddress(EmailTo);//live Clinician Email ==> EmailTo and to==> is patient email

                string Subject = subject;
                smtpClient.Host = SMTPServer;
                smtpClient.Port = int.Parse(port);
                smtpClient.UseDefaultCredentials = false;
                /*tom edit end*/
                smtpClient.Credentials = new System.Net.NetworkCredential(Login, Secret);
                message.From = fromAddress;
                message.To.Add(toAddress); //Recipent email 
                if (!string.IsNullOrEmpty(ccopy))
                {
                    message.CC.Add(ccopy);
                }
                message.Subject = Subject;
                if (mailBody != null)
                    message.Body = mailBody.ToHtmlString();
                else
                    message.Body = "";
                message.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(FileName_report))//if report
                {
                    try
                    {
                        var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/PDFs/" + FileName_report + ".pdf");
                        MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path));
                        message.Attachments.Add(new System.Net.Mail.Attachment(ms, FileName_report + ".pdf", MediaTypeNames.Application.Pdf));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (!string.IsNullOrEmpty(FileName_Letter))//if Letter
                {
                    try
                    {
                        var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/PDFs/" + FileName_Letter + ".pdf");
                        MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path));
                        message.Attachments.Add(new System.Net.Mail.Attachment(ms, FileName_Letter + ".pdf", MediaTypeNames.Application.Pdf));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (EnableEmail == "YES")
                {
                    smtpClient.Send(message);
                }
                else
                {
                    //do nothing
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            ////-----------------------Email address account
        }
               
    }
}