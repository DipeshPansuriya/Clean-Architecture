using Application_Common;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace Generic_Command.InsertUpdate.Menu_InstUpd
{
    public class Menu_InstUpd : IRequest<Response>
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int ParentMenuId { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsSysAdmin { get; set; }

        public class Menu_InstUpdCommandHandler : IRequestHandler<Menu_InstUpd, Response>
        {
            private readonly IDapper<Response> dapper;

            public Menu_InstUpdCommandHandler(IDapper<Response> dapper)
            {
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Menu_InstUpd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    List<Menu_InstUpd> menuparrent = new()
                    {
                        new Menu_InstUpd { MenuId = 1,MenuName = "Home", MenuURL = "Home", ParentMenuId = 0, IsActive = true, DisplayOrder = 1,},
                        new Menu_InstUpd { MenuId = 2,MenuName = "Organizations", MenuURL = "", ParentMenuId = 0, IsActive = true, DisplayOrder = 2},
                        new Menu_InstUpd { MenuId = 3,MenuName = "Locations", MenuURL = "", ParentMenuId = 0, IsActive = true, DisplayOrder = 3},
                        new Menu_InstUpd { MenuId = 4,MenuName = "Accounting", MenuURL = "", ParentMenuId = 0, IsActive = true, DisplayOrder = 4},
                        new Menu_InstUpd { MenuId = 5,MenuName = "Party's", MenuURL = "", ParentMenuId = 0, IsActive = true, DisplayOrder = 5},
                        new Menu_InstUpd { MenuId = 6,MenuName = "Other Libery", MenuURL = "", ParentMenuId = 0, IsActive = true, DisplayOrder = 6},
                        new Menu_InstUpd { MenuId = 26,MenuName = "Client Register", MenuURL = "ClientRegister", ParentMenuId = 0, IsActive = true, DisplayOrder = 7, IsSysAdmin = true},
                    };

                    List<Menu_InstUpd> menuchild = new()
                    {
                        /////// Child Menu Organizations ///////
                        new Menu_InstUpd { MenuId = 7,MenuName = "Compnay", MenuURL = "Company", ParentMenuId = 2, IsActive = true, DisplayOrder = 1},
                        new Menu_InstUpd { MenuId = 8,MenuName = "Branch", MenuURL = "Branch", ParentMenuId = 2, IsActive = true, DisplayOrder = 2},
                        new Menu_InstUpd { MenuId = 9,MenuName = "Financial Year", MenuURL = "FincialYear", ParentMenuId = 2, IsActive = true, DisplayOrder = 3},
                        new Menu_InstUpd { MenuId = 10,MenuName = "User", MenuURL = "User", ParentMenuId = 2, IsActive = true, DisplayOrder = 4},
                        new Menu_InstUpd { MenuId = 11,MenuName = "Role", MenuURL = "Role", ParentMenuId = 2, IsActive = true, DisplayOrder = 5},
                        new Menu_InstUpd { MenuId = 12,MenuName = "Right", MenuURL = "Right", ParentMenuId = 2, IsActive = true, DisplayOrder = 6},
                        new Menu_InstUpd { MenuId = 13,MenuName = "Organizations Setting", MenuURL = "OrgSetting", ParentMenuId = 2, IsActive = true, DisplayOrder = 7},

                        /////// Child Menu Locations ///////
                        new Menu_InstUpd { MenuId = 14,MenuName = "Continent", MenuURL = "Continent", ParentMenuId = 3, IsActive = true, DisplayOrder = 1},
                        new Menu_InstUpd { MenuId = 15,MenuName = "Country", MenuURL = "Country", ParentMenuId = 3, IsActive = true, DisplayOrder = 2},
                        new Menu_InstUpd { MenuId = 16,MenuName = "State", MenuURL = "State", ParentMenuId = 3, IsActive = true, DisplayOrder = 3},
                        new Menu_InstUpd { MenuId = 17,MenuName = "City", MenuURL = "City", ParentMenuId = 3, IsActive = true, DisplayOrder = 4},
                        new Menu_InstUpd { MenuId = 18,MenuName = "Airport", MenuURL = "Airport", ParentMenuId = 3, IsActive = true, DisplayOrder = 5},
                        new Menu_InstUpd { MenuId = 19,MenuName = "Port", MenuURL = "Port", ParentMenuId = 3, IsActive = true, DisplayOrder = 6},

                        /////// Child Menu Accounting ///////
                        new Menu_InstUpd { MenuId = 20,MenuName = "Currency", MenuURL = "Currency", ParentMenuId = 4, IsActive = true, DisplayOrder = 1},
                        new Menu_InstUpd { MenuId = 21,MenuName = "Ex. Rate", MenuURL = "ExchangeRate", ParentMenuId = 4, IsActive = true, DisplayOrder = 2},
                        new Menu_InstUpd { MenuId = 22,MenuName = "Tax/Vat", MenuURL = "Tax", ParentMenuId = 4, IsActive = true, DisplayOrder = 3},
                        new Menu_InstUpd { MenuId = 23,MenuName = "Charge", MenuURL = "Cahrge", ParentMenuId = 4, IsActive = true, DisplayOrder = 4},

                        /////// Child Menu Party's ///////
                        new Menu_InstUpd { MenuId = 24,MenuName = "Customer/Vendor", MenuURL = "Party", ParentMenuId = 5, IsActive = true, DisplayOrder = 1},

                        /////// Child Menu Other Libery ///////
                        new Menu_InstUpd { MenuId = 25,MenuName = "Unit Of Measurement", MenuURL = "UOM", ParentMenuId = 6, IsActive = true, DisplayOrder = 1},
                        new Menu_InstUpd { MenuId = 26,MenuName = "Container", MenuURL = "Container", ParentMenuId = 6, IsActive = true, DisplayOrder = 1},
                    };

                    Menucls menucls = new()
                    {
                        ParrentMenu = menuparrent,
                        ChildMenu = menuchild,
                    };
                    string XMLMenu = GenericFunction.ClassToXML<Menucls>(menucls);
                    DynamicParameters param = new();

                    param.Add("@XMLMenu", XMLMenu);

                    string? data = await dapper.ExecuteScalarAsync
                       ("sp_AdminMenu_InsertUpdate",
                       param,
                       System.Data.CommandType.StoredProcedure
                       );

                    response.ResponseStatus = true;
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                }
                return response;
            }
        }
    }

    public class Menucls
    {
        public List<Menu_InstUpd> ParrentMenu { get; set; }
        public List<Menu_InstUpd> ChildMenu { get; set; }
    }
}