using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using Login_Command.Model;
using MediatR;
using System.Data;
using System.Net;

namespace Login_Command.List
{
    public class LoginDetails_Lst : IRequest<Response>
    {
        public string LoginEmail { get; set; }
        public string Password { get; set; }

        public class LoginDetailsHandler : IRequestHandler<LoginDetails_Lst, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public LoginDetailsHandler(INotificationMsg notification,
                IDapper<Response> dapper)
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(LoginDetails_Lst request, CancellationToken cancellationToken)
            {
                Response response = new Response
                {
                    ResponseStatus = false
                };
                try
                {
                    DynamicParameters param = new();
                    param.Add("@LoginEmail", request.LoginEmail);
                    param.Add("@LoginPassword", request.Password);

                    string? loginisexists = await dapper.ExecuteScalarAsync
                        ("usp_CheckLogin",
                        param,
                        System.Data.CommandType.StoredProcedure);

                    if (string.IsNullOrEmpty(loginisexists))
                    {
                        UserInfo? userInfo = (await dapper.GetDataListAsync<UserInfo>
                             ("usp_LoginHeader",
                             param,
                             System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                        if (userInfo != null)
                        {
                            param = new DynamicParameters();
                            param.Add("@UserId", userInfo.UserId);
                            param.Add("@CompanyId", userInfo.CompanyId);
                            param.Add("@BranchId", userInfo.BranchId);

                            DataSet ds = await dapper.GetDataSetAsync("usp_LoginDetail",
                                param,
                                System.Data.CommandType.StoredProcedure);
                            userInfo.ParentMenu = GenericFunction.TableToList<UserRightDtl>(ds.Tables[0]).ToList();
                            List<UserRightDtl> lstchld = GenericFunction.TableToList<UserRightDtl>(ds.Tables[1]).ToList();
                            foreach (UserRightDtl item in userInfo.ParentMenu)
                            {
                                List<UserRightDtl> childmenu = lstchld.Where(x => x.ParentMenuId == item.MenuId).ToList();
                                if (childmenu.Count > 0)
                                {
                                    item.ChildMenu = childmenu;
                                }
                            }
                            //userInfo.userBranchDtls = GenericFunction.TableToList<UserBranchDtl>(ds.Tables[0]);
                            //userInfo.userBranchDtls = (await dapper.GetDataListAsync<UserBranchDtl>
                            //("usp_LoginDetail",
                            //    param,
                            //    System.Data.CommandType.StoredProcedure)).ToList();
                        }
                        response.ResponseStatus = true;
                        response.ResponseObject = userInfo;
                    }
                    else
                    {
                        response.ResponseObject = loginisexists;
                    }
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
}