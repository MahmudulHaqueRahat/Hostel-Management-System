using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class NotificationService
    {
        NotificationRepo repo;
        Mapper mapper;

        public NotificationService(NotificationRepo repo)
        {
            this.repo = repo;

            this.mapper = MapperConfig.GetMapper();
        }

        public bool Create(NotificationDTO dto)
        {
            var data = mapper.Map<Notification>(dto);

            return repo.Create(data);
        }

        public List<NotificationDTO> GetByUser(int userId)
        {
            var data = repo.GetByUser(userId);

            return mapper.Map<List<NotificationDTO>>(data);
        }

        public int GetUnreadCount(int userId)
        {
            return repo.GetUnreadCount(userId);
        }

        public void MarkAllAsRead(int userId)
        {
            repo.MarkAllAsRead(userId);
        }
    }
}