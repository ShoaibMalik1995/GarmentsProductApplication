using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVS.Dotnet328.GarmentsShop.Models.Users
{
    public class LoginModel
    {
        public string LoginId { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}