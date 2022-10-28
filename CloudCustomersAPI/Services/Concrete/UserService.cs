using CloudCustomersAPI.Model;
using CloudCustomersAPI.Services.Abstract;

namespace CloudCustomersAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
