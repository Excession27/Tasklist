using TasklistAPI.Models;

namespace TasklistAPI.DAL.Interfaces
{
    public interface ITaskRepository
    {
        PaginationResult GetAll(PaginationParams @params);

        TaskModel Get(int id);

        Task<string> Delete(IEnumerable<int> list);

        Task<TaskModel> Duplicate(int id);

        Task<TaskModel> Create(TaskModel task);

        string Update(TaskModel task);
    }
}
