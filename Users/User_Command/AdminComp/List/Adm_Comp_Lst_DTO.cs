namespace User_Command.AdminBranch.List
{
    public class Adm_Comp_Lst_DTO
    {
        public int OrgProdId { get; set; }
        public int CompId { get; set; }
        public string CompName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string TelephoneNo { get; set; }
        public bool IsActive { get; set; }
    }
}