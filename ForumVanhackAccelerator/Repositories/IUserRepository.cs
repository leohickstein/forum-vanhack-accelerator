using ForumVanhackAccelerator.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByUserName(string username);
    }
}
