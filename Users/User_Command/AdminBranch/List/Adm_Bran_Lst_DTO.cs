namespace User_Command.AdminBranch.List
{
    public class Adm_Bran_Lst_DTO
    {
        public int BranchId { get; set; }
        public int OrgProdId { get; set; }
        public int CompId { get; set; }
        public string CompName { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public bool IsHo { get; set; }
        public bool IsActive { get; set; }
    }
}