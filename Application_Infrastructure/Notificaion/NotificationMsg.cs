using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

namespace Application_Infrastructure.Notificaion
{
    public class NotificationMsg : INotificationMsg
    {
        private readonly IRepositoryAsync<NotficationCls> _repository;

        public NotificationMsg(IRepositoryAsync<NotficationCls> repository)
        {
            this._repository = repository;
        }

        public bool Send(NotficationCls notfication)
        {
            switch (notfication.MsgType)
            {
                case NotificationType.Mail:
                    return this.SendMail(notfication);

                case NotificationType.SMS:
                    return true;

                case NotificationType.Whatsapp:
                    return true;

                default:
                    return true;
            }
        }

        private bool SendMail(NotficationCls notfication)
        {
            try
            {
                // create email message
                MimeMessage email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(notfication.MsgFrom));
                email.To.Add(MailboxAddress.Parse(notfication.MsgTo));
                email.Subject = notfication.MsgSubject;
                email.Body = new TextPart(TextFormat.Html) { Text = notfication.MsgBody };

                // send email
                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(APISetting.EmailConfiguration.SMTPAddress, APISetting.EmailConfiguration.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(APISetting.EmailConfiguration.UserId, APISetting.EmailConfiguration.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                notfication.MsgSatus = NotificationStatus.Success;
                this._repository.UpdateAsync(notfication);
                return true;
            }
            catch (Exception ex)
            {
                notfication.MsgSatus = NotificationStatus.Fail;
                if (ex.InnerException != null)
                {
                    notfication.FailDetails = ex.Message + ex.InnerException.Message;
                }
                else
                {
                    notfication.FailDetails = ex.Message;
                }

                this._repository.UpdateAsync(notfication);
                return false;
            }
        }
    }
}