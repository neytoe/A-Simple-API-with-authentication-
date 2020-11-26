using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFPractice.Dtos.Weapon;
using EFPractice.WeaponServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFPractice.Controllers
{  
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponservice)
        {
            _weaponService = weaponservice;
        }

        [HttpPost]
        public  async Task<IActionResult> AddWeapon (AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}
