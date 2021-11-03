using System.Threading.Tasks;
using Models;

namespace FileData
{
    public interface IUserService
    {
        Task<User> ValidateUser(string UserName, string Password);
    }
}