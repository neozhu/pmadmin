using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlHelper2;

namespace WebApp
{
    public static class Auth
    {
        public static string CurrentUserName {

            get {
                string fullName = string.Empty;
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    fullName = HttpContext.Current.User.Identity.Name;
                }
                else {
                    fullName = "无名氏";
                }


                return fullName;
            }
        }

        public static int DefaultCompanyId() {

            string fullName = Auth.CurrentUserName;
            if (fullName != "无名氏") {
                return DatabaseFactory.CreateDatabase().ExecuteScalar<int>($"select CompanyId from dbo.AspNetUsers where UserName ='{fullName}3' ");
            }

            return 0;
        }
    }
}