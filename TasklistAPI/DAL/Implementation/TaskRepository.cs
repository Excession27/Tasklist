using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasklistAPI.DAL.Interfaces;
using TasklistAPI.Data;
using TasklistAPI.Models;

namespace TasklistAPI.DAL.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasklistContext _tasklistContext;


        public TaskRepository(TasklistContext context)
        {

            _tasklistContext = context;
        }
        public async Task<TaskModel> Create(TaskModel task)
        {

                task.EntryDate = DateTime.Now;
                task.IsCompleted = false;
                task.Task = $"000 - {task.Task}";
                await _tasklistContext.TableTasksLists.AddAsync(task);
                await _tasklistContext.SaveChangesAsync();
                return task;

        }

        public async Task<string> Delete(IEnumerable<int> list)
        {
            try
            {
                List<TaskModel> allTasks = _tasklistContext.TableTasksLists.ToList();

                list.ToList().ForEach(task => _tasklistContext.TableTasksLists.Remove(allTasks.First(t => t.Id == task)));
                await _tasklistContext.SaveChangesAsync();
                return "Done.";
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }

        }

        public async Task<TaskModel> Duplicate(int id)
        {

                TaskModel old = _tasklistContext.TableTasksLists.Find(id)!;
                TaskModel duplicate = new TaskModel();
                duplicate.Task = $"000 - Copy of {old.Task}";
                duplicate.IsCompleted = old.IsCompleted;
                duplicate.EntryDate = old.EntryDate;
                await _tasklistContext.TableTasksLists.AddAsync(duplicate);
                await _tasklistContext.SaveChangesAsync();
                return duplicate;

        }

        public string Update(TaskModel task)
        {
             _tasklistContext.TableTasksLists.Update(task);
            _tasklistContext.SaveChanges();
            return "Successfully updated";

        }

        public TaskModel Get(int id)
        {
            return _tasklistContext.TableTasksLists.Find(id)!;
        }

        public PaginationResult GetAll(PaginationParams @params)
        {
            IEnumerable<TaskModel> list = _tasklistContext.TableTasksLists.ToList();
            IEnumerable<TaskModel> result = list.Skip((@params.Page - 1) * @params.ItemsPerPage).Take(@params.ItemsPerPage).ToList();
            PaginationMetadata paginationMetadata = new PaginationMetadata(list.Count(), @params.Page, @params.ItemsPerPage);

            PaginationResult finalResult = new PaginationResult(result, paginationMetadata);

            return finalResult;
        }


    }
}
