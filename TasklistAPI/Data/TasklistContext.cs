using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TasklistAPI.Models;

namespace TasklistAPI.Data
{
    public partial class TasklistContext : DbContext
    {
        public TasklistContext()
        {
        }

        public TasklistContext(DbContextOptions<TasklistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TaskModel> TableTasksLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WVD\\SQLEXPRESS;Database=Tasklist;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.ToTable("Table_TasksList");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Task).HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
