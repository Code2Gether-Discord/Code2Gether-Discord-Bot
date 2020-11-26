using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    public class UsersController : Controller
    {
        #region Fields
        private DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public UsersController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        [HttpPost]
        public async Task AddUserAsync(User userToAdd)
        {
            await _dbContext.Users.AddAsync(userToAdd);
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToArrayAsync();
        }

        [HttpGet("{ID}")]
        public async Task<User> GetUserAsync(string ID)
        {
            long longId;

            if(!long.TryParse(ID, out longId))
            {
                // todo: return error JSON
                return null;
            }

            return await _dbContext.Users
                .FirstOrDefaultAsync(x => x.ID == longId);
        }
    }
}
