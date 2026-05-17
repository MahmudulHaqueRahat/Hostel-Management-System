using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class NotificationRepo
    {
        HostelContext db;

        public NotificationRepo(HostelContext db)
        {
            this.db = db;
        }

        public bool Create(Notification notification)
        {
            db.Notifications.Add(notification);

            return db.SaveChanges() > 0;
        }

        public List<Notification> GetByUser(int userId)
        {
            return db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public int GetUnreadCount(int userId)
        {
            return db.Notifications
                .Count(n =>
                    n.UserId == userId &&
                    n.IsRead == false);
        }

        public void MarkAllAsRead(int userId)
        {
            var notifications = db.Notifications
                .Where(n =>
                    n.UserId == userId &&
                    n.IsRead == false)
                .ToList();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            db.SaveChanges();
        }
    }
}