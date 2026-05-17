using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class RoomAllocationService
    {

        RoomAllocationRepo repo;
        Mapper mapper;
        public RoomAllocationService(RoomAllocationRepo repo)
        {
            this.repo = repo;

            this.mapper = MapperConfig.GetMapper();
        }
        public bool BookRoom(int residentId, int roomId)
        {
            bool alreadyRequested =
                repo.HasPendingRequest(residentId, roomId);

            if (alreadyRequested)
            {
                return false;
            }

            RoomAllocationDTO dto = new RoomAllocationDTO()
            {
                ResidentId = residentId,
                RoomId = roomId,
                AllocationDate = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true,
                Status = "Pending"
            };

            var data = mapper.Map<RoomAllocation>(dto);

            return repo.Create(data);
        }
        public List<int> GetPendingRoomIds(int residentId)
        {
            return repo.GetPendingRoomIds(residentId);
        }
        public RoomAllocationDTO GetApprovedRoom(int residentId)
        {
            var data = repo.GetApprovedRoom(residentId);

            return mapper.Map<RoomAllocationDTO>(data);
        }
        public List<RoomAllocationDTO> GetPendingRooms(int residentId)
        {
            var data = repo.GetPendingRooms(residentId);

            return mapper.Map<List<RoomAllocationDTO>>(data);
        }
        //for admin to find requests
        public List<RoomAllocationDTO> GetAllPendingRequests()
        {
            var data = repo.GetAllPendingRequests();

            return mapper.Map<List<RoomAllocationDTO>>(data);
        }

        public bool ApproveRoom(int allocationId)
        {
            return repo.ApproveRoom(allocationId);
        }

        public bool RejectRoom(int allocationId)
        {
            return repo.RejectRoom(allocationId);
        }

        public bool CancelBooking(int residentId, int roomId)
        {
            return repo.DeletePendingRequest(residentId, roomId);
        }

        public bool HasApprovedRoom(int residentId)
        {
            return repo.HasApprovedRoom(residentId);
        }

    }
}

