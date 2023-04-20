using TaskSystem.Models;

namespace TaskSystem.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAll();
        Task<TaskModel> GetById(int id);
        Task<TaskModel> ToAdd(TaskModel task);
        Task<TaskModel> ToUpdate(TaskModel task, int id);
        Task<bool> ToDelete(int id);
    }
}
