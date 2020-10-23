using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Authorizations
{
    public class Administrator
    {
        public const string DefaultRoleName = "超级管理员";
        public const string DefaultUserName = "administrator";
        public const string DefaultPassword = "1q2w3E*";

        public static bool Ensures(string userName)
        {
            return userName == DefaultUserName;
        }
    }
}
