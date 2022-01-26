using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movie_api.Auth
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
