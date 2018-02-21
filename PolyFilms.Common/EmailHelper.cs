using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PolyFilms.Common
{
    public class EmailHelper
    {
        #region Email Settings
        public static string MailServerEmail { get; set; }

        public static string MailServerHost { get; set; }

        public static int MailServerPort { get; set; }

        public static string MailServerPassword { get; set; }

        public static bool MailServerEnableSsl { get; set; }

        public static string MailServerFromMail { get; set; }
        #endregion

        #region Send Mail Method
        public static void SendMail(string to, string subject, string body, bool isHtml = false, string bcc = "", string cc = "", string attachmentFileName = "")
        {
            // Instantiate a new instance of MailMessage
            var mailMessage = new MailMessage { From = new MailAddress(MailServerFromMail) };

            // Add To email addresses
            AddReceipt(Enums.ReceiptType.To, to, mailMessage);

            // Add bcc email addresses
            AddReceipt(Enums.ReceiptType.Bcc, bcc, mailMessage);

            // Add CC email addresses
            AddReceipt(Enums.ReceiptType.Cc, cc, mailMessage);

            // Add attachments
            AddReceipt(Enums.ReceiptType.Attachment, attachmentFileName, mailMessage);

            // Set the subject of the mail message
            mailMessage.Subject = subject;

            // Set the body of the mail message
            mailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mailMessage.IsBodyHtml = isHtml;

            // Instantiate a new instance of SmtpClient
            var smtpClient = new SmtpClient
            {
                Host = MailServerHost,
                Port = MailServerPort,
                EnableSsl = MailServerEnableSsl,
                Credentials = new NetworkCredential(MailServerEmail, MailServerPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };


            smtpClient.Send(mailMessage);
        }

        public static void SendMailWithAttachment(string to, string subject, string body, Attachment objAttachment, string cc)
        {
            // Instantiate a new instance of MailMessage
            var mailMessage = new MailMessage { From = new MailAddress(MailServerFromMail) };

            // Add To email addresses
            AddReceipt(Enums.ReceiptType.To, to, mailMessage);

            // Add CC email addresses
            AddReceipt(Enums.ReceiptType.Cc, cc, mailMessage);

            // Set the subject of the mail message
            mailMessage.Subject = subject;

            // Set the body of the mail message
            mailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mailMessage.IsBodyHtml = true;

            mailMessage.Attachments.Add(objAttachment);

            // Instantiate a new instance of SmtpClient
            var smtpClient = new SmtpClient
            {
                Host = MailServerHost,
                Port = MailServerPort,
                EnableSsl = MailServerEnableSsl,
                Credentials = new NetworkCredential(MailServerEmail, MailServerPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };


            smtpClient.Send(mailMessage);
        }

        private static void AddReceipt(Enums.ReceiptType receiptType, string receipt, MailMessage mMailMessage)
        {
            if (!string.IsNullOrEmpty(receipt))
            {
                string[] arrReceipt = receipt.Split(',', ';');

                switch (receiptType)
                {
                    case Enums.ReceiptType.To:
                        foreach (string strReceipt in arrReceipt)
                        {
                            mMailMessage.To.Add(new MailAddress(strReceipt));
                        }
                        break;
                    case Enums.ReceiptType.Cc:
                        foreach (string strReceipt in arrReceipt)
                        {
                            mMailMessage.CC.Add(new MailAddress(strReceipt));
                        }
                        break;
                    case Enums.ReceiptType.Bcc:
                        foreach (string strReceipt in arrReceipt)
                        {
                            mMailMessage.Bcc.Add(new MailAddress(strReceipt));
                        }
                        break;
                    case Enums.ReceiptType.Attachment:
                        foreach (string strReceipt in arrReceipt)
                        {
                            mMailMessage.Attachments.Add(new Attachment(strReceipt));
                        }
                        break;
                }
            }
        }

        public static void SendAsyncEmail(string to, string subject, string body, bool isHtml = false, string bcc = "",
            string cc = "", string attachmentFileName = "")
        {
            Task task = new Task(() => SendMail(to, subject, body, isHtml, bcc, cc, attachmentFileName));
            task.Start();
        }

        public static void SendMailWithAttachmentAsync(string to, string subject, string body, Attachment objAttachment, string cc)
        {
            Task task = new Task(() => SendMailWithAttachment(to, subject, body, objAttachment, cc));
            task.Start();
        }
        #endregion
    }
}
