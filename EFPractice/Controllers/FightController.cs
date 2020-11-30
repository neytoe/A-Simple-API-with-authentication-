using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFPractice.Dtos.Fight;
using EFPractice.Services.FightService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IFightServices _fightService;

        public FightController(IFightServices fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("weapon")]
        public async Task<IActionResult> Weaponattack (WeaponAttackDto request)
        {
            return Ok(await _fightService.weaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<IActionResult> Skillattack(SkillAttackDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }
    }
}
