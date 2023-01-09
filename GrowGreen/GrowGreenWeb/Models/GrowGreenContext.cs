using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrowGreenWeb.Models
{
    public partial class GrowGreenContext : DbContext
    {
        public GrowGreenContext()
        {
        }

        public GrowGreenContext(DbContextOptions<GrowGreenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Badge> Badges { get; set; } = null!;
        public virtual DbSet<BadgeLearner> BadgeLearners { get; set; } = null!;
        public virtual DbSet<CarbonHistory> CarbonHistories { get; set; } = null!;
        public virtual DbSet<CarbonType> CarbonTypes { get; set; } = null!;
        public virtual DbSet<CarbonTypeHistory> CarbonTypeHistories { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<ChatReport> ChatReports { get; set; } = null!;
        public virtual DbSet<Completed> Completeds { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseReview> CourseReviews { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<GivingReview> GivingReviews { get; set; } = null!;
        public virtual DbSet<ItemType> ItemTypes { get; set; } = null!;
        public virtual DbSet<Lecture> Lectures { get; set; } = null!;
        public virtual DbSet<Newsletter> Newsletters { get; set; } = null!;
        public virtual DbSet<NewsletterEditHistory> NewsletterEditHistories { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PointsEarned> PointsEarneds { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizChoice> QuizChoices { get; set; } = null!;
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; } = null!;
        public virtual DbSet<RecyclingLocation> RecyclingLocations { get; set; } = null!;
        public virtual DbSet<RecyclingRecord> RecyclingRecords { get; set; } = null!;
        public virtual DbSet<RedeemReward> RedeemRewards { get; set; } = null!;
        public virtual DbSet<ReportHistory> ReportHistories { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Reward> Rewards { get; set; } = null!;
        public virtual DbSet<SearchRequest> SearchRequests { get; set; } = null!;
        public virtual DbSet<SearchResult> SearchResults { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Tip> Tips { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<VideoCompletion> VideoCompletions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:GrowGreenDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Badges)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Badge_Course");
            });

            modelBuilder.Entity<BadgeLearner>(entity =>
            {
                entity.HasKey(e => new { e.BadgeId, e.LearnerId });

                entity.ToTable("BadgeLearner");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.BadgeLearners)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("FK_BadgeLearner_Badge");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.BadgeLearners)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK_BadgeLearner_User");
            });

            modelBuilder.Entity<CarbonHistory>(entity =>
            {
                entity.ToTable("CarbonHistory");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.CarbonHistories)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK_CarbonHistory_User");
            });

            modelBuilder.Entity<CarbonType>(entity =>
            {
                entity.ToTable("CarbonType");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UnitName).HasMaxLength(100);
            });

            modelBuilder.Entity<CarbonTypeHistory>(entity =>
            {
                entity.ToTable("CarbonTypeHistory");

                entity.HasOne(d => d.CarbonHistory)
                    .WithMany(p => p.CarbonTypeHistories)
                    .HasForeignKey(d => d.CarbonHistoryId)
                    .HasConstraintName("FK_CarbonTypeHistory_CarbonHistory");

                entity.HasOne(d => d.CarbonType)
                    .WithMany(p => p.CarbonTypeHistories)
                    .HasForeignKey(d => d.CarbonTypeId)
                    .HasConstraintName("FK_CarbonTypeHistory_CarbonType");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.Content).HasMaxLength(280);

                entity.Property(e => e.EditedTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Chat_Course");

                entity.HasOne(d => d.ReplyToChat)
                    .WithMany(p => p.InverseReplyToChat)
                    .HasForeignKey(d => d.ReplyToChatId)
                    .HasConstraintName("FK_Chat_Chat");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Chat_User");
            });

            modelBuilder.Entity<ChatReport>(entity =>
            {
                entity.ToTable("ChatReport");

                entity.Property(e => e.Content).HasMaxLength(280);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatReports)
                    .HasForeignKey(d => d.ChatId)
                    .HasConstraintName("FK_ChatReport_Chat");
            });

            modelBuilder.Entity<Completed>(entity =>
            {
                entity.ToTable("Completed");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Name, "IX_Course")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(200);

                entity.Property(e => e.LastUpdatedTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_User");
            });

            modelBuilder.Entity<CourseReview>(entity =>
            {
                entity.ToTable("CourseReview");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseReviews)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Review_Course");
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.ToTable("Donation");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Purpose).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Donation_User");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Donations)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Donation_Payment");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Attendee).HasMaxLength(200);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SpecialGuest).HasMaxLength(200);
            });

            modelBuilder.Entity<GivingReview>(entity =>
            {
                entity.ToTable("GivingReview");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.GivingReviews)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_GivingReview_Post");
            });

            modelBuilder.Entity<ItemType>(entity =>
            {
                entity.ToTable("ItemType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lecture");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Lecture_Course");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("Newsletter");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<NewsletterEditHistory>(entity =>
            {
                entity.ToTable("NewsletterEditHIstory");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Newsletter)
                    .WithMany(p => p.NewsletterEditHistories)
                    .HasForeignKey(d => d.NewsletterId)
                    .HasConstraintName("FK_NewsletterEditHIstory_Newsletter");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsletterEditHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_NewsletterEditHIstory_User");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("Payment");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(200);

                entity.Property(e => e.TransactionType).HasMaxLength(50);
            });

            modelBuilder.Entity<PointsEarned>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("PointsEarned");

                entity.Property(e => e.TransactionId).ValueGeneratedNever();

                entity.HasOne(d => d.Transaction)
                    .WithOne(p => p.PointsEarned)
                    .HasForeignKey<PointsEarned>(d => d.TransactionId)
                    .HasConstraintName("FK_PointsEarned_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Quiz_Lecture");
            });

            modelBuilder.Entity<QuizChoice>(entity =>
            {
                entity.ToTable("QuizChoice");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.HasOne(d => d.QuizQuestion)
                    .WithMany(p => p.QuizChoices)
                    .HasForeignKey(d => d.QuizQuestionId)
                    .HasConstraintName("FK_QuizChoice_QuizQuestion");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.ToTable("QuizQuestion");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_QuizQuestion_Quiz");
            });

            modelBuilder.Entity<RecyclingLocation>(entity =>
            {
                entity.ToTable("RecyclingLocation");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<RecyclingRecord>(entity =>
            {
                entity.ToTable("RecyclingRecord");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.ItemType)
                    .WithMany(p => p.RecyclingRecords)
                    .HasForeignKey(d => d.ItemTypeId)
                    .HasConstraintName("FK_RecyclingRecord_ItemType");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.RecyclingRecords)
                    .HasForeignKey(d => d.LearnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecyclingRecord_User");

                entity.HasOne(d => d.RecyclingLocation)
                    .WithMany(p => p.RecyclingRecords)
                    .HasForeignKey(d => d.RecyclingLocationId)
                    .HasConstraintName("FK_RecyclingRecord_RecyclingLocation");
            });

            modelBuilder.Entity<RedeemReward>(entity =>
            {
                entity.HasKey(e => new { e.RewardId, e.UserId })
                    .HasName("PK_RedeemRewards");

                entity.ToTable("RedeemReward");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.RedeemRewards)
                    .HasForeignKey(d => d.RewardId)
                    .HasConstraintName("FK_RedeemReward_Reward");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RedeemRewards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RedeemReward_User");
            });

            modelBuilder.Entity<ReportHistory>(entity =>
            {
                entity.ToTable("ReportHistory");

                entity.Property(e => e.Filename).HasMaxLength(200);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.ReportHistories)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_ReportHistory_User");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Post");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_Request_User");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Reward");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<SearchRequest>(entity =>
            {
                entity.ToTable("SearchRequest");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Keyword).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.HasOne(d => d.SearchResult)
                    .WithMany(p => p.SearchRequests)
                    .HasForeignKey(d => d.SearchResultId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SearchRequest_SearchResult");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SearchRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SearchRequest_User");
            });

            modelBuilder.Entity<SearchResult>(entity =>
            {
                entity.ToTable("SearchResult");

                entity.HasMany(d => d.Posts)
                    .WithMany(p => p.SearchResults)
                    .UsingEntity<Dictionary<string, object>>(
                        "SearchResultPost",
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostId").HasConstraintName("FK_SearchResultPost_Post"),
                        r => r.HasOne<SearchResult>().WithMany().HasForeignKey("SearchResultId").HasConstraintName("FK_SearchResultPost_SearchResult"),
                        j =>
                        {
                            j.HasKey("SearchResultId", "PostId");

                            j.ToTable("SearchResultPost");
                        });
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Ticket_Event");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Ticket_User");
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.ToTable("Tip");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.HasOne(d => d.Completed)
                    .WithMany(p => p.Tips)
                    .HasForeignKey(d => d.CompletedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tip_Completed");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Tips)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_Tip_Payment");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.Qualification).HasMaxLength(200);

                entity.Property(e => e.SignupTimestamp).HasColumnType("datetime");

                entity.HasMany(d => d.CoursesNavigation)
                    .WithMany(p => p.Learners)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseSignup",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("FK_CourseSignup_Course"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("LearnerId").HasConstraintName("FK_CourseSignup_User"),
                        j =>
                        {
                            j.HasKey("LearnerId", "CourseId");

                            j.ToTable("CourseSignup");
                        });

                entity.HasMany(d => d.QuizChoices)
                    .WithMany(p => p.Learners)
                    .UsingEntity<Dictionary<string, object>>(
                        "QuizResponse",
                        l => l.HasOne<QuizChoice>().WithMany().HasForeignKey("QuizChoiceId").HasConstraintName("FK_QuizResponse_QuizChoice"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("LearnerId").HasConstraintName("FK_QuizResponse_User"),
                        j =>
                        {
                            j.HasKey("LearnerId", "QuizChoiceId");

                            j.ToTable("QuizResponse");
                        });
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Video");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Video_Lecture");
            });

            modelBuilder.Entity<VideoCompletion>(entity =>
            {
                entity.ToTable("VideoCompletion");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.VideoCompletions)
                    .HasForeignKey(d => d.LearnerId)
                    .HasConstraintName("FK_VideoCompletion_User");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.VideoCompletions)
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("FK_VideoCompletion_Video");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
