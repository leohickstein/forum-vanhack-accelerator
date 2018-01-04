using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumVanhackAccelerator.Data.Model;
using ForumVanhackAccelerator.Repositories;

namespace ForumVanhackAccelerator.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApplicationUser GetUserByUserName(string username)
        {
            return _userRepository.GetUserByUserName(username);
        }
    }
}
