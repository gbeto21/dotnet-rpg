using System.Threading.Tasks;
using dotnet_rpg.Dtos;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            this.weaponService = weaponService;

        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon){
            return Ok(await weaponService.AddWeapon(newWeapon));
        }
    }
}