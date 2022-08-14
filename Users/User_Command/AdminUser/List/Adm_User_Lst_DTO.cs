namespace User_Command.AdminBranch.List
{
    public class Adm_User_Lst_DTO
    {
        public int OrgProdId { get; set; }
        public int UserId { get; set; }
        public string LoginMail { get; set; }
        public string LoginPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
    }
}