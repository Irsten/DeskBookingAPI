using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Services
{
    public interface IRoomService
    {
        bool CreateRoom();
        bool DeleteRoom(Room room);
        List<GetAllRoomsDto> GetAll();
    }

    public class RoomService : IRoomService
    {
        // TODO
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper; 

        public RoomService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateRoom()
        {
            _dbContext.Rooms.Add(new Room());
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteRoom(Room room)
        {
            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();

            return true;
        }


        public List<GetAllRoomsDto> GetAll()
        {
            var listOfRooms = new List<GetAllRoomsDto>();
            var rooms = _dbContext.Rooms.ToList();
            foreach (var room in rooms)
            {
                var tempDesks = _dbContext.Desks.Where(d => d.RoomId == room.Id).ToList();
                var tempRoom = new GetAllRoomsDto()
                {
                    RoomId = room.Id,
                    Desks = _mapper.Map<List<RoomDeskDto>>(tempDesks),
                };
                listOfRooms.Add(tempRoom);
            }
            return listOfRooms;
        }
    }
}
