using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsersModel, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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

            // Ensure UsersModel table uses UserID as the primary key
            builder.Entity<UsersModel>().ToTable("Users")
                .HasKey(u => u.UserID); // Use UserID as the primary key

            builder.Entity<UsersModel>().Property(u => u.UserID)
                .ValueGeneratedOnAdd(); // Only this column should be identity

            builder.Entity<UsersModel>().Property(u => u.Id)
                .HasColumnName("UserID") // Rename IdentityUser<int>'s Id to UserID
                .ValueGeneratedNever(); // Prevent IdentityUser<int> from generating Id separately

            // Professors
            builder.Entity<ProfessorModel>(entity =>
            {
                entity.HasKey(e => e.ProfessorID);
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<ProfessorModel>(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Students
            builder.Entity<StudentModel>(entity =>
            {
                entity.HasKey(e => e.StudentID);
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<StudentModel>(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Subjects
            builder.Entity<SubjectModel>(entity =>
            {
                entity.HasKey(e => e.SubjectID);
                entity.HasOne(e => e.Professor)
                      .WithMany()
                      .HasForeignKey(e => e.ProfessorID)
                      .OnDelete(DeleteBehavior.Restrict); // Prevents deletion of referenced Professors
            });

            // Exams
            builder.Entity<ExamModel>(entity =>
            {
                entity.HasKey(e => e.ExamID);
                entity.HasOne(e => e.Subject)
                      .WithMany(s => s.Exams)
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Professor)
                      .WithMany()
                      .HasForeignKey(e => e.CreatedBy)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Questions
            builder.Entity<QuestionModel>(entity =>
            {
                entity.HasKey(e => e.QuestionID);
                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(e => e.ExamID)
                      .OnDelete(DeleteBehavior.Restrict); //  Prevents Question deletion if submissions exist

            });

            // Submissions (Fixing Cascade Issues)
            builder.Entity<SubmissionModel>(entity =>
            {
                entity.HasKey(e => e.SubmissionID);
                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.Submissions)
                      .HasForeignKey(e => e.ExamID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Question)
                      .WithMany()
                      .HasForeignKey(e => e.QuestionID)
                      .OnDelete(DeleteBehavior.SetNull); // Make sure QuestionID is nullable
            });

            // Logs
            builder.Entity<LogsModel>(entity =>
            {
                entity.HasKey(e => e.LogID);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
