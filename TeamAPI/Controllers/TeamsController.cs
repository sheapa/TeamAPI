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
            return await _context.Teams.ToListAsync();
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


        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/addPlayer/{playerId}")]
        public async Task<IActionResult> PutTeamAddPlayer(int id, Team team, int playerId)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            if (team.Players.Count < 8)
            {
                var player = await _context.Players.FindAsync(playerId);
                team.Players.Add(player);
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
          if (_context.Teams == null)
          {
              return Problem("Entity set 'TeamContext.Teams'  is null.");
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
