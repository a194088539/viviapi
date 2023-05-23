using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.WebComponents
{
    public class EmailHelper
    {
        private MailMessage mail;

        public EmailHelper(string from, string to, string subject, string body, bool isHtml, Encoding encoding)
        {
            if (string.IsNullOrEmpty(from))
                from = viviapi.BLL.SysConfig.MailFrom;
            this.mail = new MailMessage(new MailAddress(from, viviapi.BLL.SysConfig.MailDisplayName), new MailAddress(to));
            this.mail.Subject = subject;
            this.mail.Body = body;
            this.mail.BodyEncoding = encoding;
            this.mail.IsBodyHtml = isHtml;
            this.mail.SubjectEncoding = encoding;
        }

        public bool Send()
        {
            bool flag = false;
            try
            {
                string mailDomain = viviapi.BLL.SysConfig.MailDomain;
                int mailDomainPort = viviapi.BLL.SysConfig.MailDomainPort;
                string mailServerUserName = viviapi.BLL.SysConfig.MailServerUserName;
                string mailServerPassWord = viviapi.BLL.SysConfig.MailServerPassWord;
                if (!string.IsNullOrEmpty(mailDomain) && mailDomainPort > 0 && !string.IsNullOrEmpty(mailServerUserName) && !string.IsNullOrEmpty(mailServerPassWord))
                {
                    new SmtpClient(mailDomain, mailDomainPort)
                    {
                        Credentials = ((ICredentialsByHost)new NetworkCredential(mailServerUserName, mailServerPassWord)),
                        EnableSsl = (viviapi.BLL.SysConfig.MailIsSsl == "1")
                    }.Send(this.mail);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return flag;
        }

        public void Send2()
        {
            try
            {
                string mailDomain = viviapi.BLL.SysConfig.MailDomain;
                int mailDomainPort = viviapi.BLL.SysConfig.MailDomainPort;
                string mailServerUserName = viviapi.BLL.SysConfig.MailServerUserName;
                string mailServerPassWord = viviapi.BLL.SysConfig.MailServerPassWord;
                if (string.IsNullOrEmpty(mailDomain) || mailDomainPort <= 0 || string.IsNullOrEmpty(mailServerUserName) || string.IsNullOrEmpty(mailServerPassWord))
                    return;
                new SmtpClient(mailDomain, mailDomainPort)
                {
                    Credentials = ((ICredentialsByHost)new NetworkCredential(mailServerUserName, mailServerPassWord)),
                    EnableSsl = (viviapi.BLL.SysConfig.MailIsSsl == "1")
                }.Send(this.mail);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
