using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movie_api.Auth
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("admin") && password.Equals("movieApi@321");
        }
    }
}
