namespace Application_Common
{
    public class NotficationCls
    {
        public int Id { get; set; }
        public string MsgType { get; set; }
        public string MsgFrom { get; set; }
        public string MsgTo { get; set; }
        public string MsgCC { get; set; }
        public string MsgSubject { get; set; }
        public string MsgBody { get; set; }
        public string MsgSatus { get; set; }
        public string FailDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        // public EmailAttachment[] EmailAttachments { get; set; }
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }

    public enum NotificationType
    {
        Mail = 0,
        SMS = 1,
        Whatsapp = 2
    }

    public enum NotificationStatus
    {
        Success = 0,
        Fail = 1,
        Pending = 2,
    }
}