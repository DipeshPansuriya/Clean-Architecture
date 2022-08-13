namespace Login_Command.Model
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public int OrgProdId { get; set; }
        public string LoginMail { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Userbranchid { get; set; }
        public string ProductName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<UserRightDtl> ParentMenu { get; set; } = new List<UserRightDtl>();
        //public IEnumerable<UserRightDtl> ChildMenu { get; set; } = new List<UserRightDtl>();
    }

    public class UserRightDtl
    {
        public int RightId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int ParentMenuId { get; set; }
        public int DisplayOrder { get; set; }
        public bool ViewAccess { get; set; }
        public bool AddAccess { get; set; }
        public bool EditAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public List<UserRightDtl> ChildMenu { get; set; }
    }
}