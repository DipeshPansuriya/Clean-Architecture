using Application_Database;
using Application_Domain.UserConfig;

namespace Test_Application.InitializeDbData
{
    public class Test_User_Data
    {
        public static void InitializeData(APP_DbContext context)
        {
            context.Users.Add(new user_cls
            {
                UserName = "Test Test",
                EmailId = "Test@test.com",
                Password = "Test@123",
                IsActive = true
            });

            context.SaveChanges();
        }
    }
}