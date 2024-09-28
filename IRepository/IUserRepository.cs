using ProjectMaCaisseAPI_V01.Data;

namespace ProjectMaCaisseAPI_V01.IRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        List<User> GetUsersById(int UserId);
        bool CreateUser(User? user); 
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
