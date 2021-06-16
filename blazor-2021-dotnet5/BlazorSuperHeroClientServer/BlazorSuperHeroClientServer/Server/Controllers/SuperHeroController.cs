using BlazorSuperHeroClientServer.Server.Interfaces;
using BlazorSuperHeroClientServer.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSuperHeroClientServer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ILogger<SuperHeroController> _logger;
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ILogger<SuperHeroController> logger,
                                    ISuperHeroService superHeroService)
        {
            _logger = logger;
            _superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _superHeroService.Get();
            return Ok(result);
        }
    }
}
