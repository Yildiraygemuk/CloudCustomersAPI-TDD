using CloudCustomersAPI.Model;

namespace CloudCustomersAPI.Services.Abstract
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
    }
}
