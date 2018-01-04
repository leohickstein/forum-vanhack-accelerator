using ForumVanhackAccelerator.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Services
{
    public interface IUserService
    {
        ApplicationUser GetUserByUserName(string username);
    }
}
