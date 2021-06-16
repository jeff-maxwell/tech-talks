using BlazorSuperHeroClientServer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSuperHeroClientServer.Server.Interfaces
{
    public interface ISuperHeroService
    {
        Task<IEnumerable<Hero>> Get();
    }
}
