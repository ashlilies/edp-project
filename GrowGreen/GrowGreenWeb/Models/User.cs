using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class User
    {
        public User()
        {
            BadgeLearners = new HashSet<BadgeLearner>();
            CarbonHistories = new HashSet<CarbonHistory>();
            Chats = new HashSet<Chat>();
            CourseSignups = new HashSet<CourseSignup>();
            Courses = new HashSet<Course>();
            Donations = new HashSet<Donation>();
            NewsletterEditHistories = new HashSet<NewsletterEditHistory>();
            Posts = new HashSet<Post>();
            RecyclingLocations = new HashSet<RecyclingLocation>();
            RecyclingRecords = new HashSet<RecyclingRecord>();
            RedeemRewards = new HashSet<RedeemReward>();
            ReportHistories = new HashSet<ReportHistory>();
            Requests = new HashSet<Request>();
            SearchRequests = new HashSet<SearchRequest>();
            Tickets = new HashSet<Ticket>();
            UserSessions = new HashSet<UserSession>();
            VideoCompletions = new HashSet<VideoCompletion>();
            QuizChoices = new HashSet<QuizChoice>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhotoFilename { get; set; }
        public string Phone { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public DateTime SignupTimestamp { get; set; }
        public string Address { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Qualification { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLecturer { get; set; }
        public bool IsLearner { get; set; }

        public virtual PointsEarned? PointsEarned { get; set; }
        public virtual ICollection<BadgeLearner> BadgeLearners { get; set; }
        public virtual ICollection<CarbonHistory> CarbonHistories { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<CourseSignup> CourseSignups { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<NewsletterEditHistory> NewsletterEditHistories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<RecyclingLocation> RecyclingLocations { get; set; }
        public virtual ICollection<RecyclingRecord> RecyclingRecords { get; set; }
        public virtual ICollection<RedeemReward> RedeemRewards { get; set; }
        public virtual ICollection<ReportHistory> ReportHistories { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }
        public virtual ICollection<VideoCompletion> VideoCompletions { get; set; }

        public virtual ICollection<QuizChoice> QuizChoices { get; set; }
    }
}
