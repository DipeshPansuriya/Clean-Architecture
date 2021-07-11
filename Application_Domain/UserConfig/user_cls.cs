namespace Application_Domain.UserConfig
{
    public class user_cls : audit_cls
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}