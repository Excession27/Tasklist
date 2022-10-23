namespace TasklistAPI.Models
{
    public class PaginationResult
    {
        public PaginationResult(IEnumerable<TaskModel> taskList, PaginationMetadata meta)
        {
            TaskList = taskList;
            PaginationMetadata = meta;
        }
        public IEnumerable<TaskModel> TaskList { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }
    }
}
