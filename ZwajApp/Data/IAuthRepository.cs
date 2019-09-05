using System.Threading.Tasks;
using ZwajApp.Models;

namespace ZwajApp.Data
{
    public interface IAuthRepository
    {
         Task<User> Resgister(User user, string password);

         Task<User> Login (string username,string password);

         Task<bool> UserExists(string username);
    }
}