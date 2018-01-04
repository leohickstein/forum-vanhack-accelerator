using ForumVanhackAccelerator.Data;
using ForumVanhackAccelerator.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _db;

        public UserRepository(ForumDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUserByUserName(string username)
        {
            return _db.Users.Where(u => u.UserName == username).SingleOrDefault();
        }
    }
}
