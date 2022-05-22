using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAPI.Models;

namespace TeamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamContext _context;

        public TeamsController(TeamContext context)
        {
            _context = context;
        }

        // GET: api/Teams  ***GET ALL TEAMS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            return await _context.Teams.Include(t=> t.Players).ToListAsync();
        }
        
        // GET: api/Teams/5 ***GET TEAM BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // GET: api/Teams/sort ***GET TEAMS SORTED ALPHABETICALLY
        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsAlpha()
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }

            var alphaTeams = _context.Teams.OrderBy(t => t.Name);

            return Ok(alphaTeams);

        }

        // GET: api/Teams/Location ***GET TEAMS SORTED BY LOCATION
        [HttpGet("location")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsLocation()
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }

            var locationTeams = _context.Teams.OrderBy(t => t.Location);

            return Ok(locationTeams);

        }


        // Patch: api/Teams/5 ***ADD PLAYER TO TEAM
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutTeamAddPlayer(int id, [FromBody]int playerId)
        {
            // Grabs team and includes it's entire lineup of players.
            var team = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            // check for 8 or more players. 
            if (_context.Players.Count() >= 8)
            {
                BadRequest("Team already has 8 players.");
            }

            // check player id is not assigned to different team
            foreach (var p in _context.Teams.Include(t => t.Players).SelectMany(p => p.Players))
            {
                if (p.Id == playerId)
                {
                    return BadRequest("Player already on a different team.");
                }
            }

            //Grabs first player object with id.
            var player = _context.Players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                return NotFound("No player added.");
            }

            // Adds player to team.
            team.Players.Add(player);

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teams *** CREATE NEW TEAM
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
          if (_context.Teams == null)
          {
              return Problem("Entity set 'TeamContext.Teams'  is null.");
          }

          // Rejects duplicate name. 
          if (_context.Teams.Any(t => t.Name == team.Name) ||
              _context.Teams.Any(t => t.Location == team.Location))
          {
                return BadRequest("Team name or location already in use.");
          }
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
