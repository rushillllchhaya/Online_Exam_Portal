using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        // DbSet for all models
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<ProfessorModel> Professors { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<ExamModel> Exams { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<SubmissionModel> Submissions { get; set; }
        public DbSet<LogsModel> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Users table
            builder.Entity<UsersModel>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).UseIdentityColumn();
                entity.ToTable("Users");
            });

            // Professors table
            builder.Entity<ProfessorModel>(entity =>
            {
                entity.HasKey(e => e.ProfessorID);
                entity.Property(e => e.ProfessorID).UseIdentityColumn();
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<ProfessorModel>(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Professors");
            });

            // Students table
            builder.Entity<StudentModel>(entity =>
            {
                entity.HasKey(e => e.StudentID);
                entity.Property(e => e.StudentID).UseIdentityColumn();
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<StudentModel>(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Students");
            });

            // Subjects table
            builder.Entity<SubjectModel>(entity =>
            {
                entity.HasKey(e => e.SubjectID);
                entity.Property(e => e.SubjectID).UseIdentityColumn();

                // Define one-to-many relationship
                entity.HasOne(e => e.SubjectProfessor).WithMany(p => p.Subjects).HasForeignKey(e => e.ProfessorID).OnDelete(DeleteBehavior.Restrict);
                entity.ToTable("Subjects");
            });

            // Exams table
            builder.Entity<ExamModel>(entity =>
            {
                entity.HasKey(e => e.ExamID);
                entity.Property(e => e.ExamID).UseIdentityColumn();
                entity.HasOne(e => e.SubjectExam)
                      .WithMany(s => s.Exams)
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Exams");
            });

            // Questions table
            builder.Entity<QuestionModel>(entity =>
            {
                entity.HasKey(e => e.QuestionID);
                entity.Property(e => e.QuestionID).UseIdentityColumn();
                entity.HasOne(e => e.Questionexam).WithMany(e => e.questionexam).HasForeignKey(e => e.ExamID).OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Questions");
            });

            // Submissions table
            builder.Entity<SubmissionModel>(entity =>
            {
                entity.HasKey(e => e.SubmissionID);
                entity.Property(e => e.SubmissionID).UseIdentityColumn();
                entity.HasOne(e => e.Exam).WithMany(e => e.Submission).HasForeignKey(e => e.ExamID).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.QuestionSubmit).WithMany().HasForeignKey(e => e.QuestionID).OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Submissions");
            });

            // Monitoring Logs table
            builder.Entity<LogsModel>(entity =>
            {
                entity.HasKey(e => e.LogID);
                entity.Property(e => e.LogID).UseIdentityColumn();
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("MonitoringLogs");
            });
        }
    }
}
