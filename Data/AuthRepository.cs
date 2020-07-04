using System.Threading.Tasks;
using dotnet_rpg.Model;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext pContext)
        {
            this.context = pContext;

        }

        public async Task<ServiceResponse<string>> Login(string pUserName, string pPassword)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(pUserName.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }

            else if(!VerifyPasswordHash(pPassword, user.PasswordHash, user.PasswordSalt)){
                
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
                response.Data = user.Id.ToString();

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User pUser, string pPassword)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(await UserExist(pUser.UserName))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatePasswordHas(pPassword, out byte[] pPasswordHash, out byte [] pPasswordSalt);
            pUser.PasswordHash = pPasswordHash;
            pUser.PasswordSalt = pPasswordSalt;
            
            await context.Users.AddAsync(pUser);
            await context.SaveChangesAsync();
            response.Data = pUser.Id;
            return response;
        }

        public async Task<bool> UserExist(string pUserName)
        {
            if(await context.Users.AnyAsync(u=> u.UserName.ToLower() == pUserName.ToLower()))
                return true;
            
            return false;
        }

        private void CreatePasswordHas(string pPassword, out byte[] pPasswordHash, out byte[] pPasswordSalt){
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pPasswordSalt = hmac.Key;
                pPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pPassword));
            }   
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt){
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

    }
}