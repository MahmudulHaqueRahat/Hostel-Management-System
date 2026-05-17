using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<Room, RoomDTO>().ReverseMap();
            cfg.CreateMap<Resident, ResidentDTO>().ReverseMap();
            cfg.CreateMap<Expense, ExpenseDTO>().ReverseMap();
            cfg.CreateMap<Bill, BillDTO>()
                .ForMember(
                    dest => dest.ResidentName,
                    opt => opt.MapFrom(src => src.Resident.User.FullName)
                );
                

            cfg.CreateMap<RoomAllocation, RoomAllocationDTO>()
                .ForMember(
                    dest => dest.RoomNumber,
                    opt => opt.MapFrom(src => src.Room.RoomNumber)
                )
                .ForMember(
                    dest => dest.FloorNumber,
                    opt => opt.MapFrom(src => src.Room.FloorNumber)
                )
                .ForMember(
                    dest => dest.MonthlyRent,
                    opt => opt.MapFrom(src => src.Room.MonthlyRent)
                )
                .ForMember(
                    dest => dest.ResidentName,
                    opt => opt.MapFrom(src => src.Resident.User.FullName)
                )
                .ReverseMap()
                // When mapping DTO -> Entity, do not populate navigation properties.
                // This prevents AutoMapper from creating new Room/Resident entities
                // (which would be treated as Added by EF and may cause insert errors).
                .ForMember(dest => dest.Room, opt => opt.Ignore())
                .ForMember(dest => dest.Resident, opt => opt.Ignore());

        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }

    }
}