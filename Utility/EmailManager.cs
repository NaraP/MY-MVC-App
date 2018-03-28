using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Utility
{
    public class EmailManager
    {
        #region Class level variable
        string _fromMail;
        string _toMail;
        string _ccMail;
        string _bccMail;
        string _subject;
        string _body;
        string _UserId;
        string _PassWord;
        string _mailServerName;
        string[] _attachmentPath;
        bool _isMailTypeTextOrHTML;
        const string DEFAULT_MAIL_SERVER_NAME = "blr-outlook.wipro.com";
        #endregion

        #region Default constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmailManager()
        {
            _fromMail = string.Empty;
            _toMail = string.Empty;
            _ccMail = string.Empty;
            _bccMail = string.Empty;
            _subject = string.Empty;
            _body = string.Empty;
            _UserId = ConfigurationManager.AppSettings["EmailUserId"].ToString();
            _PassWord = ConfigurationManager.AppSettings["EmailPassWord"].ToString();
            _mailServerName = DEFAULT_MAIL_SERVER_NAME;
            _isMailTypeTextOrHTML = true;
            _attachmentPath = null;
        }
        #endregion

        #region Parametric constructor
        /// <summary>
        /// Parametric constructor
        /// </summary>
        /// <param name="attachment">Mail attachment</param>
        public EmailManager(string[] attachment)
        {
            _fromMail = string.Empty;
            _toMail = string.Empty;
            _ccMail = string.Empty;
            _bccMail = string.Empty;
            _subject = string.Empty;
            _body = string.Empty;
            _mailServerName = DEFAULT_MAIL_SERVER_NAME;
            if (attachment != null)
            {
                _attachmentPath = attachment;
            }
            _isMailTypeTextOrHTML = true;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// From Email address of the Recipients.
        /// </summary>
        public string FromMail
        {
            get
            {
                return _fromMail;
            }
            set
            {
                _fromMail = value;
            }
        }

        /// <summary>
        /// From Email address of the Recipients.
        /// </summary>
        public string ToMail
        {
            get
            {
                return _toMail;
            }
            set
            {
                _toMail = value;
            }
        }

        /// <summary>
        /// CC Email address of the Recipients.
        /// </summary>
        public string CCMail
        {
            get
            {
                return _ccMail;
            }
            set
            {
                _ccMail = value;
            }
        }

        /// <summary>
        /// BCC Email address of the Recipients.
        /// </summary>
        public string BCCMail
        {
            get
            {
                return _bccMail;
            }
            set
            {
                _bccMail = value;
            }
        }

        /// <summary>
        /// Subject of the mail to be send
        /// </summary>
        public string MailSubject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// Body of the mail to be send
        /// </summary>
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        /// <summary>
        /// Mail Server Name
        /// </summary>
        public string MailServerName
        {
            get
            {
                return _mailServerName;
            }
            set
            {
                _mailServerName = value;
            }
        }

        /// <summary>
        /// Body of the mail, whether Text or HTML.
        /// </summary>
        public bool MailTypeTextOrHTML
        {
            get
            {
                return _isMailTypeTextOrHTML;
            }
            set
            {
                _isMailTypeTextOrHTML = value;
            }
        }
        #endregion

        private string[] RecipientsList(string recipients)
        {
            string[] recipientList = null;
            if (!string.IsNullOrEmpty(recipients))
            {
                if (recipients.Contains(","))
                {
                    recipientList = recipients.Split(',');
                }
                else if (recipients.Contains(";"))
                {
                    recipientList = recipients.Split(';');
                }
                else
                {
                    recipientList = recipients.Split(',');
                }
            }
            return recipientList;
        }

        public string[] AttachmentPath
        {
            get
            {
                return _attachmentPath;
            }
            set
            {
                _attachmentPath = value;
            }
        }


        #region Method to send mail
        /// <summary>
        /// The method responsible for sending mail depends on the TO, CC and BCC mail address.
        /// </summary>
        /// <param name="smtpServer">The name of the SMTP server</param>
        public void SendMail()
        {
            MailAddress fromAddress = new MailAddress(_fromMail);
            MailMessage mail = new MailMessage();
            SmtpClient mailClient = null;
            try
            {
                mail.From = fromAddress;
                string[] toMailList = RecipientsList(_toMail);
                if (toMailList != null)
                {
                    foreach (string mailId in toMailList)
                    {
                        if (!string.IsNullOrEmpty(mailId) && !mailId.ToLower().Contains("tk.kurien@wipro.com") && !mailId.ToLower().Contains("azim.premji@wipro.com") && !mailId.ToLower().Contains("abidali.neemuchwala@wipro.com"))
                            mail.To.Add(mailId);
                    }
                }

                string[] ccMailList = RecipientsList(_ccMail);
                if (ccMailList != null)
                {
                    foreach (string mailId in ccMailList)
                    {
                        if (!string.IsNullOrEmpty(mailId) && !mailId.ToLower().Contains("tk.kurien@wipro.com") && !mailId.ToLower().Contains("azim.premji@wipro.com") && !mailId.ToLower().Contains("abidali.neemuchwala@wipro.com"))
                            mail.CC.Add(mailId);
                    }
                }
                string[] bccMailList = RecipientsList(_bccMail);
                if (bccMailList != null)
                {
                    foreach (string mailId in bccMailList)
                    {
                        if (!string.IsNullOrEmpty(mailId) && !mailId.ToLower().Contains("tk.kurien@wipro.com") && !mailId.ToLower().Contains("azim.premji@wipro.com") && !mailId.ToLower().Contains("abidali.neemuchwala@wipro.com"))
                            mail.Bcc.Add(mailId);
                    }
                }
                mail.Subject = _subject;
                mail.Body = _body;
                mail.IsBodyHtml = _isMailTypeTextOrHTML;
                if (_attachmentPath != null)
                {
                    foreach (string str in _attachmentPath)
                    {
                        if (System.IO.File.Exists(str))
                        {
                            Attachment attachment = new Attachment(str);
                            mail.Attachments.Add(attachment);
                        }
                    }
                }
                mailClient = new SmtpClient(_mailServerName);
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                if (_fromMail.ToLower() == "education.evaluation@wipro.com")
                {
                    _UserId = ConfigurationManager.AppSettings["EEEmailUserId"].ToString();
                    _PassWord = ConfigurationManager.AppSettings["EEEmailPassWord"].ToString();
                }
                if (_fromMail.ToLower() == "wmg.competency@wipro.com")
                {
                    _UserId = ConfigurationManager.AppSettings["WMGEmailUserId"].ToString();
                    _PassWord = ConfigurationManager.AppSettings["WMGEmailPassWord"].ToString();
                }

                mailClient.Credentials = new NetworkCredential(_UserId, _PassWord);
                mailClient.Port = 587;
                mailClient.Send(mail);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {
                mailClient = null;
                mail.Dispose();
                mail = null;
            }
        }
        #endregion
    }
}
