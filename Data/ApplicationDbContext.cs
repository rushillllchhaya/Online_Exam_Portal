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

        // DbSet for all models (EXCLUDING UsersModel, as Identity handles it)
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

            // Ensure Identity uses int-based keys and tables
            builder.Entity<UsersModel>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");

            // Prevent duplicate identity columns
            builder.Entity<UsersModel>()
                .Property(u => u.Id)
                .ValueGeneratedNever();

            // Professors table
            builder.Entity<ProfessorModel>(entity =>
            {
                entity.HasKey(e => e.ProfessorID);
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
                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(e => e.ExamID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.ToTable("Questions");
            });

            // Submissions table
            builder.Entity<SubmissionModel>(entity =>
            {
                entity.HasKey(e => e.SubmissionID);
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

            // Logs table
            builder.Entity<LogsModel>(entity =>
            {
                entity.HasKey(e => e.LogID);
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
                new UsersModel { UserID = 5, Id = 5, Name = "John Doe", Role = "Professor", Email = "johndoe@example.com", Password = hasher.HashPassword(null, "securepassword") },
                new UsersModel { UserID = 6, Id = 5, Name = "Alice Smith", Role = "Student", Email = "alicesmith@example.com", Password = hasher.HashPassword(null, "securepassword") }
            );

            builder.Entity<ProfessorModel>().HasData(
                new ProfessorModel { ProfessorID = 1, UserID = 5, Name = "John Doe", Number = 1234567890, Address = "123 University Street", Designation = "Head of Department" }
            );

            builder.Entity<StudentModel>().HasData(
                new StudentModel { StudentID = 1, UserID = 6, EnrollmentDate = new DateTime(2025, 02, 16), Number = 9876543210, Address = "456 College Avenue", SecondaryEmail = "alice.alt@example.com" }
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
