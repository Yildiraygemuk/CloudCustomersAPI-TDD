using CloudCustomersAPI.Config;
using CloudCustomersAPI.Model;
using CloudCustomersAPI.Services.Abstract;
using Microsoft.Extensions.Options;

namespace CloudCustomersAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;
        

        public UserService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if(usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<User>();
            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
