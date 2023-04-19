using TaskSystem.Models;

namespace TaskSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> ToAdd(UserModel user);
        Task<UserModel> ToUpdate(UserModel user, int id);
        Task<bool> ToDelete(int id);
    }
}
