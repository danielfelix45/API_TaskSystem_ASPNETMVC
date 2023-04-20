using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repositories.Interfaces;

namespace TaskSystem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskSystemDbContext _dbContext;
        public TaskRepository(TaskSystemDbContext taskSystemDbContext)
        {
            _dbContext = taskSystemDbContext;
        }
        public async Task<List<TaskModel>> GetAll()
        {
            return await _dbContext.Tasks.Include(x => x.User).ToListAsync();
        }

        public async Task<TaskModel> GetById(int id)
        {
            return await _dbContext.Tasks.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> ToAdd(TaskModel task)
        {
           await _dbContext.Tasks.AddAsync(task);
           await _dbContext.SaveChangesAsync();
            return task;
        }
        public async Task<TaskModel> ToUpdate(TaskModel task, int id)
        {
            TaskModel taskById = await GetById(id);

            if(taskById == null)
            {
                throw new Exception($"The task for the ID: {id} not found in the database.");
            }

            taskById.Name = task.Name;
            taskById.Description = taskById.Description;
            taskById.Status = task.Status;
            taskById.UserId = task.UserId;

            _dbContext.Tasks.Update(taskById);
           await _dbContext.SaveChangesAsync();
            return taskById;
        }

        public async Task<bool> ToDelete(int id)
        {
            TaskModel taskById = await GetById(id);

            if(taskById == null )
            {
                throw new Exception($"The task for the ID: {id} not found in the database.");
            }

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
