using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.AspNetCore.Identity;
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
                entity.HasOne(e => e.SubjectProfessor)
                      .WithMany(p => p.Subjects)
                      .HasForeignKey(e => e.ProfessorID)
                      .OnDelete(DeleteBehavior.Restrict);
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

            // Questions table (Fixed Naming)
            builder.Entity<QuestionModel>(entity =>
            {
                entity.HasKey(e => e.QuestionID);
                entity.Property(e => e.QuestionID).UseIdentityColumn();
                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(e => e.ExamID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Questions");
            });

            // Submissions table (Using StudentID instead of UserID)
            builder.Entity<SubmissionModel>(entity =>
            {
                entity.HasKey(e => e.SubmissionID);
                entity.Property(e => e.SubmissionID).UseIdentityColumn();
                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.Submissions)
                      .HasForeignKey(e => e.ExamID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.QuestionSubmit)
                      .WithMany()
                      .HasForeignKey(e => e.QuestionID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Submissions");
            });

            // Monitoring Logs table
            builder.Entity<LogsModel>(entity =>
            {
                entity.HasKey(e => e.LogID);
                entity.Property(e => e.LogID).UseIdentityColumn();
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("MonitoringLogs");
            });

            /////////////////////////////////////////////////////////
            // Seeding Data
            var hasher = new PasswordHasher<UsersModel>();

            builder.Entity<UsersModel>().HasData(
                new UsersModel { UserID = 1, Name = "John Doe", Role = "Professor", Email = "johndoe@example.com", Password = hasher.HashPassword(null, "securepassword") },
                new UsersModel { UserID = 2, Name = "Alice Smith", Role = "Student", Email = "alicesmith@example.com", Password = hasher.HashPassword(null, "securepassword") }
            );

            builder.Entity<ProfessorModel>().HasData(
                new ProfessorModel { ProfessorID = 1, UserID = 1, Name = "John Doe", Number = 1234567890, Address = "123 University Street", Designation = "Head of Department" }
            );

            builder.Entity<StudentModel>().HasData(
                new StudentModel { StudentID = 1, UserID = 2, EnrollmentDate = new DateTime(2025, 02, 16), Number = 9876543210, Address = "456 College Avenue", SecondaryEmail = "alice.alt@example.com" }
            );

            builder.Entity<SubjectModel>().HasData(
                new SubjectModel { SubjectID = 1, SubjectCode = "CS101", SubjectName = "Computer Science", ProfessorID = 1 }
            );

            builder.Entity<ExamModel>().HasData(
                new ExamModel { ExamID = 1, SubjectID = 1, Title = "Midterm Exam", Description = "Midterm covering first 5 chapters", Schedule = new DateTime(2025, 02, 23), Duration = 90, CreatedBy = 1 }
            );

            builder.Entity<QuestionModel>().HasData(
                new QuestionModel { QuestionID = 1, ExamID = 1, QuestionText = "What is OOP?", QuestionType = "MCQ", Options = "A) Object-Oriented Programming;B) Only One Process;C) Open Online Platform;D) None", CorrectAnswer = "A" }
            );

            builder.Entity<SubmissionModel>().HasData(
                new SubmissionModel { SubmissionID = 1, ExamID = 1, StudentID = 1, QuestionID = 1, Answer = "A" }
            );

            builder.Entity<LogsModel>().HasData(
                new LogsModel { LogID = 1, ExamID = 1, UserID = 1, Timestamp = new DateTime(2025, 02, 16, 10, 15, 00), ActivityTime = "10:15 AM", Notes = "Exam started successfully." }
            );
        }
    }
}
