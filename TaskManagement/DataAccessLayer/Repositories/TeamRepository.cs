using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement;
using TaskManagement.Models;

namespace DataAccessLayer.Repositories
{
    public class TeamRepository
    {
        private readonly TaskDbContext _dbContext;

        public TeamRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _dbContext.Teams.Include(a => a.UserTeam).ThenInclude(a => a.User).ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return _dbContext.Teams.Include(a=> a.UserTeam).ThenInclude(a => a.User).FirstOrDefault(a => a.TeamId == teamId);
        }

        public async Task AddTeamAsync(Team team)
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }

        public async Task AddTeamUserAsync(UserTeam t)
        {
            _dbContext.UserTeam.Add(t);
            //var team = await this.GetTeamByIdAsync(tObj.TeamId);
            //team.UserTeam.Add(t);
            //_dbContext.Entry(team).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }


        public async Task<UserTeam> GetTeamUser(int teamId, int userId)
        {
            return await _dbContext.UserTeam.Include(a => a.User).Include(a => a.Team).FirstOrDefaultAsync(a => a.UserId == userId && a.TeamId == teamId);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            var team = await _dbContext.Teams.FindAsync(teamId);
            if (team != null)
            {
                _dbContext.Teams.Remove(team);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUserTeam(UserTeam userTeam)
        {
            var uTeam = await _dbContext.UserTeam.FirstOrDefaultAsync(a => a.TeamId == userTeam.TeamId && a.UserId == userTeam.UserId);
            if (uTeam != null)
            {
                _dbContext.UserTeam.Remove(uTeam);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
