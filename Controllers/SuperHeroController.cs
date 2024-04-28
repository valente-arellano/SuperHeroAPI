using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : Controller
    {
        private readonly ILogger<SuperHeroController> _logger;
        private readonly DataContext _context;

        public SuperHeroController(ILogger<SuperHeroController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public async Task<ActionResult<SuperHero>> GetAllHeroes() {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SuperHero>> GetHeroById(int id) {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero is null) {
                return NotFound("Hero not found.");
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> CreateHero(SuperHero newHero) {
            _context.SuperHeroes.Add(newHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero newHero) {
            var hero = await _context.SuperHeroes.FindAsync(newHero.Id);

            if (hero is null) {
                return NotFound("Hero not found.");
            }

            hero.Name = newHero.Name;
            hero.FirstName = newHero.FirstName;
            hero.LastName = newHero.LastName;
            hero.Place = newHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id) {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero is null) {
                return NotFound("Hero not found.");
            }

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}