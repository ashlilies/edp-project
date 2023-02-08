namespace GrowGreenWeb.Models;

public partial class Chat
{
    public Chat Clone()
    {
        return new Chat
        {
            Id = Id,
            Timestamp = Timestamp,
            Content = Content,
            IsReadByLecturer = IsReadByLecturer,
            UserId = UserId,
            User = User,
            EditedTimestamp = EditedTimestamp,
            CourseId = CourseId,
            Course = Course,
            ReplyToChatId = ReplyToChatId,
            ReplyToChat = ReplyToChat,
            AttachmentUrl = AttachmentUrl
        };
    }
}

public partial class Course
{
    public int AvailableCapacity
    {
        get => MaxCapacity - CourseSignups.Count;
    }
}

public partial class Email
{
    // todo: removet his workaround
    public DateTime TimestampSgp
    {
        get => Timestamp.AddHours(8);
    }
}