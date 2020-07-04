using System.Threading.Tasks;
using dotnet_rpg.Model;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register(User pUser, string pPassword);

         Task<ServiceResponse<string>> Login(string pUserName, string pPassword);

         Task<bool> UserExist(string pUserName);
    }
}