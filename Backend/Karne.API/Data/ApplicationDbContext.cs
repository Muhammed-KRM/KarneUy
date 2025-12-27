using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<ExamHeader> ExamHeaders { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<RawDataUpload> RawDataUploads { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentExamResult> StudentExamResults { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<AppLog> AppLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<ClassMessage> ClassMessages { get; set; }
        
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // Social
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<SocialFollow> SocialFollows { get; set; }
        public DbSet<SocialAction> SocialActions { get; set; }
        public DbSet<SocialComment> SocialComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure StudentNumber unique index
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentNumber)
                .IsUnique();

            // Configure Delete Behaviors to restrict or cascade as needed
            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.Questions)
                .HasForeignKey(eq => eq.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Lesson)
                .WithMany(l => l.Topics)
                .HasForeignKey(t => t.LessonId)
                .OnDelete(DeleteBehavior.NoAction);

            // Social Follow Unique & Restrict
            modelBuilder.Entity<SocialFollow>()
                .HasIndex(f => new { f.FollowerId, f.FollowingId })
                .IsUnique();

            modelBuilder.Entity<SocialFollow>()
                .HasOne(f => f.Follower)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<SocialFollow>()
                .HasOne(f => f.Following)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Avoid Cycles for SocialAction
            modelBuilder.Entity<SocialAction>()
                .HasOne(a => a.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete actions when user is deleted (handle manually or keep)

            modelBuilder.Entity<SocialAction>()
                .HasOne(a => a.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SocialAction>()
                .HasOne(a => a.Test)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Test Question Order Index
            modelBuilder.Entity<TestQuestion>()
                .HasIndex(t => new { t.TestId, t.Order });
        }
    }
}
