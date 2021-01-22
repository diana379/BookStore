using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {   
            if(!userManager.Users.Any())
            {
                 var user = new AppUser
            {
                DisplayName = "Diana",
                Email = "diana@test.com",
                UserName = "diana@test.com",
                Adress = new Adress
                {
                    Nume = "Incrosnatu",
                    Prenume = "Diana",
                    Strada = "Cal. Calarasilor",
                    Oras = "Braila",
                    CodPostal = "6100"
                }
            };
            await userManager.CreateAsync(user, "P@ssw0rd");
            }
           
        }
    }
}