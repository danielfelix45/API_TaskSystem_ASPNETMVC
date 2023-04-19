using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repositories.Interfaces;

namespace TaskSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSystemDbContext _dbContext;
        public UserRepository(TaskSystemDbContext taskSystemDbContext)
        {
            _dbContext = taskSystemDbContext;
        }
        public async Task<List<UserModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> ToAdd(UserModel user)
        {
           await _dbContext.Users.AddAsync(user);
           await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<UserModel> ToUpdate(UserModel user, int id)
        {
            UserModel userById = await GetById(id);

            if(userById == null)
            {
                throw new Exception($"The user for the ID: {id} not found in the database.");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
           await _dbContext.SaveChangesAsync();
            return userById;
        }

        public async Task<bool> ToDelete(int id)
        {
            UserModel userById = await GetById(id);

            if(userById == null )
            {
                throw new Exception($"The user for the ID: {id} not found in the database.");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
