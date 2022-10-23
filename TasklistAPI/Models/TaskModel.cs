using System;
using System.Collections.Generic;

namespace TasklistAPI.Models
{
    public partial class TaskModel
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}
